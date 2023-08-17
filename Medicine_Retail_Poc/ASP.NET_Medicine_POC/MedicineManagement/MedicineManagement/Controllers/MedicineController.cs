using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MedicineManagement.Controllers
{
    public class MedicineController : Controller
    {
        private readonly IMedicineService _service;

        private readonly IWebHostEnvironment _environment;

        public MedicineController(IMedicineService service, IWebHostEnvironment environment)
        {
            _service = service;
            _environment = environment;
        }

        [HttpGet]
        public IActionResult Home(string category)
        {
            if (string.IsNullOrEmpty(category))
            {
                // Handle the case when no category is provided
                return RedirectToAction("Index", "Home");
            }

            var medicinesByCategory = _service.GetMedicinesByCategory(category);
            ViewBag.Category = category; // Set the ViewBag.Category for the view
            return View(medicinesByCategory);
        }

        [HttpGet]
        public IActionResult Index()
        {
            var categories = _service.GetDiseaseCategories();
            ViewBag.Categories = new SelectList(categories);
            return View();
        }

        [HttpPost]
        public IActionResult Index(Medicine model)
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
                        model.ImagePath = "/images/" + fileName;
                    }

                    _service.AddMedicine(model);
                    return RedirectToAction("Index", "Medicine");
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

        //add methods to update,delete medicines

        [HttpGet]
        public IActionResult Update(int id)
        {
            var medicine = _service.GetMedicineById(id);
            if (medicine == null)
            {
                return NotFound();
            }

            var categories = _service.GetDiseaseCategories();
            ViewBag.Categories = new SelectList(categories);
            return View(medicine);
        }

        [HttpPost]
        public IActionResult Update(Medicine model)
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

                        // Set the ImagePath property of the medicine object to the filename
                        model.ImagePath = "/images/" + fileName;
                    }

                    _service.UpdateMedicine(model);
                    return RedirectToAction("Index", "Medicine");
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
                ModelState.AddModelError("", "An error occurred while processing the request. Please try again later.");
                var categories = _service.GetDiseaseCategories();
                ViewBag.Categories = new SelectList(categories);
                return View(model);
            }
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                var medicine = _service.GetMedicineById(id);
                if (medicine == null)
                {
                    return NotFound();
                }

                _service.DeleteMedicine(medicine);
                TempData["Message"] = "Deleted";
                return RedirectToAction("Home", "Disease");
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                // In a production application, you might want to log the error or show an error page
                ModelState.AddModelError("", "An error occurred while processing the request. Please try again later.");
                return View();
            }
        }
    }
}
