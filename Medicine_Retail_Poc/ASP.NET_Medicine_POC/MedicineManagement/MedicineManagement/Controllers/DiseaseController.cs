using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicineManagement.Controllers
{
    public class DiseaseController : Controller
    {
        private readonly IDiseaseService _service;
        private readonly IWebHostEnvironment _environment;

        public DiseaseController(IDiseaseService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        public IActionResult Home()
        {
            List<Disease> diseases = new List<Disease>();

            if (_service != null && _service.GetDiseases() != null && _service.GetDiseases().Count() > 0)
            {
                diseases = _service.GetDiseases().Select(disease =>
                    new Disease
                    {
                        Id = disease.Id,
                        DiseaseCategory = disease.DiseaseCategory,
                        ImagePath = disease.ImagePath
                    }
                ).ToList();
            }


            return View(diseases);
        }


        [HttpGet]
        public IActionResult Create()
        {
            // Display the alert message if available
            if (TempData["LoginMessage"] != null)
            {
                ViewBag.LoginMessage = TempData["LoginMessage"];
                ViewBag.IsAdmin = TempData["IsAdmin"] != null && (bool)TempData["IsAdmin"];

                // Clear TempData to prevent the message from showing again on subsequent requests
                TempData["LoginMessage"] = null;
                TempData["IsAdmin"] = null;
            }

            var categories = _service.GetDiseaseCategories();
            ViewBag.Categories = new SelectList(categories);
            return View();
        }

        [HttpPost]
        public IActionResult Create(Disease model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existingDiseaseWithCategory = _service.GetDiseases()
                        .FirstOrDefault(d => d.DiseaseCategory == model.DiseaseCategory);

                    if (existingDiseaseWithCategory != null)
                    {
                        ModelState.AddModelError("DiseaseCategory", "A disease with the same category already exists.");
                    }
                    else
                    {
                        if (model.ImageFile != null && model.ImageFile.Length > 0)
                        {
                            // Generate a unique filename for the image
                            var fileName = Path.GetRandomFileName() + Path.GetExtension(model.ImageFile.FileName);

                            // Save the image to the wwwroot/images directory
                            var imagePath = Path.Combine(_environment.WebRootPath, "images", fileName);
                            using (var fileStream = new FileStream(imagePath, FileMode.Create))
                            {
                                model.ImageFile.CopyTo(fileStream);
                            }

                            // Set the ImagePath property of the disease object to the filename
                            model.ImagePath = "/images/" + fileName;
                        }

                        _service.AddDisease(model);

                        // Set a temporary success message to be displayed on the next page load
                        TempData["Message"] = "Disease added successfully.";

                        return RedirectToAction("Create", "Disease");
                    }
                }

                // If ModelState is not valid or a duplicate disease category was found,
                // populate the ViewBag and return the view
                var categories = _service.GetDiseaseCategories();
                ViewBag.Categories = new SelectList(categories);
                return View(model);
            }
            catch (Exception ex)
            {
                // Handle the exception
                ModelState.AddModelError("", "An error occurred while processing the request. Please try again later.");
                var categories = _service.GetDiseaseCategories();
                ViewBag.Categories = new SelectList(categories);
                return View(model);
            }
        }

    }
}
