using CleanArchitecture.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CleanArchitecture.Infrastructure.DataContext {
    public class ApplicationDataContext : DbContext {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options) 
            : base(options) {
        }

        public DbSet<Team> Teams { get; set; }

        protected override void OnModelCreating(ModelBuilder builder) {
            builder.Entity<Team>(ConfigureTeam);
        }

        private void ConfigureTeam(EntityTypeBuilder<Team> builder) {
            builder.ToTable("Team");

            builder.HasIndex(x => x.Name)
                .IsUnique()
                .HasName("TeamNameIndex");

            builder.Property(x => x.Id)
                .ValueGeneratedOnAdd();
        }
    }
}
