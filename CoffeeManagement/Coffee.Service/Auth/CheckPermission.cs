using Coffee.Core.DbManager;
using Coffee.EntityFramworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.Core.Auth
{
    public class CheckPermission : ICheckPermission
    {
      //  private readonly CoffeeDbContext _dbContext;
        public CheckPermission(CoffeeDbContext dbContext)
        {
        //    _dbContext = dbContext;
        }
        public async Task<bool> CheckPermissionAsync(long userId, string role)
        {
            return true;
        }
    }

    public interface ICheckPermission
    {
        public Task<bool> CheckPermissionAsync(long userId, string role);
    }
}
