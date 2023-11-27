using Microsoft.EntityFrameworkCore;
using WEBApi.Models;

namespace WEBApi.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) 
        { 
        }

    public DbSet<Cliente> Clientes { get; set; }

    public bool ClienteExists(int id)
        {
            return Clientes.Any(c => c.Id == id);
        }
    }
}
