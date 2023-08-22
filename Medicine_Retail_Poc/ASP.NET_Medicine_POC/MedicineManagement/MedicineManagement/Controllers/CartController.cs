using BusinessLogicLayer.Services;
using DataAccessLayer.Domain;
using Microsoft.AspNetCore.Mvc;

namespace MedicineManagement.Controllers
{
    public class CartController : Controller
    {
        private readonly IMedicineService _service;
        private readonly ICartService _cart;

        public CartController(IMedicineService service, ICartService cart)
        {
            _service = service;
            _cart = cart;
        }

        // ...
        // add a get method for AddToCart action which returns AddToCart view with medicine object

        [HttpGet]
        public IActionResult AddToCart()
        {
            try
            {
                var items = _cart.GetCartItems();
                var cartItems = new List<AddToCart>();
                float totalSum = 0; // Initialize the total sum to zero
                var cartItemIds = new List<int>(); // List to store cart item ids

                foreach (var item in items)
                {
                    var cartItem = new AddToCart
                    {
                        MedicineName = item.MedicineName,
                        BrandName = item.BrandName,
                        Category = item.Category,
                        Quantity = item.Quantity,
                        Weight = item.Weight,
                        TotalCost = item.TotalCost,
                        Id = item.Id,
                        MedicineId = item.MedicineId
                    };
                    cartItems.Add(cartItem);

                    totalSum += item.TotalCost; // Accumulate the total sum
                    cartItemIds.Add(item.Id); // Add cart item id to the list
                }

                ViewBag.TotalSum = totalSum; // Pass the total sum to the view
                ViewBag.CartItemIds = cartItemIds; // Pass the list of cart item ids to the view
                return View(cartItems);
            }
            catch (Exception ex)
            {
                // Log the exception
                Console.WriteLine("An error occurred while getting cart items");

                // Return an error view
                return View("Error");
            }
        }




        [HttpPost]
        public IActionResult AddToCart(int id, int quantity)
        {
            try
            {
                var medicine = _service.GetMedicineById(id);
                if (medicine == null)
                {
                    return NotFound();
                }

                _cart.AddItemToCart(medicine, quantity);

                return RedirectToAction("AddToCart", "Cart");
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while adding item to cart: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult RemoveFromCart(int id)
        {
            try
            {
                var medicine = _cart.GetCartItems().FirstOrDefault(i => i.Id == id);
                if (medicine == null)
                {
                    return NotFound();
                }

                _cart.RemoveItemFromCart(medicine);

                return RedirectToAction("AddToCart", "Cart");
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while removing item from cart: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }

        [HttpPost]
        public IActionResult ClearCart()
        {
            try
            {
                _cart.ClearCart();

                return RedirectToAction("AddToCart", "Cart");
            }
            catch (Exception ex)
            {
                // Print the error message to the console
                Console.WriteLine($"An error occurred while clearing the cart: {ex.Message}");

                // Return an error view
                return View("Error");
            }
        }
    }
}
