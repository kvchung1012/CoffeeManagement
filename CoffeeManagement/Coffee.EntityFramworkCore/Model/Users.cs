using Coffee.EntityFramworkCore.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore.Model
{
    public class Users : BaseEntity
    {
        public string FullName { get; set; }
        public string UserName { get; set; }
        public string HashedPassword { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime Birthday { get; set; }
        public string Identity { get; set; }
        public string Address { get; set; }
        public bool IsActive { get; set; }
        public int LoginFailCount { get; set; }
        public string ForgotCode { get; set; }
        public DateTime ExpriedForgotCode { get; set; }
        public string RefreshToken { get; set; }
        public DateTime ExpriedRefreshToken { get; set; }
    }
}
