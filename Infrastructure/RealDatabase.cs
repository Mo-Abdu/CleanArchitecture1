using Microsoft.EntityFrameworkCore;
using Models;

namespace Infrastructure
{
    public class RealDatabase : DbContext
    {
        public RealDatabase(DbContextOptions<RealDatabase> options) : base(options) { }
        public DbSet<Author> Authors { get; set; } 
        public DbSet<Book> Books { get; set; }

    }
}
