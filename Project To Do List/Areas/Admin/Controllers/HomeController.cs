using Microsoft.AspNetCore.Mvc;
using Project_To_Do_List.Areas.Admin.Attributes;

namespace Project_To_Do_List.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckLogin]
    
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
