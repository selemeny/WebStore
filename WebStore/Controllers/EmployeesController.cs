using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employees;

        public EmployeesController(IEmployeesData employees) => _employees = employees;


        //[Route("employees")]
        public IActionResult Index() => View(_employees.GetAll());

        //[Route("employee/{Id}")]
        public IActionResult Details(int id)
        {
            var employee = _employees.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}