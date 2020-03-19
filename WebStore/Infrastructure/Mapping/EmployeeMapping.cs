using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebStore.Models;
using WebStore.ViewModels;

namespace WebStore.Infrastructure.Mapping
{
    public static class EmployeeMapping
    {
        public static EmployeeViewModel ToView(this Employee x) => new EmployeeViewModel
        {
            Id = x.Id,
            Name = x.FirstName,
            SecondName = x.SurName,
            Patronymic = x.Patronymic,
            Age = x.Age
        };


        public static Employee FromView(this EmployeeViewModel employee) => new Employee
        {
            Id = employee.Id,
            FirstName = employee.Name,
            SurName = employee.SecondName,
            Patronymic = employee.Patronymic,
            Age = employee.Age
        };
    }
}
