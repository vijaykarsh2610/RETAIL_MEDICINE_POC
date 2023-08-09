using DataAccessLayer.Domain;

namespace BusinessLogicLayer.Services
{
    public interface IMedicineService
    {
        List<string> GetDiseaseCategories();

        void AddMedicine(Medicine medicine);
    }
}
