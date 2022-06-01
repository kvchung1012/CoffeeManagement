using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Order : BaseEntity
    {
        /// <summary>
        /// Mã hóa đơn
        /// </summary>
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Code { get; set; }

        /// <summary>
        /// Id của người mua, có thể NULL
        /// </summary>
        public long? UserId { get; set; }

        /// <summary>
        /// Tổng tiền hóa đơn
        /// </summary>
        /// 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal TotalPrice { get; set; }

        /// <summary>
        /// Tiền khách đưa
        /// </summary>
        /// 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ReceiveMoney { get; set; }

        /// <summary>
        /// Tiền thừa trả khách
        /// </summary>
        /// 
        [Column(TypeName = "decimal(18, 2)")]
        public decimal? ChangeMoney { get; set; }
    }
}
