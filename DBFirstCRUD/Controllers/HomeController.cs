using DBFirstCRUD.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBFirstCRUD.Controllers
{
    public class HomeController : Controller
    {
        Entities context = new Entities();

        public ActionResult Search(string searchTerm)
        {
            List<Employee> employees = new List<Employee>();
            employees = context.Employees.Where(e => e.Name.Contains(searchTerm)).ToList();
            return View(employees);
        }
        public ActionResult Index(int page = 1)
        {
            int pageSize = 10;

            List<Employee> employees = new List<Employee>();
            employees = context.Employees.OrderBy(e => e.Id).Skip((page-1)*pageSize).Take(pageSize).ToList();

            ViewBag.TotalPages = Math.Ceiling((double)context.Employees.Count() / pageSize);
            ViewBag.CurrentPage = page;

            return View(employees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            context.Employees.Add(emp);
            context.SaveChanges();
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Update(int id)
        {
            var employee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Update(Employee updateEmp)
        {
            var employee = context.Employees.Where(e => e.Id == updateEmp.Id).FirstOrDefault();
            if (employee != null)
            {
                employee.Name = updateEmp.Name;
                employee.DOB = updateEmp.DOB;
                employee.Age = updateEmp.Age;
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }

        public ActionResult Delete(int id)
        {
            var employee = context.Employees.Where(e => e.Id == id).FirstOrDefault();
            return View(employee);
        }

        [HttpPost]
        public ActionResult Delete(Employee updateEmp)
        {
            var employee = context.Employees.Where(e => e.Id == updateEmp.Id).FirstOrDefault();
            if (employee != null)
            {
                context.Employees.Remove(employee);
                context.SaveChanges();
            }
            return RedirectToAction("Index", "Home");
        }
    }
}