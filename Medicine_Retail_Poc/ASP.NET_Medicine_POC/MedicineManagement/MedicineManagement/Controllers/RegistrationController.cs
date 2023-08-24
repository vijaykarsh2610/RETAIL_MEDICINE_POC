using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace MedicineManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _service;
        private readonly ILogger<RegistrationController> _logger;

        //Constructor
        public RegistrationController(IRegistrationService service, ILogger<RegistrationController> logger)
        {
            _service = service;
            _logger = logger;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Registration model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    TempData["RegistrationMessage"] = "Please enter valid values.";
                    return View(model);
                }

                if( await _service.EmailExists(model.Email))
                {
                    TempData["RegistrationMessage"] = "Email already exists";
                    return View(model);
                }


                var user = await _service.Create(model);

                if (user != null)
                {
                    TempData["RegistrationSuccess"] = true;
                }
                else
                {
                    TempData["RegistrationMessage"] = "An error occurred during registration.";
                }

                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Handle exceptions
                _logger.LogError($"An error occurred while registering user: {ex.Message}");
                TempData["RegistrationMessage"] = "An error occurred.";
                return View(model);
            }
        }

    }
}
