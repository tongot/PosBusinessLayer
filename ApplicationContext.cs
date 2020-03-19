
using System.Data.Entity;

namespace AppDatabase
{
    public class ApplicationContext:DbContext
    {
        public ApplicationContext() :
            base("ApplicationContext")
        { } 
       public DbSet <Customer> customers { get; set; }
        public DbSet<Employee> employees { get; set; }
        public DbSet<Product> products { get; set; }
        public DbSet<Role> roles { get; set; }
        public DbSet<RoleToEmployee> RoleToEmployees { get; set; }
        public DbSet<Sale> sales { get; set; }
        public DbSet<Category> categories { get; set; }
        public DbSet<Branch> branches { get; set; }
        public DbSet<Permissions> Permissions { get; set; }
        public DbSet<permissionToRoles> permissionToRoles { get; set; }
        public DbSet<StockTrack> stockTracks { get; set; }
        public DbSet<Discount> Discounts { get; set; }
        public DbSet <Tax>Taxes { get; set; }
        public DbSet<ProductToDiscount> productToDiscounts { get; set; }
        public DbSet<ProductToTax> productToTaxes { get; set; }
        public DbSet<EmployeeDailyBalances> employeeDailyBalances { get; set; }
        public DbSet<OpenCloseBalances> openCloseBalances { get; set; }
        public DbSet<COB_Run> COB_Runs { get; set; }
        public DbSet<productOnsalelist> productOnsalelists { get; set; }
        public DbSet<Stock> stocks { get; set; }



    }
}
