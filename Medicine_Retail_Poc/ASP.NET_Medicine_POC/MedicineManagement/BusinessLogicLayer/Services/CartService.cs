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

        public void RemoveItemFromCart(AddToCart item)
        {
            _repository.RemoveItem(item);
        }

        public void ClearCart()
        {
            _repository.Clear();
        }

        public IEnumerable<AddToCart> GetCartItems()
        {
            return _repository.GetItems();
        }
    }
}
