using ADOIntegrationWithMVC.Models;
using System.Collections.Generic;
using System.Web.Mvc;

namespace ADOIntegrationWithMVC.Controllers
{
    public class HomeController : Controller
    {
        RehanDBDataContext dbContext= new RehanDBDataContext();
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(Login model)
        {
            var IsExists = dbContext.CheckLogin(model);
            if (IsExists)
            {
                return RedirectToAction("EmpDetails", "Home");
            }
            else
            {
                ViewBag.Msg = "Invalid Username/Password";
                return View();
            }
            
        }
        public ActionResult EmpDetails()
        {
            List<Employee> employees = dbContext.GetEmployees();
            return View(employees);
        }
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Employee emp)
        {
            var isInserted = dbContext.AddEmployee(emp);
            bool check = dbContext.UpdateEmployee(emp);
            if (check == true)
            {
                TempData["InsertMessage"] = "";
                ModelState.Clear();
                return RedirectToAction("EmpDetails");
            }
            return View();
            //if (isInserted)
            //    return RedirectToAction("EmpDetails", "Home");
            //else
            //{
            //    ViewBag.msg = "Registration Failed";
            //    return View();
            //}
        }
        public ActionResult Details(int id)
        {
            var row = dbContext.Details(id);
            return View(row);
        }
        public ActionResult Edit(int id)
        {
            var row = dbContext.GetEmployees().Find(x=>x.EmpId==id);

            return View(row);
        }
        [HttpPost]
        public ActionResult Edit(int id , Employee emp)
        {
            if(ModelState.IsValid == true)
            {
                bool check = dbContext.UpdateEmployee(emp);
                if(check == true)
                {
                    TempData["UpdateMessage"] = "";
                    ModelState.Clear();
                    return RedirectToAction("EmpDetails");
                }
            }
            return View();
        }
        public ActionResult Delete(int id)
        {
            RehanDBDataContext dbContext = new RehanDBDataContext();
            var row = dbContext.GetEmployees().Find(model => model.EmpId == id);

            return View(row);
        }
        [HttpPost]
        public ActionResult Delete(int id , Employee emp)
        {
            RehanDBDataContext dbContext = new RehanDBDataContext();
            bool check = dbContext.DeleteEmployee(id);
            if (check == true)
            {
                TempData["DeleteMessage"] = "";
                return RedirectToAction("EmpDetails");
            }

            return View();
        }

    }
}