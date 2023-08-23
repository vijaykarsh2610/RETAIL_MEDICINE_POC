using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace DataAccessLayer.Domain
{
    public class Medicine
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Medicine name is required")]
        [StringLength(30, ErrorMessage = "Brand name must be between 5 and 30 characters long.")]
        public string? medicine_name { get; set; }

        [Required(ErrorMessage = "Brand name is required")]
        [StringLength(30, ErrorMessage = "Brand name must be between 5 and 30 characters long.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Brand name must contain only letters.")]
        public string? brand_name { get; set; }

        [Required(ErrorMessage = "Disease category is required")]
        public string? disease_category { get; set; }

        [Required(ErrorMessage = "Cost is required")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Cost must be greater than 0")]
        public float cost { get; set; }

        [Required(ErrorMessage = "Manufacturing date is required")]
        [PastDate(ErrorMessage = "Manufacturing date cannot be a future date")]
        public DateTime mfd { get; set; }

        [Required(ErrorMessage = "Expiry date is required")]
        [FutureDate(ErrorMessage = "Expiry date must be a future date")]
        public DateTime Expd { get; set; }

        [Required(ErrorMessage = "Weight is required")]
        public string? weight { get; set; }

        [Required(ErrorMessage = "Composition is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Composition must be between 5 and 30 characters long.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Composition must contain only letters.")]
        public string? composition { get; set; }

        [Required(ErrorMessage = "Formulation type is required")]
        [StringLength(30, MinimumLength = 5, ErrorMessage = "Formulation type be between 5 and 30 characters long.")]
        [RegularExpression(@"^[a-zA-Z ]*$", ErrorMessage = "Formulation type must contain only letters.")]
        public string? formulation_type { get; set; }

        [NotMapped]
        public IFormFile ImageFile { get; set; }

        // Property to store the file path after saving
        public string? ImagePath { get; set; }
    }

    public class PastDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = Convert.ToDateTime(value);

            if (date <= DateTime.Today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }

    public class FutureDateAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            DateTime date = Convert.ToDateTime(value);

            if (date >= DateTime.Today)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult(ErrorMessage);
            }
        }
    }
}
