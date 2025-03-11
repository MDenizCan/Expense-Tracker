using Microsoft.EntityFrameworkCore;

namespace SpendSmart.Models
{
    public class SpendSmartDbContext : DbContext //Inherits from DbContext
    {
        public DbSet<Expense> Expenses { get; set; } // EF Core will create a table called Expenses in the database
        public SpendSmartDbContext(DbContextOptions<SpendSmartDbContext> options) //Constructor
            : base(options)
        {
            
        }
    }
}
