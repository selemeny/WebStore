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
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employees) => _employeesData = employees;


        //[Route("employees")]
        public IActionResult Index() => View(_employeesData.GetAll());

        //[Route("employee/{Id}")]
        public IActionResult Details(int id)
        {
            var employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }


        public IActionResult Create()
        {
            return View(new Employee());
        }

        [HttpPost]
        public IActionResult Create(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (!ModelState.IsValid)
                return View(employee);

            _employeesData.Add(employee);
            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id is null) 
                return View(new Employee());
            if (id < 0)
                return BadRequest();

            var employee = _employeesData.GetById((int)id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }

        [HttpPost]
        public IActionResult Edit(Employee employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (!ModelState.IsValid)
                return View(employee);

            var id = employee.Id;
            if (id == 0)
                _employeesData.Add(employee);
            else
                _employeesData.Edit(id, employee);

            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}