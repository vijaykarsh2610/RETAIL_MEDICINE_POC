using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public class CartRepository: ICartRepository
    {
        private readonly List<AddToCart> _items;
        private readonly ApplicationDbContext _context;

        public CartRepository(ApplicationDbContext applicationDb)
        {
            _items = new List<AddToCart>();
            _context = applicationDb;

        }

        public void AddItem(AddToCart item)
        {
            var existingItem = _items.FirstOrDefault(i => i.MedicineId == item.MedicineId);
            if (existingItem != null)
            {
                existingItem.Quantity += item.Quantity;
            }
            else
            {
                //item.Id = _items.Count + 1;
                //_items.Add(item);
                _context.AddToCart.Add(item);
                _context.SaveChanges();
            }
        }

        public void RemoveItem(AddToCart item)
        {
            var _item = _items.FirstOrDefault(i => i.Id == item.Id);
            if (item != null)
            {
                _context.Remove(item);
                _context.SaveChanges();
            }
        }

        public void Clear()
        {
            var items = _context.AddToCart.ToList();
            _context.RemoveRange(items);
            _context.SaveChanges();
        }

        public IEnumerable<AddToCart> GetItems()
        {
            //get all items from database in AddToCart table and return them
            return _context.AddToCart.ToList();
        }
    }
}
