using Coffee.EntityFramworkCore.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Coffee.EntityFramworkCore
{
    public class CoffeeDbContext : DbContext
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }


        // define system

        public DbSet<SystemTable> SystemTables { get; set; }
        public DbSet<SystemTableColumn> SystemTableColumns { get; set; }
        public DbSet<MasterData> MasterDatas { get; set; }

        // authen && author
        public DbSet<Users> Users { get; set; }
        public DbSet<Roles> Roles { get; set; }
        public DbSet<Position> Positions { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<PermissionRole> PermissionRoles { get; set; }
        public DbSet<UserPosition> UserPositions { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }


        // quản lý kho hàng
        public DbSet<Material> Materials { get; set; }
        public DbSet<WareHouse> WareHouses { get; set; }
        public DbSet<ImportInvoice> ImportInvoices { get; set; }
        public DbSet<ImportInvoiceDetail> ImportInvoiceDetails { get; set; }
        public DbSet<RequestImport> RequestImports { get; set; }
        public DbSet<RequestImportDetail> RequestImportDetails { get; set; }

        public DbSet<ExportInvoice> ExportInvoices { get; set; }
        public DbSet<ExportInvoiceDetail> ExportInvoiceDetails { get; set; }

        // quản lý bán hàng
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }


        // quản lý sản phẩm

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductPrice> ProductPrices { get; set; }
        public DbSet<ProductCombo> ProductCombos { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet<ProductDiscount> ProductDiscounts { get; set; }
        public DbSet<ReviewProduct> ReviewProducts { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }

    }
}
