using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Users : BaseEntity
    {
        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string FullName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(512)")]
        public string UserName { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(1024)")]
        public string HashedPassword { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Phone { get; set; }

        [Required]
        [Column(TypeName = "nvarchar(256)")]
        public string Email { get; set; }
        public DateTime? Birthday { get; set; }
        public string? Identity { get; set; }
        public string? Address { get; set; }
        public bool IsActive { get; set; }
        public int? LoginFailCount { get; set; }
        public string? ForgotCode { get; set; }
        public DateTime? ExpriedForgotCode { get; set; }
        public string? RefreshToken { get; set; }
        public DateTime? ExpriedRefreshToken { get; set; }
    }
}
