using DataAccessLayer.Data;
using DataAccessLayer.Domain;

namespace DataAccessLayer.Repository
{

    public class DiseaseRepository : IDiseaseRepository
    {
        private readonly ApplicationDbContext _context;

        public DiseaseRepository(ApplicationDbContext context)
        {
            _context = context;
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
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while saving changes to the database.", ex);
            }
        }
    }
}
