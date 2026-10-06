using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Tanjente_Midterm_Store.Data;
using Tanjente_Midterm_Store.Models;

namespace Tanjente_Midterm_Store.Controllers
{
    public class CartController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CartController(ApplicationDbContext context)
        {
            _context = context;
        }

        // POST: Cart/AddToCart
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AddToCart(int id)
        {
            var product = await _context.Products.FindAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            var cartItem = new CartItem
            {
                ProductId = product.Id,
                ProductName = product.Name,
                Price = product.Price,
                Quantity = 1
            };

            _context.CartItems.Add(cartItem);

            await _context.SaveChangesAsync();

            return RedirectToAction("Index", "Products");
        }

        // GET: Cart
        public async Task<IActionResult> Index()
        {
            var cartItems = await _context.CartItems
                .ToListAsync();

            return View(cartItems);
        }

        // POST: Cart/UpdateQuantity
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateQuantity(int id, int quantity)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            if (quantity < 1)
            {
                quantity = 1;
            }

            cartItem.Quantity = quantity;

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        // POST: Cart/Remove
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Remove(int id)
        {
            var cartItem = await _context.CartItems.FindAsync(id);

            if (cartItem == null)
            {
                return NotFound();
            }

            _context.CartItems.Remove(cartItem);

            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}