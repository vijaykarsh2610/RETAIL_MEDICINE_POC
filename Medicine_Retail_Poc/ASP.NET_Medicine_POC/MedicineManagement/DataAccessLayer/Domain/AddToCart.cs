using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domain
{
    public class AddToCart
    {
        public int Id { get; set; }
        public int MedicineId { get; set; }
        public string MedicineName { get; set; }
        public string BrandName { get; set; }
        public string Category { get; set; }
        public int Quantity { get; set; }
        public string Weight { get; set; }

        //add total cost 
        public float TotalCost { get; set; }
    }
}
