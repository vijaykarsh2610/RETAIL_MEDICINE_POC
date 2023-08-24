using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<PaymentService> _logger;
        public PaymentService(IPaymentRepository repository, ILogger<PaymentService> logger)
        {
            _repository = repository;
            _logger = logger;
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
