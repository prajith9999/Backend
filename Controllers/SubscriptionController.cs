using Microsoft.AspNetCore.Mvc;

namespace vueproject_asp.Controllers
{
    public class SubscriptionController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
