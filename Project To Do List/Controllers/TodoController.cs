using Microsoft.AspNetCore.Mvc;
using Project_To_Do_List.Models;
using Microsoft.AspNetCore.Http;
using System.Data;
using System.Data.SqlClient;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Project_To_Do_List.Controllers
{
    public class TodoController : Controller
    {
        DbConnect db = new DbConnect();
        string strConnection = "Server = NGUYENDUNG\\SQLEXPRESS;Database = TodoList; UID = sa; Password = 123; Trust Server Certificate = true";

        
      

        public IActionResult Index()
        {
            var resultOn = db.ToDoList.Where(item => item.status == 1).ToList();
            var resultOff = db.ToDoList.Where(item => item.status == 0).ToList();
            if (TempData["checkASC"] != null)
            {
                resultOn = db.ToDoList.Where(item => item.status == 1).OrderBy(item => item.textToDo).ToList();
                resultOff = db.ToDoList.Where(item => item.status == 0).OrderBy(item => item.textToDo).ToList();

            }
            if (TempData["checkDSC"] != null)
            {
                resultOn = db.ToDoList.Where(item => item.status == 1).OrderByDescending(item => item.textToDo).ToList();
                resultOff = db.ToDoList.Where(item => item.status == 0).OrderByDescending(item => item.textToDo).ToList();

            }
            ViewBag.StateOn = resultOn;
            ViewBag.resultOff = resultOff;
           
            return View("Index");
            
            
        }

        public IActionResult OnState(int? id)
        {

              int _state = 1;
           
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("update TableToDo set status = @state where STT = @stt", conn);
                cmd.Parameters.AddWithValue("@state", _state.ToString());
                cmd.Parameters.AddWithValue("@stt", id.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }
        public IActionResult OffState(int? id)
        {

            int _state = 0;

            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("update TableToDo set status = @state where STT = @stt", conn);
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
           
            string str = fc["inputTask"].ToString().Trim();
            int state = 0;
            using (SqlConnection conn = new SqlConnection(strConnection))
            {
                SqlCommand cmd = new SqlCommand("insert into TableToDo(textToDo,status) values(@txtToDo,@state)", conn);
                cmd.Parameters.AddWithValue("@txtToDo", str);
                cmd.Parameters.AddWithValue("@state", state.ToString());
                conn.Open();
                cmd.ExecuteNonQuery();
            }
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id) {
          

                using (SqlConnection conn = new SqlConnection(strConnection))
             {
                 SqlCommand cmd = new SqlCommand("delete from  TableToDo where STT = @stt ", conn);
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
