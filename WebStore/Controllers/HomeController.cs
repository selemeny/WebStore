using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace WebStore.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return Content("Hello controller - action Index");
        }

        public IActionResult SomeAction()
        {
            return Content("Hello controller - action SomeAction");
        }
    }
}