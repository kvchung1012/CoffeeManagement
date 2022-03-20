using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Auth
{
    public class JwtResultModel
    {
        public string AccessToken { get; set; }
        public int ExpriedIn { get => 60 * 60 * 1; }
        public JwtResultModel(string accessToken)
        {
            AccessToken = accessToken;
        }
    }
}
