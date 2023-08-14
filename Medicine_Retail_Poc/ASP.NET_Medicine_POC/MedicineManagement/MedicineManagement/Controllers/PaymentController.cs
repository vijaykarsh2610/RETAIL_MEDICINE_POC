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

        public PaymentController(IPaymentService service,IMedicineService medicineService, IWebHostEnvironment environment,ApplicationDbContext dbContext)
        {
            _service = service;
            _environment = environment;
            _medicineService = medicineService;
            _context = dbContext;
        }
        public IActionResult PaymentVerification(int id)
        {
            // Find the medicine in the database
            var medicine = _medicineService.GetMedicineById(id);

            if (medicine == null)
            {
                return NotFound();
            }

            // Generate a random string for the payment verification
            var paymentVerification = _service.GeneratePaymentVerification();

            // Save the payment verification to the database
            _service.AddPayment(medicine.Id, paymentVerification);

            // Pass the payment verification to the view
            ViewBag.PaymentVerification = paymentVerification;

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult VerifyPayment(string paymentVerification)
        {
            // Find the payment in the database
            var payment = _context.Payments.FirstOrDefault(p => p.PaymentVerification == paymentVerification);

            if (payment != null)
            {
                // Payment successful
                _context.Payments.Remove(payment);
                _context.SaveChanges();

                // Pass a flag to the view to indicate payment success
                ViewBag.PaymentSuccess = true;

                return View("PaymentVerification");
            }
            else
            {
                ViewBag.Paymentfailed = true;
                // Payment failed
                return View("PaymentVerification");
            }
        }
    }
}
