using DataAccessLayer.Domain;

namespace BusinessLogicLayer.Services
{
    public interface IMedicineService
    {
        List<string> GetDiseaseCategories();

        void AddMedicine(Medicine medicine);

        Medicine GetMedicineById(int medicineId);

        IEnumerable<Medicine> GetMedicinesByCategory(string category);
    }
}
