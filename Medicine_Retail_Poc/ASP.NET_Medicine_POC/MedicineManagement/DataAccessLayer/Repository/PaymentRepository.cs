using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.Extensions.Logging;


namespace DataAccessLayer.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<PaymentRepository> _logger;
        public PaymentRepository(ApplicationDbContext context,ILogger<PaymentRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public void AddPayment(Payments payment)
        {
            try
            {
                _context.Payments.Add(payment);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error adding payment: {ex.Message}");
                Console.WriteLine($"Error adding payment: {ex.Message}");
            }
        }
    }
}
