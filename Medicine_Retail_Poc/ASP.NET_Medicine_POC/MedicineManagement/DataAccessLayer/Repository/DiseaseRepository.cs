using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.Extensions.Logging;

namespace DataAccessLayer.Repository
{

    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<DiseaseRepository> _logger;

        public DiseaseRepository(ApplicationDbContext context,ILogger<DiseaseRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <summary>
        /// Add a new disease to the database.
        /// </summary>
        /// <param name="disease">The disease to add.</param>
        public void Add(Disease disease)
        {
            try
            {
                _context.Diseases.Add(disease);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                _logger.LogError($"Error adding disease: {ex.Message}");
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while adding the disease to the database.", ex);
            }
        }

        /// <summary>
        /// Get all diseases from the database.
        /// </summary>
        /// <returns>A list of all diseases.</returns>
        public IEnumerable<Disease> GetAllDiseases()
        {
            try
            {
                return _context.Diseases.ToList();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                _logger.LogError($"Error fetching all diseases: {ex.Message}");
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while fetching all diseases from the database.", ex);
            }
        }

        /// <summary>
        /// Save changes to the database.
        /// </summary>
        public void Save()
        {
            try
            {
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                _logger.LogError($"Error saving changes to the database: {ex.Message}");
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }
    }
}
