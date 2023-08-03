using DataAccessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface IMedicineService
    {
        List<string> GetDiseaseCategories();

        void AddMedicine(Medicine medicine);
    }
}
