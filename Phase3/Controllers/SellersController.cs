using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Phase3.Models;

namespace Phase3.Controllers
{
    public class SellersController : Controller
    {
        private readonly AppDbContext _context;

        public SellersController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Sellers
        public IActionResult Index(string name)
        {
            return View(_context.Laptops.Where(x=>x.SellerName == name));
        }

        // GET: Sellers/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        // GET: Sellers/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Sellers/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Title,Description,Price,ImageUrl,NoOfSales,SellerName")] LaptopModel laptopModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(laptopModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(laptopModel);
        }

        // GET: Sellers/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops.FindAsync(id);
            if (laptopModel == null)
            {
                return NotFound();
            }
            return View(laptopModel);
        }

        // POST: Sellers/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Description,Price,ImageUrl,NoOfSales,SellerName")] LaptopModel laptopModel)
        {
            if (id != laptopModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(laptopModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LaptopModelExists(laptopModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(laptopModel);
        }

        // GET: Sellers/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var laptopModel = await _context.Laptops
                .FirstOrDefaultAsync(m => m.Id == id);
            if (laptopModel == null)
            {
                return NotFound();
            }

            return View(laptopModel);
        }

        // POST: Sellers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var laptopModel = await _context.Laptops.FindAsync(id);
            _context.Laptops.Remove(laptopModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public IActionResult CreateSeller()
        {
            return View();
        }

        // POST: CustomersController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult CreateSeller(SellerModel s)
        {
            try
            {
                _context.Add(s);
                _context.SaveChanges();

                ViewBag.msg = "User Created";
                return RedirectToAction("login");
            }
            catch
            {
                return View();
            }
        }
        public IActionResult Login()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Login(CustomerLoginViewModel c)
        {
            var seller = _context.Sellers.Single(x => x.Email == c.Email);
            if (c.Password == seller.Password)
            {
                return RedirectToAction("index",new { name = c.Email });
            }
            else
                ViewBag.msg = "Invalid Credentials";

            return View();
        }    

        private bool LaptopModelExists(int id)
        {
            return _context.Laptops.Any(e => e.Id == id);
        }
    }
}
