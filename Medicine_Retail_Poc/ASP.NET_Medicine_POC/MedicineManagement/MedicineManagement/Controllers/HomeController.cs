using BusinessLogicLayer.Services;
using MedicineManagement.Models;
using MedicineManagement.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MedicineManagement.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IDiseaseService _service;

        public HomeController(ILogger<HomeController> logger, IDiseaseService service = null)
        {
            _logger = logger;
            _service = service;
        }

        public IActionResult Index()
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


            return View(diseases);
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