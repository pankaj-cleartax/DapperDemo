using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using System.Data.SqlClient;
using DapperDemo.Models;
using MySql.Data.MySqlClient;

namespace DapperDemo.Controllers
{
    public class EmployeeController : Controller
    {
        private IDbConnection  GetConnection()
        {
            return new MySqlConnection(ConfigurationManager.ConnectionStrings["ConnectionStringToUse"].ConnectionString);
        }

        // GET: Employee
        public ActionResult Index()
        {
            using (IDbConnection db = GetConnection())
            {
                var employees = db.Query<EmpModel>("select * from `dbo.employee`");
                return View(employees);
            }
        }

        // GET: Employee/Details/5
        public ActionResult Details(int id)
        {
            using (var conn = GetConnection())
            {
                var model = conn.Query<EmpModel>("select * from `dbo.employee` where Id=@Id", new { Id = id }).FirstOrDefault();
                return View(model);
            }
        }

        // GET: Employee/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Employee/Create
        [HttpPost]
        public ActionResult Create(EmpModel model)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var transaction = conn.BeginTransaction();
                    conn.Execute(@"Insert into `dbo.employee` values(@EmpId, @Name, @City, @Address)",
                        new { Empid = model.Id, model.Name, model.City, model.Address }, transaction);
                    transaction.Commit();
                    conn.Close();
                    return RedirectToAction("Index");
                }
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Edit/5
        public ActionResult Edit(int id)
        {
            using (var conn = GetConnection())
            {
                var model = conn.Query<EmpModel>("select * from `dbo.employee` where Id=@Id", new { Id = id }).FirstOrDefault();
                return View(model);
            }
        }

        // POST: Employee/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, EmpModel model)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var transaction = conn.BeginTransaction();
                    conn.Execute(@"Update `dbo.employee` set Id = @Id,
                                    Name = @Name,
                                    City = @City,
                                    Address = @Address where id=@Id",
                                    model, transaction:transaction);
                    transaction.Commit();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Employee/Delete/5
        public ActionResult Delete(int id)
        {
            using (var conn = GetConnection())
            {
                var model = conn.Query<EmpModel>("select * from `dbo.employee` where Id=@Id", new { Id = id }).FirstOrDefault();
                return View(model);
            }
        }

        // POST: Employee/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, EmpModel model)
        {
            try
            {
                using (var conn = GetConnection())
                {
                    conn.Open();
                    var transaction = conn.BeginTransaction();
                    conn.Execute(@"Delete from `dbo.employee` where id=@Id", model, transaction:transaction);
                    transaction.Commit();
                    conn.Close();
                }
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
