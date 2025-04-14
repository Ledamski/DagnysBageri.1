using System.IO.Compression;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MormorDagnysBageri.api.Data;
using MormorDagnysBageri.api.Entities;
using MormorDagnysBageri.api.ViewModels;

namespace MormorDagnysBageri.api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OrderController(DataContext context) : ControllerBase
{
    private readonly DataContext _context = context;

    [HttpGet()]
    public async Task<ActionResult> ListAllOrders()
    {
        var order = await _context.Products
        .Include(c => c.SupplierItems)
        .Select(order => new
        {
            OrderNumber = order.ProductId,
            order.ProductName,
            order.ItemNumber,
            order.Quantity,
            LineSum = order.PricePerKg * order.Quantity
        })
        .ToListAsync();

        return Ok(new { success = true, StatusCode = 200, data = order });
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> FndAllOrders(int id)
    {
        var order = await _context.Products
        .Where(o => o.ProductId == id)
        .Include(c => c.SupplierItems)
        .Select(order => new
        {
            OrderNumber = order.ProductId,
            order.ProductName,
            order.ItemNumber,
            order.Quantity,
            LineSum = order.PricePerKg * order.Quantity
        })
        .SingleOrDefaultAsync();

        if (order is null)
        {
            return NotFound(new { success = false, StatusCode = 404, data = order });
        }
        return Ok(new { success = true, StatusCode = 200, data = order });
    }
    [HttpPost()]
    public async Task<ActionResult> AddProduct(ProductOrderViewModel order)
    {
        var neworder = new Supplier
        {
            Product = order.Product,
            SupplierItems = []
        };

        foreach (var product in order.Products)
        {
            var prod = await _context.Products.SingleOrDefaultAsync(p => p.ProductId == product.ProductId);
            if (prod is null) return BadRequest($"We couldnt find an order with order number, try again!");
            var item = new SupplierItem
            {
                PricePerKg = product.PricePerKg,
                Quantity = product.Quantity,
                ProductId = product.ProductId
            };
            neworder.SupplierItems.Add(item);


        }
        try
        {
            await _context.Suppliers.AddAsync(neworder);
            await _context.SaveChangesAsync();
            return Ok();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
            return BadRequest($"Sorry we couldnt find any order with order number, try again!");
        }

    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateOrder(int id, ProductOrderViewModel order)
    {
        var orderToUpdate = await _context.Products
        .Where(c => c.ProductId == id)
        .Include(o => o.SupplierItems)
        .SingleOrDefaultAsync();

        if (orderToUpdate is null) return BadRequest($"Sorry we couldnt find any order with order number: {id}, please try again");

        foreach (var item in order.Products)
        {
            foreach (var supplierItem in orderToUpdate.SupplierItems)
            {
                supplierItem.ProductName = item.ProductName;
                supplierItem.Quantity = item.Quantity;
                supplierItem.PricePerKg = item.PricePerKg;
            }
        }

        await _context.SaveChangesAsync();
        return NoContent();
    }
}