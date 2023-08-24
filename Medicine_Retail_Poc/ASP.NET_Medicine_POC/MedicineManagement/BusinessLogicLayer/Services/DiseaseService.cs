using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer.Services
{
    public class DiseaseService : IDiseaseService
    {
        private readonly IDiseaseRepository _repository;

        private readonly ILogger<DiseaseRepository> _logger;
        public DiseaseService(IDiseaseRepository repository, ILogger<DiseaseRepository> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public IEnumerable<Disease> GetDiseases()
        {
            try
            {
                return _repository.GetAllDiseases();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                _logger.LogError($"Error fetching all diseases: {ex.Message}");
                throw new Exception("An error occurred while fetching diseases from the database.", ex);
            }
        }

        /// <summary>
        /// Get the list of available disease categories.
        /// </summary>
        /// <returns>A list of disease categories.</returns>
        public List<string> GetDiseaseCategories()
        {
            return Disease.DiseaseCategories;
        }

        /// <summary>
        /// Add a new disease to the database.
        /// </summary>
        /// <param name="disease">The disease to add.</param>
        public void AddDisease(Disease disease)
        {
            try
            {
                _repository.Add(disease);
                _repository.Save();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                _logger.LogError($"Error adding disease: {ex.Message}");
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while adding the disease to the database.", ex);
            }
        }
    }
}
