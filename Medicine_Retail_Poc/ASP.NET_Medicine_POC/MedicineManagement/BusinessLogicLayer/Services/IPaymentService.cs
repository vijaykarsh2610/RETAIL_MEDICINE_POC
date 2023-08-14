﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IPaymentService
    {
        string GeneratePaymentVerification();
        void AddPayment(int medicineId, string paymentVerification);
    }
}
