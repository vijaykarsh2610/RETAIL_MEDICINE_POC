using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Domain
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        public string medicine_name { get; set; }
        
        // add brand_name,disease_category,cost,mfd,Expd,weight/capacity,image properties

        public string brand_name { get; set; }

        public string disease_category { get; set; }

        public float cost { get; set; }

        public DateTime mfd { get; set; }

        public DateTime Expd { get; set; }

        public float weight { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        // Property to store the file path after saving
        public string? ImagePath { get; set; }
    }
}
