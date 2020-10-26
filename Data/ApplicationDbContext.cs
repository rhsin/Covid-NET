using Covid.Models;
using IdentityServer4.EntityFramework.Options;
using Microsoft.AspNetCore.ApiAuthorization.IdentityServer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace Covid.Data
{
    public class ApplicationDbContext : ApiAuthorizationDbContext<AppUser>
    {
        public ApplicationDbContext(
            DbContextOptions options,
            IOptions<OperationalStoreOptions> operationalStoreOptions) : base(options, operationalStoreOptions)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<CountListDailyCount>()
                .HasKey(cd => new { cd.CountListId, cd.DailyCountId });
        }

        public DbSet<AppUser> AppUser { get; set; }
        public DbSet<CountList> CountList { get; set; }
        public DbSet<DailyCount> DailyCount { get; set; }
        public DbSet<CountListDailyCount> CountListDailyCount { get; set; }
    }
}
