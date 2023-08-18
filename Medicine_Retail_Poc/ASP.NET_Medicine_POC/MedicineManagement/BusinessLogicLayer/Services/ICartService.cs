using DataAccessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Services
{
    public interface ICartService
    {
        void AddItemToCart(Medicine medicine, int quantity);
        void RemoveItemFromCart(AddToCart item);
        void ClearCart();
        IEnumerable<AddToCart> GetCartItems();
    }
}
