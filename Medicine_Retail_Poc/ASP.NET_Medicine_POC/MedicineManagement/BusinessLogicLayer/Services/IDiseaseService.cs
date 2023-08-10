using DataAccessLayer.Domain;
using DataAccessLayer.Data;
using Microsoft.EntityFrameworkCore;

namespace BusinessLogicLayer.Services
{
    public interface IDiseaseService
    {
        List<string> GetDiseaseCategories();
        void AddDisease(Disease disease);

        IEnumerable<Disease> GetDiseases();


    }

}