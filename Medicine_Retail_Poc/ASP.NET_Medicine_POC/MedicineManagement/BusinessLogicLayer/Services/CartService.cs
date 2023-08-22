using DataAccessLayer.Domain;
using DataAccessLayer.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public class CartService: ICartService
    {
        private readonly ICartRepository _repository;

        public CartService(ICartRepository repository)
        {
            _repository = repository;
        }

        public void AddItemToCart(Medicine medicine, int quantity)
        {
            try
            {
                var item = new AddToCart
                {
                    MedicineId = medicine.Id,
                    MedicineName = medicine.medicine_name,
                    BrandName = medicine.brand_name,
                    Category = medicine.disease_category,
                    Quantity = quantity,
                    Weight = medicine.weight,
                    TotalCost = medicine.cost * quantity
                };

                _repository.AddItem(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while adding item to cart: {ex.Message}");
            }
        }

        public void RemoveItemFromCart(AddToCart item)
        {
            try
            {
                _repository.RemoveItem(item);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while removing item from cart: {ex.Message}");
            }
        }

        public void ClearCart()
        {
            try
            {
                _repository.Clear();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while clearing cart: {ex.Message}");
            }
        }

        public IEnumerable<AddToCart> GetCartItems()
        {
            try
            {
                return _repository.GetItems();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error occurred while getting cart items: {ex.Message}");
                return null;
            }
        }
    }
        
}
