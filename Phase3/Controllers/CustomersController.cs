using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Phase3.Models;
using Microsoft.AspNetCore.Identity;

namespace Phase3.Controllers
{
    public class CustomersController : Controller
    {
       
        CustomerBO content = new CustomerBO();
        public AppDbContext _context;
        public CustomersController(AppDbContext context)
        {
            _context = context;
        }
        public IActionResult RedirectToLogin()
        {

            return View();
        }
        public IActionResult IndexForCustomer()
        {
            return View();
        }
            public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(CustomerLoginViewModel c)
        {
            var customer = _context.Customers.Single(x => x.Email == c.Email);
            if (c.Password == customer.Password)
            {
                return RedirectToAction(nameof(Dashboard));
            }
            else
                ViewBag.msg = "Invalid Credentials";

            return View();
        }
        public IActionResult Dashboard()
        {
            
            return View(_context.Laptops); 
        }
       

        public IActionResult Index()
        {
            List<CustomerModel> customers = new List<CustomerModel>();
            customers = content.GetAllCustomers();
            return View(customers);
        }
      

        // GET: CustomersController/Details/5
        public IActionResult Details(int id)
        {
            CustomerModel c = content.GetAllCustomersById(id);
            return View(c);
        }

        // GET: CustomersController/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CustomerModel c)
        {
            try
            {
                content.AddCustomer(c);
                
                ViewBag.msg = "User Created";
                return RedirectToAction(nameof(Login));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Edit/5
        public ActionResult Edit(int id)
        {
            return View(content.GetAllCustomersById(id));
        }

        // POST: CustomersController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, CustomerModel c)
        {
            try
            {
                content.EditCustomer(c);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: CustomersController/Delete/5
        public ActionResult Delete(int id)
        {
            return View(content.GetAllCustomersById(id));
        }

        // POST: CustomersController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, CustomerModel c)
        {
            try
            {
                content.DeleteCustomer(c);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        public ActionResult LaptopDetails(int id)
        {
            var laptop = _context.Laptops.Single(x => x.Id == id);
            return View(laptop);
        }
        public IActionResult Payment(int id)
        {
            ViewBag.id = id;
            return View();
        }
        public async Task<IActionResult> Order(int id)
        {
            LaptopModel laptop = new LaptopModel();
            foreach (var lap in _context.Laptops.ToList())
            {
                if (lap.Id == id)
                {
                    laptop = lap;
                    break;
                }
            }
            laptop.NoOfSales = laptop.NoOfSales + 1;
            _context.Update(laptop);
            _context.SaveChanges();
            return View(laptop);
        }
    }
}
