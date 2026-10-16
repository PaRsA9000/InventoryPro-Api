using Api.Dtos;
using Data.Context;
using Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrdersController : ControllerBase
{
    private readonly AppDbContext _context;

    public OrdersController(AppDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var orders = await _context.Orders
            .Include(o => o.Items)
            .Where(o => !o.IsDeleted)
            .ToListAsync();
        return Ok(orders);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder([FromBody] CreateOrderDto dto)
    {
        if (dto.Items == null || !dto.Items.Any())
            return BadRequest("لیست سفارش نباید خالی باشد.");

        var order = new Order
        {
            CustomerName = dto.CustomerName,
            Status = OrderStatus.Pending,
            CreatedAt = DateTime.UtcNow
        };

        decimal total = 0;

        foreach (var item in dto.Items)
        {
            var product = await _context.Products.FindAsync(item.ProductId);
            if (product == null)
                return NotFound($"محصول با شناسه {item.ProductId} یافت نشد.");

            if (product.StockQuantity < item.Quantity)
                return BadRequest($"موجودی محصول '{product.Name}' کافی نیست. موجودی فعلی: {product.StockQuantity}");

            product.StockQuantity -= item.Quantity;

            var orderItem = new OrderItem
            {
                ProductId = product.Id,
                Quantity = item.Quantity,
                UnitPrice = product.Price,
                CreatedAt = DateTime.UtcNow
            };

            total += orderItem.UnitPrice * orderItem.Quantity;
            order.Items.Add(orderItem);
        }

        order.TotalAmount = total;
        _context.Orders.Add(order);
        await _context.SaveChangesAsync();

        return Ok(new { message = "سفارش با موفقیت ثبت و انبار بروزرسانی شد.", orderId = order.Id, totalAmount = order.TotalAmount });
    }
}
