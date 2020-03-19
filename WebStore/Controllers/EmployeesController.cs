using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Data;
using WebStore.Infrastructure.Interfaces;
using WebStore.Models;
using WebStore.ViewModels;
using WebStore.Infrastructure.Mapping;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        private readonly IEmployeesData _employeesData;

        public EmployeesController(IEmployeesData employees) => _employeesData = employees;


        //[Route("employees")]
        public IActionResult Index() => View(_employeesData.GetAll().Select(x => x.ToView()));

        //[Route("employee/{Id}")]
        public IActionResult Details(int id)
        {
            var employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }


        public IActionResult Create()
        {
            return View(new EmployeeViewModel());
        }

        [HttpPost]
        public IActionResult Create(EmployeeViewModel employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(Employee));

            if (!ModelState.IsValid)
                return View(employee);

            _employeesData.Add(employee.FromView());
            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Edit(int? id)
        {
            if (id is null) 
                return View(new EmployeeViewModel());
            if (id < 0)
                return BadRequest();

            var employee = _employeesData.GetById((int)id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }

        [HttpPost]
        public IActionResult Edit(EmployeeViewModel employee)
        {
            if (employee is null)
                throw new ArgumentNullException(nameof(EmployeeViewModel));

            if (!ModelState.IsValid)
                return View(employee);

            if (employee.Name == "123" && employee.SecondName == "QWE")
                ModelState.AddModelError(string.Empty, "Странные имя и фамилия...");

            if (!ModelState.IsValid)
                return View(employee);

            var id = employee.Id;
            if (id == 0)
                _employeesData.Add(employee.FromView());
            else
                _employeesData.Edit(id, employee.FromView());

            _employeesData.SaveChanges();

            return RedirectToAction("Index");
        }


        public IActionResult Delete(int id)
        {
            if (id <= 0)
                return BadRequest();

            var employee = _employeesData.GetById(id);
            if (employee is null)
                return NotFound();

            return View(employee.ToView());
        }


        [HttpPost]
        public IActionResult DeleteConfirmed(int id)
        {
            _employeesData.Delete(id);
            _employeesData.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}