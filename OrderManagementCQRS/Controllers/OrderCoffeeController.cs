using Microsoft.AspNetCore.Mvc;
using OrderManagementBusiness.RequestModels.CommandRequestModels;
using OrderManagementBusiness.Interfaces.ICommandHandlers;
using OrderManagementBusiness.Interfaces.IQueryHandlers;
using OrderManagementBusiness.RequestModels.QueryRequestModels;

namespace OrderManagementCQRS.Controllers;

[ApiController]
[Route("[controller]/[action]")]
public class OrderLaptopController : ControllerBase
{

    private readonly ICreateNewOrder _createNewOrder;
    private readonly IGetOrderById _getOrderById;
    private readonly ILogger _logger;

    public OrderLaptopController(ICreateNewOrder createNewOrder, IGetOrderById getOrderById, ILogger<OrderLaptopController> logger)
    {
        _createNewOrder = createNewOrder;
        _getOrderById = getOrderById;
        _logger = logger;
        _logger.LogInformation("OrderLaptopController visited at {Time}", DateTime.Now);
    } // end constructor

    [HttpPost]
    public ActionResult CreateNewOrder(NewOrderRequest m)
    {
        _logger.LogDebug("Order Created Successfully");
        if (m.ProductId != 0)
        {
            var result = _createNewOrder.CreateNewOrder(m);
            _logger.LogDebug("Order Created Successfully");
            return result is not null ? Ok(new JsonResult(result) { StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK }) : Ok(new JsonResult(null) { StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent });
        }
        else
        {
            return Ok("please check input parameters!");
        }
    }

    [HttpGet]
    public ActionResult GetOrderById(OrderRequest m)
    {
        _logger.LogDebug("Ordessssr Created Successfully");
        if (m.OrderId != 0)
        {
            var result = _getOrderById.GetOrderById(m);
            _logger.LogDebug("Order Retrieved Successfully");
            return result is not null ? Ok(new JsonResult(result) { StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status200OK }) : Ok(new JsonResult(null) { StatusCode = Microsoft.AspNetCore.Http.StatusCodes.Status204NoContent });
        }
        else
        {
            return Ok("please check input parameters!");
        }
    }
}