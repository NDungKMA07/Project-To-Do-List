﻿using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Project_To_Do_List.Models;


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

                if (record.Password == _password)
                {

                    string UserJson = JsonConvert.SerializeObject(record);
                    HttpContext.Session.SetString("User", UserJson);
                    return Redirect("/Todo/Index");
                }
            }
            else
            {
                return RedirectToAction("Login");
            }

            return RedirectToAction("Login");
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Remove("User");
            return Redirect("Login");
        }
    }
}
