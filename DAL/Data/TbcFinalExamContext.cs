using DataContract.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Data
{
    public class TbcFinalExamContext : DbContext
    {
        public TbcFinalExamContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }

        public DbSet<Gender> Genders { get; set; }

        public DbSet<PhysicalPersonConnectionType> PhysicalPersonConnectionTypes { get; set; }

        public DbSet<TelephoneType> TelephoneTypes { get; set; }

        public DbSet<PhysicalPerson> PhysicalPersons { get; set; }

        public DbSet<PhysicalPersonConnection> PhysicalPersonConnections { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable("City");
            modelBuilder.Entity<Gender>().ToTable("Gender");
            modelBuilder.Entity<PhysicalPersonConnectionType>().ToTable("PhysicalPersonConnectionType");
            modelBuilder.Entity<TelephoneType>().ToTable("TelephoneType");
            modelBuilder.Entity<PhysicalPerson>().ToTable("PhysicalPerson");
            modelBuilder.Entity<PhysicalPersonConnection>().ToTable("PhysicalPersonConnection");
        }
    }
}
