using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using MvcApp.Context;
using MvcApp.Models;
using MvcApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace MvcApp.Controllers
{
    public class HomeController : Controller
    {
        PhoneContext _db;
        private static int a = 0;
        private readonly IWebHostEnvironment _app;
        public HomeController(PhoneContext db, IWebHostEnvironment app)
        {
            _db = db;
            _app = app;
        }

        public IActionResult Index()
        {
            return View("Index1");
        }
        [Route("Filter/Index1")]
        public IActionResult Index(int? id)
        {
            var phones = _db.Phones.ToList();
            var orderes = _db.Orders.ToList();

            var compModels = phones
                .Select(c => new CompanyModel { Id = c.Id, Name = c.Company })
                .ToList();
            // добавляем на первое место
            compModels.Insert(0, new CompanyModel { Id = 0, Name = "Все" });

            IndexViewModel ivm = new IndexViewModel { Companies = compModels, Phones = phones };

            // если передан id компании, фильтруем список
            if (id != null && id > 0)
                ivm.Phones = phones.Where(p => p.Id == id);
            return View("Index1");

        }
        [HttpGet]
        public IActionResult Buy(int id)
        {
            a = id;
            ViewBag.PhoneId = id;
            return View();
        }

        [HttpPost]
        public string Buy(Order order)
        {
            _db.Orders.Add(order);

            _db.SaveChanges();

            //var l = _db.Orders.OrderByDescending(i => i.OrderId).FirstOrDefault();
            //var p = _db.Phones.FirstOrDefault(i => i.Id == l.PhoneId);

            //var id = Phone.SqlQuery("select p.name from Phones as p where p.id=2");
            return $"Thanks for shopping";
        }

        public VirtualFileResult GetFile()
        {
            //string path = Path.Combine(_app.ContentRootPath, "Files/text.pdf");
            //string s = "application/pdf";
            //return File(path, s, "text.pdf");

            var filepath = Path.Combine("~/Files", "text.pdf");
            return File(filepath, "text/plain", "text.pdf");
        }
    }
}
