using System.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using WebApplication1.Data.dao.Order;

namespace WebApplication1.Controller;

[ApiController]
[Route("api/orders")]
public class OrderController : ControllerBase
{
    private DbContext _context;

    public OrderController(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Endpoint giving access to all orders in system.  
    /// </summary>
    /// <param name="from">Get orders on page with specified start index </param>
    /// <param name="to">Get orders on page with specified end index</param>
    /// <param name="pageSize">Count of orders per page. Max value is 300</param>
    /// <param name="pageIndex">Current page index</param>
    /// <returns>Array of orders</returns>
    [HttpGet("")]
    public IActionResult GetOrders(int? from, int? to, int pageSize = 100, int pageIndex = 1)
    {
        if (pageSize > 300)
            return BadRequest("page size is more than max value");
        var currentPage = pageSize * pageIndex;

        var orders = _context.Orders.ToList().GetRange(currentPage, pageSize);

        if (to != null)
        {
            if (to > orders.Count - 1)
                return BadRequest(
                    $"\"To\" index is greater than orders count in page(last order index: {(orders.Count - 1).ToString()})");

            orders.RemoveRange((int)to - 1, orders.Count - ((int)to - 1));
        }

        if (from != null)
        {
            if (from < 0)
                return BadRequest("\"From\" index is less than 0");
            orders.RemoveRange(0, (int)from - 1);
        }

        var orderIds = orders.Select(order => order.Id).ToList();

        return Ok(JsonConvert.SerializeObject(orderIds));
    }

    /// <summary>
    /// 
    /// </summary>
    /// <returns>Current count of all orders</returns>
    [HttpGet("count")]
    public IActionResult GetAllOrdersCount() => Ok(JsonConvert.SerializeObject(_context.Orders.Count()));


    /// <summary>
    /// Gets order by it's Id. 
    /// </summary>
    /// <param name="orderId">Id of order to get</param>
    /// <returns>Order object</returns>
    [HttpGet("{orderId}")]
    public async Task<IActionResult> GetOrder(ulong orderId)
    {
        var order = await _context.Orders.Include(order => order.Statuses).Include(order => order.Materials)
            .FirstOrDefaultAsync(order => order.Id == orderId);
        if (order == null)
            return NotFound("Order not found");
        var response = new
        {
            Client = order.ClientId,
            CurrentStatus = order.Statuses.Last(),
            PerformingSupplier = order.SupplierId,
            OrderedMaterials = order.Materials.Select(product => product.Id),
            OrderExparation = order.ExpirationTime
        };
        return Ok(JsonConvert.SerializeObject(response));
    }

    [HttpGet("submitted")]
    public IActionResult GetSubmittedOrders()
    {
        var orders = _context.Orders.Where(order => order.Statuses.Last().Name.ToLower() == "submitted");
        var ids = orders.Select(order => order.Id);

        return Ok(JsonConvert.SerializeObject(ids.ToList()));
    }

    [HttpGet("{orderId}/price")]
    public async Task<IActionResult> GetOrderPrice(ulong orderId)
    {
        var order = await _context.Orders.Include(order => order.Materials).FirstOrDefaultAsync(order => order.Id == orderId);
        if (order == null)
            return NotFound("Order not found");

        throw new NotImplementedException();
    }
    
    [HttpPost("{orderId}/set/status={name}")]
    public async Task<IActionResult> SetOrderStatus(ulong orderId, string name)
    {
        var order = await _context.Orders.Include(order => order.Statuses)
            .FirstOrDefaultAsync(order => order.Id == orderId);
        if (order == null)
            return NotFound("Order not found");

        throw new NotImplementedException();
    }

    [HttpGet("client/{clientId}/all")]
    public IActionResult GetAllClientOrders(string clientId, string? withStatus)
    {
        var orders = _context.Orders.Where(order => order.ClientId == clientId);
        if (withStatus != null)
            orders = orders
                .Where(
                    order => order.Statuses
                        .Select(status => status.Name.ToLower())
                        .ToList()
                        .Last() == withStatus
                );
        var orderIds = orders.ToList().Select(order => order.Id);
        return Ok(JsonConvert.SerializeObject(orderIds));
    }
}