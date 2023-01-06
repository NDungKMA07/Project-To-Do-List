using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_To_Do_List.Models;
using BC = BCrypt.Net.BCrypt;

using Microsoft.AspNetCore.Mvc.Filters;
using Project_To_Do_List.Areas.Admin.Attributes;

namespace Project_To_Do_List.Areas.Admin.Controllers
{

    [Area("Admin")]
    
    public class AccountController : Controller
    {
        DbConnect db = new DbConnect();

    
        public IActionResult Login()
        {

            return View("Login");
        }
        [HttpPost]

        public IActionResult LoginPost(IFormCollection fc)
        {

            string _email = fc["email_Login"];
            string _password = fc["password_Login"];

            UserRecord record = db.UserRecords.Where(tbl => tbl.Email == _email).FirstOrDefault();

            if (record != null)
            {
                if(BC.Verify(_password,record.Password) == true)
                {
                    string UserJson = JsonConvert.SerializeObject(record);
                    HttpContext.Session.SetString("User", UserJson);
                    HttpContext.Session.SetString("Email_login", _email);
                    return Redirect("/Todo/Index");
                }

               
            }


            return Redirect("/Admin/Account/Login?notify=invalid");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("Email_login");
            return Redirect("Login");
        }
    }
}
