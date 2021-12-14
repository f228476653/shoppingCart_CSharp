using Microsoft.AspNetCore.Mvc;

namespace ShoppingCart.Api.Controllers
{
    public class CartController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
