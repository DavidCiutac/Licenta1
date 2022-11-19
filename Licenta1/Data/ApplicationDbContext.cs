using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Licenta1.Data;

namespace Licenta1.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Station> Stations { get; set; }
        public DbSet<GraphNetwork> GraphNetworks { get; set; }
        public DbSet<Node> Nodes { get; set; }
        public DbSet<TrainRoute> TrainRoutes { get; set; }
       
    }
}