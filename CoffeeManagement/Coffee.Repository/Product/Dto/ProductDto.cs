using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Application.Product.Dto
{
    public class ProductDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public string CategoryName { get; set; }
        public bool SaleType { get; set; }
        public decimal Value { get; set; }
        public bool IsCombo { get; set; }
        public bool IsTop { get; set; }
        public long Status { get; set; }
        public string StatusName { get; set; }
    }

    public class ProductCreateDto
    {
        public long Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public string Image { get; set; }
        public string Description { get; set; }
        public long CategoryId { get; set; }
        public bool IsCombo { get; set; }
        public bool IsTop { get; set; }
        public long Status { get; set; }
        public List<ProductComboDto> ProductCombo { get; set; }
        public List<ProductPriceDto> ProductPrice { get; set; }
    }

    public class ProductComboDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ProductRefId { get; set; }
    }

    public class ProductPriceDto
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public decimal Price { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
    }

}
