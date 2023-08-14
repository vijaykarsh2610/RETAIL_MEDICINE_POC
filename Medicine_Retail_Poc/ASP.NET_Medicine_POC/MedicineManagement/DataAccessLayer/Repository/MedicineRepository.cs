using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class MedicineRepository : IMedicineRepository
    {
        private readonly ApplicationDbContext _context;

        public MedicineRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // add methods to add,update,delete,search medicines

        public void Add(Medicine medicine)
        {
            try
            {
                _context.Medicines.Add(medicine);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                // Log or handle the exception as required
                // In a production application, you might want to log the error or notify developers/admins
                throw new Exception("An error occurred while adding the medicine to the database.", ex);
            }
        }

        public void Update(Medicine medicine)
        {
            try
            {
                _context.Medicines.Update(medicine);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while updating the medicine in the database.", ex);
            }
        }

        public void Delete(Medicine medicine)
        {
            try
            {
                _context.Medicines.Remove(medicine);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while deleting the medicine from the database.", ex);
            }
        }



        public IEnumerable<Medicine> GetAllMedicines()
        {
            try
            {
                return _context.Medicines.ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching all medicines from the database.", ex);
            }
        }

        public Medicine GetMedicineById(int id)
        {
            try
            {
                return _context.Medicines.FirstOrDefault(m => m.Id == id);
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the medicine from the database.", ex);
            }
        }

        public IEnumerable<Medicine> GetMedicinesByCategory(string category)
        {
            try
            {
                return _context.Medicines.Where(m => m.disease_category == category).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the medicines from the database.", ex);
            }
        }

        public IEnumerable<Medicine> GetMedicinesByBrand(string brand)
        {
            try
            {
                return _context.Medicines.Where(m => m.brand_name == brand).ToList();
            }
            catch (Exception ex)
            {
                throw new Exception("An error occurred while fetching the medicines from the database.", ex);
            }
        }

        public List<string> GetDiseaseCategories()
        {
            return Disease.DiseaseCategories;
        }


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
