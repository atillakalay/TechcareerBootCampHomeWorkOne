using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechcareerBootCampHomeworkOne.Models;

namespace TechcareerBootCampHomeworkOne.Controllers
{
    public class EmployeeController : Controller
    {
        private readonly NorthwindContext _northwindContext;

        public EmployeeController(NorthwindContext northwindContext)
        {
            _northwindContext = northwindContext;
        }

        public IActionResult Index()
        {
            var employees = _northwindContext.Employees.ToList();
            return View(employees);
        }

        public IActionResult Orders(int id)
        {
            var employee = _northwindContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            var orders = _northwindContext.Orders
                .Include(o => o.Customer)
                .Where(o => o.EmployeeId == id)
                .ToList();

            ViewBag.Employee = employee;
            return View(orders);
        }


        public IActionResult Profile(int id)
        {
            var employee = _northwindContext.Employees.FirstOrDefault(e => e.EmployeeId == id);
            if (employee == null)
            {
                return NotFound();
            }

            return View(employee);
        }

        [HttpPost]
        public IActionResult UpdateProfile(Employee updatedEmployee)
        {
            var existingEmployee = _northwindContext.Employees.FirstOrDefault(e => e.EmployeeId == updatedEmployee.EmployeeId);
            if (existingEmployee == null)
            {
                return NotFound();
            }

            existingEmployee.HomePhone = updatedEmployee.HomePhone;
            existingEmployee.Address = updatedEmployee.Address;

            _northwindContext.SaveChanges();

            return RedirectToAction("Profile", new { id = updatedEmployee.EmployeeId });
        }
    }
}
