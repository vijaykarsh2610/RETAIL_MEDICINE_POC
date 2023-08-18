using DataAccessLayer.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.Repository
{
    public interface ICartRepository
    {
        void AddItem(AddToCart item);
        void RemoveItem(AddToCart item);
        void Clear();
        IEnumerable<AddToCart> GetItems();
    }
}
