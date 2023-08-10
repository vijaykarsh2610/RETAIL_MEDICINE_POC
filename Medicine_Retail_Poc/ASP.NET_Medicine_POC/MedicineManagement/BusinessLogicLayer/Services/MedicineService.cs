using DataAccessLayer.Domain;
using DataAccessLayer.Repository;


namespace BusinessLogicLayer.Services
{
    public class MedicineService : IMedicineService
    {
        private readonly IMedicineRepository _repository;
        // add methods to add,update,delete,search medicines

        public MedicineService(IMedicineRepository repository)
        {
            _repository = repository;
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
    }
}
