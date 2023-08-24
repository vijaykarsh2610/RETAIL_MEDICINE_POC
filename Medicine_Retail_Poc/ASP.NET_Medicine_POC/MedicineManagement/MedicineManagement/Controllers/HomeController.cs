using BusinessLogicLayer.Services;
using MedicineManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataAccessLayer.Domain;

namespace MedicineManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDiseaseService _service;
        private readonly IMedicineService _medicineservice;
        private readonly ILogger<HomeController> _logger;
        public HomeController(ILogger<HomeController> logger,IDiseaseService service = null, IMedicineService medicineservice = null)
        {
            _service = service;
            _medicineservice = medicineservice;
            _logger = logger;
        }

        public IActionResult Index()
        {
            try
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
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while getting diseases: {ex.Message}");
                _logger.LogError($"An error occurred while getting diseases: {ex.Message}");
                // Return an error view
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult MedicineDetails(string category)
        {
            try
            {
                if (string.IsNullOrEmpty(category))
                {
                    // Handle the case when no category is provided
                    return RedirectToAction("Index", "Home");
                }

                var medicinesByCategory = _medicineservice.GetMedicinesByCategory(category);
                ViewBag.Category = category; // Set the ViewBag.Category for the view
                return View(medicinesByCategory);
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while getting medicines by category: {ex.Message}");
                _logger.LogError($"An error occurred while getting medicines by category: {ex.Message}");
                // Return an error view
                return View("Error");
            }
        }

        [HttpGet]
        public IActionResult MedicineDisplay(int medicineId)
        {
            try
            {
                var selectedMedicine = _medicineservice.GetMedicineById(medicineId);

                if (selectedMedicine == null)
                {
                    // Handle the case when the medicine is not found
                    return NotFound();
                }

                return View(selectedMedicine); // Return the view with selected medicine details
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while getting medicine by ID: {ex.Message}");
                _logger.LogError($"An error occurred while getting medicine by ID: {ex.Message}");
                // Return an error view
                return View("Error");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            try
            {
                return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while displaying the error view: {ex.Message}");
                _logger.LogError($"An error occurred while displaying the error view: {ex.Message}");
                // Return a plain text error message to the user
                return Content("An error occurred while displaying the error view. Please try again later.");
            }
        }
    }
}