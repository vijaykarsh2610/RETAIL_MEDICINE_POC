using DataAccessLayer.Domain;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public interface IDiseaseService
    {
        List<string> GetDiseaseCategories();
        void AddDisease(Disease disease);

        public IEnumerable<Disease> GetDiseases()
        {
            try
            {
                using (var context = new ApplicationDbContext(new DbContextOptionsBuilder<ApplicationDbContext>().UseSqlServer("Server=localhost\\SQLEXPRESS01;Database=MED_POC;TrustServerCertificate=True;Trusted_Connection=True;").Options))
                {
                    return context.Diseases.ToList();
                }
            }
            catch (Exception ex)
            {
                // Log error message and stack trace to a file or database.
                Console.WriteLine($"An error occurred while getting the diseases: {ex.Message}\nStackTrace: {ex.StackTrace}");
                throw;
            }
        }
    }

}