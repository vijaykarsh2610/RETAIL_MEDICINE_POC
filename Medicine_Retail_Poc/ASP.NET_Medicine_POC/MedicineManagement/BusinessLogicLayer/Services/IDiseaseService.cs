using DataAccessLayer.Domain;

namespace BusinessLogicLayer.Services
{
    public interface IDiseaseService
    {
        List<string> GetDiseaseCategories();
        void AddDisease(Disease disease);
    }
}
