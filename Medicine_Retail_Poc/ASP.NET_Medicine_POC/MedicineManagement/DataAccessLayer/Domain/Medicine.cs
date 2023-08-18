using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.Domain
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        public string medicine_name { get; set; }
        
        // add brand_name,disease_category,cost,mfd,Expd,weight/capacity,image properties

        public string? brand_name { get; set; }

        public string? disease_category { get; set; }

        public float cost { get; set; }

        public DateTime mfd { get; set; }

        public DateTime Expd { get; set; }

        public string? weight { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        // Property to store the file path after saving
        public string? ImagePath { get; set; }
    }
}
