using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManagement.Controllers
{
    public class RegistrationController : Controller
    {
        private readonly IRegistrationService _service;

        //Constructor
        public RegistrationController(IRegistrationService service)
        {
            _service = service;
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
                // Check if the model is valid based on the data annotations in the Registration class
                if (!ModelState.IsValid)
                {
                    return View(model);
                }

                // Call the Create method of the registration service to create a new user
                var user = await _service.Create(model);

                // Redirect to the login page after successful user creation
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during user creation
               
                return View(model); // Return the view with the model if an error occurs
            }
        }
    }
}
