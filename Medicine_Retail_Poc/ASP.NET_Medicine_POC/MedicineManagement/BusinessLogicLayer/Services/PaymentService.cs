using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class PaymentService : IPaymentService
    {
        private readonly IPaymentRepository _repository;

        public PaymentService(IPaymentRepository repository)
        {
            _repository = repository;
        }

        public string GeneratePaymentVerification()
        {
            return Guid.NewGuid().ToString();
        }

        public void AddPayment(int medicineId, string paymentVerification)
        {
            var payment = new Payments
            {
                MedicineId = medicineId,
                PaymentVerification = paymentVerification
            };
            _repository.AddPayment(payment);
        }
    }
}
