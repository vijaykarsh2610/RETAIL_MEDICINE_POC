using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System.Diagnostics;

namespace MedicineManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly ILoginService _service;

        // controller constructor
        public LoginController(ILoginService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Index()
        {

            if (TempData["RegistrationSuccess"] != null)
            {
                ViewData["RegistrationSuccessMessage"] = "Registration Successful! You can now log in.";
                // Remove the registration success TempData to prevent it from being displayed again
                TempData.Remove("RegistrationSuccess");

            }

            // GET request for the login page
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(Login model)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // If the model state is not valid, return the view with the current model
                    return View(model);
                }

                // Authenticate the user using the ILoginService
                var user = await _service.Authenticate(model);

                if (user == null)
                {
                    // If authentication fails, add a model error and return the view with the current model
                    ModelState.AddModelError("Email", "Invalid email or password");
                    TempData["LoginMessage"] = "Invalid email or password.";
                    TempData["IsAdmin"] = false; // Set IsAdmin to false for invalid login
                    return View(model);
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name)
                };
                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                // Sign in the user using the claims identity
                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(identity));

                if (user.IsAdmin)
                {
                    TempData["LoginMessage"] = "Login Successful!";
                    TempData["IsAdmin"] = true;
                    // If the user is an admin, redirect to the home page for admins
                    return RedirectToAction("Create", "Disease");
                }
                else
                {
                    //TempData["LoginMessage"] = "Login Successful!";
                    //TempData["IsAdmin"] = false;
                    //return View(model);

                    // If the user is not an admin, redirect to the home page for regular users

                    return RedirectToAction("Index", "Home");


                }
            }
            catch (Exception ex)
            {
                // Handle any exceptions that may occur during the login process
                // You can log the error or perform any other actions here
                // For now, simply return the view with the current model
                TempData["LoginMessage"] = "An error occurred.";
                TempData["IsAdmin"] = false; // Set IsAdmin to false for error case
                return View(model);
            }
        }

    }
}
