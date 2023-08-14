using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            _context.Payments.Add(payment);
            _context.SaveChanges();
        }
    }
}
