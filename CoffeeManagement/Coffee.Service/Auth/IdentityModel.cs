using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Auth
{
    public class IdentityModel : IIdentity
    {
        public IdentityModel()
        {

        }

        public bool IsInternal { get; init; }
        public long Id { get; init; }
        public string Signature { get; set; }
        public string FullName { get; init; }
        public string Username { get; init; }
        public string Email { get; init; }
        public string Phone { get; init; }

        public string AuthenticationType { get; init; }

        public bool IsAuthenticated { get; init; }

        public string Name { get; init; }
    }
}
