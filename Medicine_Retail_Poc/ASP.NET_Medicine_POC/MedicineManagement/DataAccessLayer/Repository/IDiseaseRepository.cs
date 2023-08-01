using DataAccessLayer.Domain;

namespace DataAccessLayer.Repository
{
    public interface IDiseaseRepository
    {
        void Add(Disease disease);
        IEnumerable<Disease> GetAllDiseases();
        void Save();
    }
}
