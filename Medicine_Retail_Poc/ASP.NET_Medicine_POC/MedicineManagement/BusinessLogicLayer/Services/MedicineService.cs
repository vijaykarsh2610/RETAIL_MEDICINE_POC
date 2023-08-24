using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using Microsoft.Extensions.Logging;

namespace BusinessLogicLayer.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;
        private readonly ILogger<MedicineService> _logger;
        // add methods to add,update,delete,search medicines

        public MedicineService(IMedicineRepository repository, ILogger<MedicineService> logger)
        {
            _repository = repository;
            _logger = logger;
        }
        public void AddMedicine(Medicine medicine)
        {
            try
            {
                _repository.Add(medicine);
                _repository.Save();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                _logger.LogError($"Error adding medicine: {ex.Message}");
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while adding the medicine to the database.", ex);
            }
        }
        public Medicine GetMedicineById(int medicineId)
        {
            return _repository.GetMedicineById(medicineId);
        }

        public IEnumerable<Medicine> GetMedicinesByCategory(string category)
        {
            return _repository.GetMedicinesByCategory(category);
        }

        public List<string> GetDiseaseCategories()
        {
            return _repository.GetDiseaseCategories();
        }

        // add methods to update,delete medicines

        public void UpdateMedicine(Medicine medicine)
        {
            try
            {
                _repository.Update(medicine);
                _repository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error updating medicine: {ex.Message}");
                throw new Exception("An error occurred while updating the medicine in the database.", ex);
            }
        }

        public void DeleteMedicine(Medicine medicine)
        {
            try
            {
                _repository.Delete(medicine);
                _repository.Save();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error deleting medicine: {ex.Message}");
                throw new Exception("An error occurred while deleting the medicine from the database.", ex);
            }
        }

        public void Save()
        {
            throw new NotImplementedException();
        }
    }
}
