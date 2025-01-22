using AktivCA.Domain.Settings;
using Microsoft.EntityFrameworkCore;

namespace AktivCA.Domain.EntityFrameworkCore.EntityFrameworkCore
{
    public class AktivCADbContext : DbContext
    {
        public AktivCADbContext(DbContextOptions<AktivCADbContext> config) : base(config)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
        public DbSet<Setting> Settings { get; set; }
    }
}
