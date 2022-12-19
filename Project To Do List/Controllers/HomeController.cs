using Microsoft.AspNetCore.Mvc;
using System;

namespace Project_To_Do_List.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Redirect("/Todo/Index");
        }
    }
}
