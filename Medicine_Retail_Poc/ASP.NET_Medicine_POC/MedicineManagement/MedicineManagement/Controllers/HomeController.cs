using BusinessLogicLayer.Services;
using MedicineManagement.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using DataAccessLayer.Domain;

namespace MedicineManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDiseaseService _service;
        private readonly IMedicineService _medicineservice;

        public HomeController(ILogger<HomeController> logger, IDiseaseService service = null, IMedicineService medicineservice = null)
        {
            _logger = logger;
            _service = service;
            _medicineservice = medicineservice;
        }

        public IActionResult Index()
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
        public IActionResult MedicineDetails(string category)
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

        [HttpGet]
        public IActionResult MedicineDisplay(int medicineId)
        {
            var selectedMedicine = _medicineservice.GetMedicineById(medicineId);

            if (selectedMedicine == null)
            {
                // Handle the case when the medicine is not found
                return NotFound();
            }

            return View(selectedMedicine); // Return the view with selected medicine details
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}