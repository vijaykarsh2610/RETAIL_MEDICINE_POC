using DataAccessLayer.Data;
using DataAccessLayer.Domain;


namespace DataAccessLayer.Repository
{
    public class PaymentRepository : IPaymentRepository
    {
        private readonly ApplicationDbContext _context;

        public PaymentRepository(ApplicationDbContext context)
        {
            _context = context;
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
                Console.WriteLine($"Error adding payment: {ex.Message}");
            }
        }
    }
}
