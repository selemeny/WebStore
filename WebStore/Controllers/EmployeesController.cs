using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebStore.Models;

namespace WebStore.Controllers
{
    //[Route("users")]
    public class EmployeesController : Controller
    {
        static readonly List<Employee> _employees = new List<Employee>
        {
            new Employee
            {
                Id = 1,
                SurName = "Иванов",
                FirstName = "Иван",
                Patronymic = "Иванович",
                Age = 39
            },
            new Employee
            {
                Id = 2,
                SurName = "Петров",
                FirstName = "Петр",
                Patronymic = "Петрович",
                Age = 18
            },
            new Employee
            {
                Id = 3,
                SurName = "Сидоров",
                FirstName = "Сидор",
                Patronymic = "Сидорович",
                Age = 27
            }
        };


        //[Route("employees")]
        public IActionResult Index() => View(_employees);

        //[Route("employee/{Id}")]
        public IActionResult Details(int Id)
        {
            var employee = _employees.FirstOrDefault(x => x.Id == Id);
            if (employee is null)
                return NotFound();

            return View(employee);
        }
    }
}