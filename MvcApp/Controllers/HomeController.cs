using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcApp.Context;
using MvcApp.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        PhoneContext _db;

        public HomeController(PhoneContext db)
        {
            _db = db;
        }

        public IActionResult Index()
        {
            return View(_db.Phones.ToList());
        }
        [HttpGet]
        public IActionResult Buy(int id)
        {
            ViewBag.PhoneId = id;
            return View();
        }
        [HttpPost]
        public string Buy(Order order)
        {
            _db.Orders.Add(order);

            _db.SaveChanges();

           
            return $"Thanks for shopping";
        }
    }
}
