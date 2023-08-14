using DataAccessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface IMedicineRepository
    {
        // add methods to add,update,delete,search medicines

        void Add(Medicine medicine);
        void Update(Medicine medicine);
        void Delete(Medicine medicine);

        Medicine GetMedicineById(int medicineId);
        IEnumerable<Medicine> GetAllMedicines();
        IEnumerable<Medicine> GetMedicinesByCategory(string category);
        List<string> GetDiseaseCategories();
        void Save();

    }
}
