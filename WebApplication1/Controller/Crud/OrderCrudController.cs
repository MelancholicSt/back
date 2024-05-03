using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao;
using WebApplication1.Data.Dto;

namespace WebApplication1.Controller;

[ApiController]

public class OrderCrudController : ControllerBase
{
    private DbContext _context;
    private UserManager<Account> _userManager;

    public OrderCrudController(DbContext context, UserManager<Account> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    [HttpPost("orders/{orderId}/add/product")]
    public IActionResult AddProduct([FromBody] ProductDto product, long orderId)
    {
        var order = _context.UserOrders.FirstOrDefault(x => x.OrderId == orderId);
        if(order is null)
            return BadRequest("Order not found");
        Product newProduct = new Product()
        {
            Name = product.Name,
            Description = product.Description,
            Measure = _context.Measures.FirstOrDefault(x => x.MeasureName == product.MeasureName),
        };
        order.Products.Add(newProduct);
        _context.SaveChanges();
        return Ok();
    }

    [HttpPut("orders/{orderId}/remove/product/{productId}")]
    public async Task<IActionResult> RemoveProduct(long orderId, long productId)
    {
        var order = _context.UserOrders.Include(x => x.Products).FirstOrDefault(x => x.OrderId == orderId);
        if (order == null)
            return BadRequest("Order not found");
        var product = order.Products.FirstOrDefault(x => x.Id == productId);
        if (product == null)
            return BadRequest("Product not found");
        order.Products.Remove(product);
        await _context.SaveChangesAsync();
        return Ok();
    }
/*
    [HttpPut("orders/{orderId}/change")]
    public async Task<IActionResult> EditProduct(long orderId, [FromBody] ClientOrder order)
    {
        var oldOrder = _context.UserOrders.FirstOrDefault(x => x.OrderId == orderId);
        if (oldOrder == null)
            return BadRequest("Order not found");
        oldOrder = order;
        await _context.SaveChangesAsync();
        return Ok();
    }*/
    [HttpGet("orders/get/all")]
    public IActionResult GetAll()
    {
        return Ok(JsonConvert.SerializeObject(_context.UserOrders.ToList()));
    }

    [HttpGet("orders/get/{orderId}")]
    public IActionResult GetOrder(long orderId)
    {
        var order = _context.UserOrders.Include(x => x.Products).FirstOrDefault(x => x.OrderId == orderId);
        if (order is null)
            return BadRequest("Order not found");
        return Ok(JsonConvert.SerializeObject(order));
    }
}