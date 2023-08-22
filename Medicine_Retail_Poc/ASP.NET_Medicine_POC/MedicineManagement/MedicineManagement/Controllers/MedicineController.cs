using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Humanizer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

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
            try
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
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while getting medicines by category: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult Index()
        {
            try
            {
                var categories = _service.GetDiseaseCategories();
                ViewBag.Categories = new SelectList(categories);
                return View();
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while getting disease categories: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult Index(Medicine model)
        {
            try
            {
                var existingMedicine = _service.GetMedicinesByCategory(model.disease_category)
                     .FirstOrDefault(m => m.medicine_name.Equals(model.medicine_name, StringComparison.OrdinalIgnoreCase));

                if (existingMedicine != null)
                {
                    ModelState.AddModelError("medicine_name", "A medicine with the same name already exists in the selected category.");
                    var categories = _service.GetDiseaseCategories();
                    ViewBag.Categories = new SelectList(categories);
                    return View("Index", model);
                }

                if (ModelState.IsValid)
                {
                    // Save the medicine and show success message
                    _service.AddMedicine(model);
                    TempData["MedicineMessage"] = "Data inserted successfully!";
                    return RedirectToAction("Index", "Medicine");
                }
                else
                {
                    TempData["MedicineMessage"] = "Please enter valid data.";
                    return View("Index", model); // Return to the same view with validation errors
                }
            }
            catch (Exception ex)
            {
                // Handle the exception as required

                TempData["MedicineMessage"] = "An error occurred while processing the request. Please try again later.";
                return RedirectToAction("Index", "Medicine");
            }
        }

        //add methods to update,delete medicines

        [HttpGet]
        public IActionResult Update(int id)
        {
            try
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
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while getting medicine by ID: {ex.Message}");

                // Return an error view
                return View("Error");
            }
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
                    TempData["MedicineMessage"] = "Data updated successfully!";
                    return RedirectToAction("Index", "Medicine");
                }
                else
                {
                    var categories = _service.GetDiseaseCategories();
                    ViewBag.Categories = new SelectList(categories);
                    TempData["MedicineMessage"] = "Please enter valid data.";
                    return View(model);
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", "An error occurred while processing the request. Please try again later.");
                var categories = _service.GetDiseaseCategories();
                ViewBag.Categories = new SelectList(categories);
                TempData["MedicineMessage"] = "An error occurred while processing the request. Please try again later.";
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
