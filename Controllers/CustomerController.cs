using Microsoft.AspNetCore.Mvc;
using CustomerManagementSystem.Data;
using CustomerManagementSystem.Models;

namespace CustomerManagementSystem.Controllers
{
    public class CustomerController : Controller
    {
        // Database context
        private readonly ApplicationDbContext _context;

        // Constructor
        public CustomerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: all customers
        public IActionResult Index()
        {
            var customers = _context.Customers.OrderBy(c => c.Name).ToList();
            return View(customers);
        }

        // GET: customer by id
        [HttpGet]
        public IActionResult GetCustomer(int id)
        {
            var customer = _context.Customers.Find(id);
            if (customer == null)
                return NotFound();
            return Json(customer);
        }

        // POST: create new customer
        [HttpPost]
        public IActionResult Create([FromForm] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Customers.Add(customer);
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return BadRequest(new { success = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: update existing customer
        [HttpPost]
        public IActionResult Update([FromForm] Customer customer)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var existing = _context.Customers.Find(customer.Id);
                    if (existing == null)
                        return NotFound();

                    existing.Name = customer.Name;
                    existing.Email = customer.Email;
                    existing.Phone = customer.Phone;
                    existing.Birthday = customer.Birthday;
                    
                    _context.SaveChanges();
                    return Json(new { success = true });
                }
                return BadRequest(new { success = false });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }

        // POST: delete customer by id
        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                var customer = _context.Customers.Find(id);
                if (customer == null)
                    return NotFound();

                _context.Customers.Remove(customer);
                _context.SaveChanges();
                return Json(new { success = true });
            }
            catch (Exception ex)
            {
                return BadRequest(new { success = false, message = ex.Message });
            }
        }
    }
}
