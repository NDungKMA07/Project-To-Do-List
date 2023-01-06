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
using Project_To_Do_List.Areas.Admin.Attributes;
using System.Security.Cryptography;

namespace Project_To_Do_List.Controllers
{
    [CheckLogin]

    public class TodoController : Controller
    {
        DbConnect db = new DbConnect();

        
      
        public IActionResult Index(int? page)
        {

          int _currentPage = page ?? 1;
           int _RecordPerPage = 3;
            string StrUserRecord = HttpContext.Session.GetString("User");
            UserRecord item = JsonConvert.DeserializeObject<UserRecord>(StrUserRecord);
            int _idUser = item.ID;
            var TaskOnList = db.ToDoList.Where(anhxa => anhxa.IdUser == _idUser && anhxa.Status).OrderByDescending(anhxa => anhxa.ID).ToList();
            var TaskOffList = db.ToDoList.Where(anhxa => anhxa.Status == false && anhxa.IdUser == _idUser).OrderByDescending(anhxa => anhxa.ID).ToList();


            if (TempData["sortASC"] != null)
            {
                TaskOnList = db.ToDoList.Where(anhxa => anhxa.IdUser == _idUser && anhxa.Status).OrderBy(anhxa => anhxa.Text).ToList();
                TaskOffList = db.ToDoList.Where(anhxa => anhxa.Status == false && anhxa.IdUser == _idUser).OrderBy(anhxa => anhxa.Text).ToList();
               
            }
            if (TempData["sortDSC"] != null)
            {
                TaskOnList = db.ToDoList.Where(anhxa => anhxa.IdUser == _idUser && anhxa.Status).OrderByDescending(anhxa => anhxa.Text).ToList();
                TaskOffList = db.ToDoList.Where(anhxa => anhxa.Status == false && anhxa.IdUser == _idUser).OrderByDescending(anhxa => anhxa.Text).ToList();
               
            }
            ViewBag.StateOn = TaskOnList.ToPagedList(_currentPage, _RecordPerPage);
            ViewBag.resultOff = TaskOffList.ToPagedList(_currentPage, _RecordPerPage);
            ViewBag.UserName = item.Name;
            return View("Index");
        }

    

        public IActionResult OnState(int? id)
        {
            bool _state = true;

            ItemToDo record = db.ToDoList.Where(anhxa => anhxa.ID == id).FirstOrDefault();
            if (record != null)
            {
                record.Status = _state;

            }
            db.SaveChanges();


          
            return RedirectToAction("Index");
        }
        public IActionResult OffState(int? id)
        {
            bool _state = false;

            ItemToDo record = db.ToDoList.Where(anhxa => anhxa.ID == id).FirstOrDefault();
            if (record != null)
            {
                record.Status = _state;

            }
            db.SaveChanges();
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
            ItemToDo record = new ItemToDo();
            record.Text = str;  
            record.IdUser = _idUser;
            record.Status = state;
            db.ToDoList.Add(record); 
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int? id) {
            
            ItemToDo record = db.ToDoList.Where(anhxa => anhxa.ID == id).FirstOrDefault();
            if (record != null) { 
                db.ToDoList.Remove(record);  
               
            }
            db.SaveChanges();
             return RedirectToAction("Index");
         
        }

        public IActionResult sortASC()
        {

            TempData["sortASC"] = 1;
            return RedirectToAction("Index");
        }
        public IActionResult sortDSC()
        {
            TempData["sortDSC"] = 2;
            return RedirectToAction("Index");
        }


    }
}
