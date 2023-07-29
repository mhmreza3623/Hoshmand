using Hoshmand.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Hoshmand.Infrastructure.DataBase
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions options):base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

        }

        public DbSet<OrderRequestEntity> OrderRequests { get; set; }
        public DbSet<CheckCodeRequestEntity> CheckCodeRequests { get; set; }
        public DbSet<IdCardRequestEntity> IdCardRequests { get; set; }
        public DbSet<NumPhoneRequestEntity> NumPhoneRequests { get; set; }
    }
}
