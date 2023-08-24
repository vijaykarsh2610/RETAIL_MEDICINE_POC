using DataAccessLayer.Data;
using DataAccessLayer.Domain;
using Microsoft.Extensions.Logging;
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
        private readonly ILogger<CartRepository> _logger;

        public CartRepository(ApplicationDbContext applicationDb,ILogger<CartRepository> logger)
        {
            _items = new List<AddToCart>();
            _context = applicationDb;
            _logger = logger;

        }

        public void AddItem(AddToCart item)
        {
            try
            {
                var existingItem = _items.FirstOrDefault(i => i.MedicineId == item.MedicineId);
                if (existingItem != null)
                {
                    existingItem.Quantity += item.Quantity;
                }
                else
                {
                    _context.AddToCart.Add(item);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while adding item to cart: {ex.Message}");
                Console.WriteLine($"Error occurred while adding item to cart: {ex.Message}");
            }
        }

        public void RemoveItem(AddToCart item)
        {
            try
            {
                var _item = _items.FirstOrDefault(i => i.Id == item.Id);
                if (item != null)
                {
                    _context.Remove(item);
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while removing item from cart: {ex.Message}");
                Console.WriteLine($"Error occurred while removing item from cart: {ex.Message}");
            }
        }

        public void Clear()
        {
            try
            {
                var items = _context.AddToCart.ToList();
                _context.RemoveRange(items);
                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while clearing cart: {ex.Message}");
                Console.WriteLine($"Error occurred while clearing cart: {ex.Message}");
            }
        }

        public IEnumerable<AddToCart> GetItems()
        {
            try
            {
                //get all items from database in AddToCart table and return them
                return _context.AddToCart.ToList();
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error occurred while getting items from cart: {ex.Message}");
                Console.WriteLine($"Error occurred while getting items from cart: {ex.Message}");
                return null;
            }
        }
    }
}
