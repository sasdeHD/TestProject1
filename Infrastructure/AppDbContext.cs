using Microsoft.EntityFrameworkCore;
using System;
using System.Text.Json;
using TestProject1.Data.Models;

namespace TestProject1.Infrastructure
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options)
               : base(options)
        {
        }

        public DbSet<Claim> Claims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            var options = new JsonSerializerOptions();

            modelBuilder.Entity<Claim>(entity =>
            {
                entity.Property(e => e.AdditionalData)
                .HasConversion(
                        v => JsonSerializer.Serialize(v, options),
                        v => JsonSerializer.Deserialize<Dictionary<string, object>>(v, options)
                    )
                .HasColumnType("jsonb");
            });
        }
    }
}
