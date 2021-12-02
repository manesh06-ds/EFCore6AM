using Microsoft.EntityFrameworkCore;

namespace DAL
{
    public class AppDbContext : DbContext
    {
        public AppDbContext()
        {

        }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Data Source=DESKTOP-NJDPTI4\SQLEXPRESS;Initial Catalog=DB_Name;Integrated Security=True");

            //DESKTOP-NJDPTI4\SQLEXPRESS
        }
    }
}