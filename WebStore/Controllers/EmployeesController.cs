using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        //[Route("employees")]
        public IActionResult Index() => View(TestData.Employees);

        //[Route("employee/{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = TestData.Employees.FirstOrDefault(x => x.Id == Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}