using Microsoft.AspNetCore.Mvc;
using Project_To_Do_List.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Data.SqlClient;
using System.Collections.Generic;
using X.PagedList;

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Newtonsoft.Json;

namespace Project_To_Do_List.Controllers
{
    public class TodoController : Controller
    {
        DbConnect db = new DbConnect();
        string strConnection = "Server = NGUYENDUNG\\SQLEXPRESS;Database = ToDoListNew; UID = sa; Password = 123; Trust Server Certificate = true";




       // public IActionResult Index(int? page)
        public IActionResult Index()
        {
            //int _currentPage = page ?? 1;
          //  int _RecordPerPage = 5;
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;

            var TaskOnList = db.ToDoList.Where(item => item.Status == true && item.IdUser == _idUser).ToList();
            var TaskOffList = db.ToDoList.Where(item => item.Status == false && item.IdUser == _idUser).ToList();
           
            HttpContext.Session.SetString("countTaskOn", TaskOnList.Count.ToString());
            HttpContext.Session.SetString("countTaskOff", TaskOffList.Count.ToString());
            var resultOn = TaskOnList;
            var resultOff = TaskOffList;

            if (TempData["checkASC"] != null)
            {
                 resultOn = TaskOnList.OrderBy(item => item.Text).ToList();
                 resultOff = TaskOnList.OrderBy(item => item.Text).ToList();

            }
            if (TempData["checkDSC"] != null)
            {
                 resultOn = TaskOnList.OrderByDescending(item => item.Text).ToList();
                 resultOff = TaskOnList.OrderByDescending(item => item.Text).ToList();

            }
           // var _RecordPage = TaskOnList.Count > TaskOffList.Count ? TaskOnList : TaskOffList;
            ViewBag.StateOn = resultOn;
            ViewBag.resultOff = resultOff;
          //  ViewBag.recordPage = _RecordPage.ToPagedList(_currentPage, _RecordPerPage);
            ViewBag.UserName = item.Name;
          
            return View("Index");
            
            
        }

    

        public IActionResult OnState(int? id)
        {

              bool _state = true;
           
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("update Content set Status = @state where ID = @stt", conn);
                cmd.Parameters.AddWithValue("@state", _state.ToString());
                cmd.Parameters.AddWithValue("@stt", id.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
        public IActionResult OffState(int? id)
        {

            bool _state = false;

            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("update Content set Status = @state where ID = @stt", conn);
                cmd.Parameters.AddWithValue("@state", _state.ToString());
                cmd.Parameters.AddWithValue("@stt", id.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Creat()
        {

            return View("Creat");  
        }
        [HttpPost]

        public IActionResult Creat(IFormCollection fc)
        {
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            string str = fc["inputTask"].ToString().Trim();

            bool state = false;
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("insert into Content(Text,Status,IdUser) values(@txtToDo,@state,@idUser)", conn);
                cmd.Parameters.AddWithValue("@txtToDo", str);
                cmd.Parameters.AddWithValue("@state", state.ToString());
                cmd.Parameters.AddWithValue("@idUser", _idUser);
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id) {
          

                using (SqlConnection conn = new SqlConnection(strConnection))
             {
                 SqlCommand cmd = new SqlCommand("delete from  Content where ID = @stt ", conn);
                 cmd.Parameters.AddWithValue("@stt", id.ToString());

                 conn.Open();
                 cmd.ExecuteNonQuery();
             }
             return RedirectToAction("Index");
            

        }

        public IActionResult sortASC() {

            TempData["checkASC"] = 1;
            return RedirectToAction("Index");
        }
        public IActionResult sortDSC()
        {
            TempData["checkDSC"] = 2;
            return RedirectToAction("Index");
        }


    }
}
