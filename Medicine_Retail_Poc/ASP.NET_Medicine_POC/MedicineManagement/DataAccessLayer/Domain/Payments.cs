﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domain
{
    public class Payments
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string? PaymentVerification { get; set; }
    }
}
