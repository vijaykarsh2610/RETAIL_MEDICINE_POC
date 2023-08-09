using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using MedicineManagement.ViewModels;
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
            List<DiseaseViewModel> diseases = new List<DiseaseViewModel>();

            if (_service != null && _service.GetDiseases() != null && _service.GetDiseases().Count() > 0)
            {
                diseases = _service.GetDiseases().Select(disease =>
                    new DiseaseViewModel
                    {
                        Id = disease.Id,
                        DiseaseCategory = disease.DiseaseCategory,
                        ImagePath = disease.ImagePath
                    }
                ).ToList();
            }


            return View(diseases); return View();
        }


        [HttpGet]
        public IActionResult Create()
        {
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
                        model.ImagePath = "/images/" + fileName; // Update the path here
                    }

                    _service.AddDisease(model);
                    return RedirectToAction("Create", "Disease");
                }
                else
                {
                    var categories = _service.GetDiseaseCategories();
                    ViewBag.Categories = new SelectList(categories);
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                // In a production application, you might want to log the error or show an error page
                ModelState.AddModelError("", "An error occurred while processing the request. Please try again later.");
                var categories = _service.GetDiseaseCategories();
                ViewBag.Categories = new SelectList(categories);
                return View(model);
            }
        }
    }
}
