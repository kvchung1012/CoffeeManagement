using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Order.Dto
{
    public class OrderDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public long CustomerId { get; set; }
        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public decimal TotalPrice { get; set; }
        public decimal ReceiveMoney { get; set; }
        public decimal ChangeMoney { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }

    public class ProductCart
    {
        public long ProductId { get; set; }
        public int Stock { get; set; }
    }

    public class CreateCart
    {
        public decimal TotalMoney { get; set; }
        public decimal ReceiveMoney { get; set; }
        public decimal ChangeMoney { get; set; }

        public string CustomerName { get; set; }
        public string CustomerPhone { get; set; }
        public string CustomerEmail { get; set; }

        public List<ProductCart> Products { get; set; }
    }

    public class OrderDetailDto
    {
        public long Id { get; set; }
        public long OrderId { get; set; }
        public long ProductId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public decimal PriceSale { get; set; }
        public int Stock { get; set; }
    }

    public class OrderFullDto : OrderDto
    {
        public string CustomerEmail { get; set; }
        public DateTime CreatedTime { get; set; }
        public long CreatedBy { get; set; }
        public string CreatedByName { get; set; }
        public List<OrderDetailDto> OrderDetail { get; set; }
    }
}
