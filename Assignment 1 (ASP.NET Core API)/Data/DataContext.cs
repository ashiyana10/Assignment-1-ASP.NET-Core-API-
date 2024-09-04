using Assignment_1__ASP.NET_Core_API_.Models;
using Microsoft.EntityFrameworkCore;

namespace Assignment_1__ASP.NET_Core_API_.Data
{
    public class DataContext:DbContext
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Key_Value> Key_Values { get; set; }
    }
}
