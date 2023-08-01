using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Domain
{
    public class Disease
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Please select a Disease Category.")]
        public string DiseaseCategory { get; set; }

        // Property to store the file upload
        [NotMapped]
        public IFormFile ImageFile { get; set; }

        // Property to store the file path after saving
        public string? ImagePath { get; set; }

        public static List<string> DiseaseCategories = new List<string>
        {
            "Diabetes",
            "Cardiac",
            "Stomach",
            "Liver",
            "Kidney"
        };
    }
}

