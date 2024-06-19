using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MKTDotNetCore.PizzaApi.Db;

namespace MKTDotNetCore.PizzaApi.Feature
{
    [Route("api/[controller]")]
    [ApiController]
    public class PizzaController : ControllerBase
    {
        private readonly AppDbContext _db;

        public PizzaController()
        {
            _db = new AppDbContext();
        }

        [HttpGet]
        public async Task<IActionResult> GetPizzaData()
        {
            var lst = await _db.pizza.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("PizzaExtra")]
        public async Task<IActionResult> GetPizzaExtra()
        {
            var lst = await _db.pizzaExtra.ToListAsync();
            return Ok(lst);
        }

        [HttpGet("Order/{invoiceNo}")]
        public async Task<IActionResult> GetOrderDetail(string invoiceNo)
        {
            var item = await _db.pizzaOrder.FirstOrDefaultAsync(x=>x.PizzaOrderInvoiceCode == invoiceNo);
            var lst=await _db.pizzaDetail.Where(x=>invoiceNo.Contains(x.PizzaOrderInvoiceCode!)).ToListAsync();
            if (item is null || lst is null) return NotFound();
            return Ok(new OrderDetailResponseModel
            {
                pizzaOrder = item,
                lstPizzaOrderDetail= lst
            });
        }

        [HttpPost("Order")]
        public async Task<IActionResult> OrderPizza(OrderRequest orderRequest)
        {
            var item = await _db.pizza.FirstOrDefaultAsync(x => x.Id == orderRequest.PizzaId);
            if (item is null) return NotFound();
            var total = item.Price;
            if (orderRequest.Extra!.Length > 0)
            {
                var lstExtra = await _db.pizzaExtra.Where(x => orderRequest.Extra.Contains(x.Id)).ToListAsync();
                total += lstExtra.Sum(x => x.Price);
            }
            var InvoiceNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            PizzaOrderModel pizzaOrder = new PizzaOrderModel
            {
                PizzaId = orderRequest.PizzaId,
                PizzaOrderInvoiceCode = InvoiceNo,
                Total = total
            };
            List<PizzaOrderDetailModel> lstExtraModel = orderRequest.Extra.Select(extraId => new PizzaOrderDetailModel
            {
                PizzaExtraId = extraId,
                PizzaOrderInvoiceCode = InvoiceNo
            }).ToList();
            await _db.pizzaOrder.AddAsync(pizzaOrder);
            await _db.pizzaDetail.AddRangeAsync(lstExtraModel);
            await _db.SaveChangesAsync();
            return Ok(new OrderResponse
            {
                InvoiceNo = InvoiceNo,
                messaage = "Thank you for ordering !",
                TotalAmount = total
            });
        }
    }
}
