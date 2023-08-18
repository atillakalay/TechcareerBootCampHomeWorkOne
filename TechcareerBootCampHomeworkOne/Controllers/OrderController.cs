using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TechcareerBootCampHomeworkOne.Models;

public class OrderController : Controller
{
    private readonly NorthwindContext _northwindContext;

    public OrderController(NorthwindContext context)
    {
        _northwindContext = context;
    }

    public IActionResult Products(int orderId)
    {
        var order = _northwindContext.Orders
            .Include(o => o.OrderDetails)
                .ThenInclude(od => od.Product)
            .FirstOrDefault(o => o.OrderId == orderId);

        if (order == null)
        {
            return NotFound();
        }

        return View(order);
    }
}
