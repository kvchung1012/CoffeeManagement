using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Helper
{
    public static class BcryptHelper
    {
        public static string GetPasswordHash(string password) => BCrypt.Net.BCrypt.HashPassword(password);

        public static bool VerifiedPassword(string password, string passwordHash) => BCrypt.Net.BCrypt.Verify(password, passwordHash);
    }
}
