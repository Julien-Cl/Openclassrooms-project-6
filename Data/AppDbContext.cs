using System.Net.Sockets;
using Microsoft.EntityFrameworkCore;
using NexaWorksTickets.Models;

namespace NexaWorksTickets.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<VersionNumber> VersionNumbers { get; set; }
        public DbSet<OpSystem> OperatingSystems { get; set; }
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Ticket> Tickets { get; set; }


        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Pour que SQL server n'affiche pas de message d'erreur lié au risque de suppressions en cascade à cause des références croisées. Donc ici on indique qu'on désactive les suppressions en cascade lors des migrations.
            foreach (var relationship in modelBuilder.Model.GetEntityTypes()
                .SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }












        }

    }
}
