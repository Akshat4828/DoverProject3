using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phase3.Models;

namespace Phase3.Controllers
{
    public class AdminAccountController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Login(AdminModel admin)
        {
            if (admin.UserName=="admin" && admin.Password=="admin")
            {
                return RedirectToAction("Dashboard", "AdminAccount");
            }

            else
                ViewBag.msg = "Invalid Credentials";
            return View();
        }
        public IActionResult Dashboard()
        { 
            
            ViewBag.msg = $"Admin  Dashboard";
           
           
            return View();
        }
    }
}
