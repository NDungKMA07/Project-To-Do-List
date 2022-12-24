using Microsoft.AspNetCore.Mvc;
using Project_To_Do_List.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.Data.SqlClient;

using Newtonsoft;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Project_To_Do_List.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class UserController : Controller
    {
        DbConnect db = new DbConnect();
        string strConnection = "Server = NGUYENDUNG\\SQLEXPRESS;Database = ToDoListNew; UID = sa; Password = 123; Trust Server Certificate = true";

        public IActionResult Index()
        {
            string _StrCountTaskOn = HttpContext.Session.GetString("countTaskOn");
            string _StrCountTaskOff = HttpContext.Session.GetString("countTaskOff");
            int TotalTask = Int32.Parse(_StrCountTaskOn) + Int32.Parse(_StrCountTaskOff);

            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);

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

            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("insert into Users(Name,Email, Password ) values (@paramName, @paramEmail, @paramPassword)", conn);
                cmd.Parameters.AddWithValue("@paramName", _name);
                cmd.Parameters.AddWithValue("@paramEmail", _email);
                cmd.Parameters.AddWithValue("@paramPassword", _password);
                conn.Open();
                cmd.ExecuteNonQuery();
            }


            return Redirect("/Admin/Account/Login");
        }

        public IActionResult UpdateName()
        {

            return View();
        }
        [HttpPost]

        public IActionResult UpdateNamePost(IFormCollection fc)
        {
            string _name = fc["input_update_name"];
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            if (!string.IsNullOrEmpty(_name))
            {
                using (SqlConnection conn = new SqlConnection(strConnection))
                {
                    SqlCommand cmd = new SqlCommand("update Users set Name = @paramName where ID = @IdUser", conn);
                    cmd.Parameters.AddWithValue("@paramName", _name);
                    cmd.Parameters.AddWithValue("@IdUser", _idUser);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
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
            if (!string.IsNullOrEmpty(_email))
            {
                using (SqlConnection conn = new SqlConnection(strConnection))
                {
                    SqlCommand cmd = new SqlCommand("update Users set Email = @paramEmail where ID = @IdUser ", conn);
                    cmd.Parameters.AddWithValue("@paramEmail", _email);
                    cmd.Parameters.AddWithValue("@IdUser", _idUser);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
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
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            if (!string.IsNullOrEmpty(_password))
            {
                using (SqlConnection conn = new SqlConnection(strConnection))
                {
                    SqlCommand cmd = new SqlCommand("update Users set Password = @paramPassword where ID = @IdUser ", conn);
                    cmd.Parameters.AddWithValue("@paramPassword", _password);
                    cmd.Parameters.AddWithValue("@IdUser", _idUser);
                    conn.Open();
                    cmd.ExecuteNonQuery();
                }
            }
            else
            {
                return RedirectToAction("UpdatePasword");
            }
            return RedirectToAction("Index");
        }

    }
}
