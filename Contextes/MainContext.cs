using api.Models;
using Microsoft.EntityFrameworkCore;

namespace api.Contextes
{
    public class MainContext : DbContext
    {
        public MainContext(DbContextOptions<MainContext> options)  : base(options)
        {
            
        }
        public DbSet<Communication> Communication { get; set; }

        public DbSet<Certificates> Certificates { get; set; }


        public DbSet<Partners> Partners { get; set; }




    }
}
