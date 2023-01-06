using Microsoft.AspNetCore.Mvc;
using Project_To_Do_List.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.Data.SqlClient;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Project_To_Do_List.Areas.Admin.Attributes;
using BC = BCrypt.Net.BCrypt;
using System.Security.Cryptography;

namespace Project_To_Do_List.Areas.Admin.Controllers
{
    [Area("Admin")]
    [CheckLogin]
    public class UserController : Controller
    {
        DbConnect db = new DbConnect();
      
        
        public IActionResult Index()
        {
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            int _StrCountTaskOn = db.ToDoList.Where(anhxa => anhxa.IdUser == _idUser && anhxa.Status).ToList().Count();
            int _StrCountTaskOff = db.ToDoList.Where(anhxa => anhxa.Status == false && anhxa.IdUser == _idUser).ToList().Count();
            int TotalTask = _StrCountTaskOn + _StrCountTaskOff;

            ViewBag.UserInfo = item;
            ViewBag.TotalTask  = TotalTask;
            ViewBag.CountTaskOn = _StrCountTaskOn;
            ViewBag.CountTaskOff = _StrCountTaskOff;
            return View("Index");
        }
        public IActionResult Creat()
        {
            return View("Creat");
        }
        [HttpPost]
        public IActionResult CreatPost(IFormCollection fc)
        {

            string _name = fc["name_Creat_Account"].ToString().Trim();
            string _email = fc["email_Creat_Account"].ToString().Trim();
            string _password = fc["password_Creat_Account"].ToString().Trim();
            _password = BC.HashPassword(_password);

            
            UserRecord user = new UserRecord();
            user.Name = _name;
            user.Email = _email;
            user.Password = _password;  
            db.UserRecords.Add(user);   
            db.SaveChanges();
            
            // su dung cmd cua sql
            /*
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("insert into Users(Name,Email, Password ) values (@paramName, @paramEmail, @paramPassword)", conn);
                cmd.Parameters.AddWithValue("@paramName", _name);
                cmd.Parameters.AddWithValue("@paramEmail", _email);
                cmd.Parameters.AddWithValue("@paramPassword", _password);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            */


            return Redirect("/Admin/Account/Login");
        }

        public IActionResult UpdateName()
        {

            return View();
        }
        [HttpPost]

        public IActionResult UpdateNamePost(IFormCollection fc)
        {
            string _name = fc["input_update_name"].ToString().Trim();
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            UserRecord record = db.UserRecords.Where(item => item.ID == _idUser).FirstOrDefault();
            if(record != null)
            {
                record.Name = _name;    
            }
            else
            {
                return RedirectToAction("UpdateName");
            }
            return RedirectToAction("Index");
        }
        public IActionResult UpdateEmail()
        {

            return View();
        }

        [HttpPost]

        public IActionResult UpdateEmailPost(IFormCollection fc)
        {
            string _email = fc["input_update_mail"];
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            UserRecord record = db.UserRecords.Where(item => item.ID == _idUser).FirstOrDefault();
            if (record != null)
            {
                record.Email = _email;
            }
            else
            {
                return RedirectToAction("UpdateEmail");
            }
            return RedirectToAction("Index");
        }
        public IActionResult UpdatePassword()
        {

            return View();
        }

        [HttpPost]

        public IActionResult UpdatePasswordPost(IFormCollection fc)
        {
            string _password = fc["input_update_pass"];
            _password = BC.HashPassword(_password);
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            UserRecord record = db.UserRecords.Where(item => item.ID == _idUser).FirstOrDefault();
            if (record != null)
            {
                record.Password = _password;
            }
            else
            {
                return RedirectToAction("UpdatePasword");
            }
            return RedirectToAction("Index");
        }

    }
}
