using Microsoft.EntityFrameworkCore;
using Payments.Domain;
using Peyment.Persistence.Database.Configuration;
using System;
using System.Linq;

namespace Peyment.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> option) : base(option)
        {

        }

        #region Dbset
        public DbSet<Plan> Plans { get; set; }
        public DbSet<Product> Pruducts { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            var cascadeFKs = modelBuilder.Model.GetEntityTypes()
            .SelectMany(t => t.GetForeignKeys())
            .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);

            foreach (var fk in cascadeFKs)
                fk.DeleteBehavior = DeleteBehavior.Restrict;

            modelBuilder.HasDefaultSchema("Makaya");
            ModelConfig(modelBuilder);
        }


        private void ModelConfig(ModelBuilder modelBuilder)
        {
            new PlanConfiguration(modelBuilder.Entity<Plan>());
            new ProductConfiguration(modelBuilder.Entity<Product>());
            new SubscribeConfiguration(modelBuilder.Entity<Subscribe>());
        }

    }
}
