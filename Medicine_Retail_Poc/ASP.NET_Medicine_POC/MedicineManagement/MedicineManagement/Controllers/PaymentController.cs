using BusinessLogicLayer.Services;
using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MedicineManagement.Controllers
{
    public class PaymentController : Controller
    {
        private readonly IPaymentService _service;

        private readonly IMedicineService _medicineService;

        private readonly IWebHostEnvironment _environment;

        private readonly ApplicationDbContext _context;

        private readonly ICartService _cart;

        public PaymentController(IPaymentService service,IMedicineService medicineService, IWebHostEnvironment environment,ApplicationDbContext dbContext,ICartService cartService)
        {
            _service = service;
            _environment = environment;
            _medicineService = medicineService;
            _context = dbContext;
            _cart = cartService;
        }
        public IActionResult PaymentVerification(string ids)
        {
            try
            {
                List<int> idList = new List<int>();

                if (!string.IsNullOrEmpty(ids))
                {
                    if (ids.Contains(","))
                    {
                        // Split the comma-separated IDs and parse them into integers
                        idList = ids.Split(',').Select(int.Parse).ToList();
                    }
                    else
                    {
                        // Parse the single ID into an integer and add to the list
                        idList.Add(int.Parse(ids));
                    }
                }

                // Find the medicines in the database using the list of IDs
                var items = _cart.GetCartItems().Where(m => idList.Contains(m.Id)).ToList();

                if (items.Count == 0)
                {
                    return NotFound();
                }

                // Generate a random string for the payment verification
                var paymentVerification = _service.GeneratePaymentVerification();

                foreach (var item in items)
                {
                    // Save the payment verification to the database for each item
                    _service.AddPayment(item.Id, paymentVerification);
                }

                // Pass the payment verification and items to the view
                ViewBag.PaymentVerification = paymentVerification;
                var itemIdsString = string.Join(",", items.Select(item => item.Id));
                ViewBag.ItemIdsString = itemIdsString;

                return View(items);
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while verifying payment: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyPayment(string paymentVerification, string ids)
        {
            try
            {
                List<int> idList = new List<int>();

                if (!string.IsNullOrEmpty(ids))
                {
                    if (ids.Contains(","))
                    {
                        // Split the comma-separated IDs and parse them into integers
                        idList = ids.Split(',').Select(int.Parse).ToList();
                    }
                    else
                    {
                        // Parse the single ID into an integer and add to the list
                        idList.Add(int.Parse(ids));
                    }
                }

                // Find the payment in the database
                var payment = _context.Payments.FirstOrDefault(p => p.PaymentVerification == paymentVerification);

                if (payment != null)
                {
                    // Payment successful
                    _context.Payments.RemoveRange(payment);
                    _context.SaveChanges();

                    foreach (var itemId in idList)
                    {
                        var item = _context.AddToCart.FirstOrDefault(i => i.Id == itemId);
                        _cart.RemoveItemFromCart(item);
                    }

                    // Pass a flag to the view to indicate payment success
                    ViewBag.PaymentSuccess = true;

                    return View("PaymentVerification");
                }
                else
                {
                    // Payment failed
                    ViewBag.PaymentFailed = true;
                    return View("PaymentVerification");
                }
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while verifying payment: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }

    }
}
