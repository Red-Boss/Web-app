using MebelWeb.Models;
using Microsoft.EntityFrameworkCore;


namespace MebelWeb.DatabaseContext
{
    public class AppDbContext:DbContext
    {
        public AppDbContext() { }

        

        // public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
        public DbSet<Ingridient> Ingridients { get; set; }
        public DbSet<Produkcia> Produkcias { get; set; }

        public DbSet<Doljnost> Doljnosts { get; set; }
        public DbSet<Syrio> Syrios { get; set; }
        public DbSet<Edinica_izmerenia> Edinica_izmerenias { get; set; }
        public DbSet<Employee> Employees { get; set; }

        public DbSet<Budjet> Budjets { get; set; }
        public DbSet<Proizvodstvo_produkci> Proizvodstvo_produkcis { get; set; }
        public DbSet<Prodaja_produkci> Prodaja_produkcis { get; set; }
        public DbSet<Zakupka_syria> Zakupka_syrias { get; set; }
        public DbSet<Zarplata> Zarplatas { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
        {
            Database.EnsureCreated();   // создаем базу данных при первом обращении
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=WIN-HQ8SMVI1KHH\\MSSQLSERVER01;Initial Catalog=MebelWeb;Integrated security=true;TrustServerCertificate=true");

            base.OnConfiguring(optionsBuilder);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Doljnost>()
            .HasMany(dl => dl.Employee)
            .WithOne(ep => ep.Doljnost)
            .HasForeignKey(ep => ep.doljnost);

            modelBuilder.Entity<Edinica_izmerenia>()
            .HasMany(dl => dl.Produkcia)
            .WithOne(ep => ep.Edinica_izmerenia)
            .HasForeignKey(ep => ep.edinica_izmerenia);

            modelBuilder.Entity<Edinica_izmerenia>()
            .HasMany(dl => dl.Syrio)
            .WithOne(ep => ep.Edinica_izmerenia)
            .HasForeignKey(ep => ep.edinica_izmerenia);


            modelBuilder.Entity<Produkcia>()
            .HasMany(dl => dl.Proizvodstvo_produkci)
            .WithOne(ep => ep.Produkcia)
            .HasForeignKey(ep => ep.produkcia);

            modelBuilder.Entity<Produkcia>()
            .HasMany(dl => dl.Prodaja_produkci)
            .WithOne(ep => ep.Produkcia)
            .HasForeignKey(ep => ep.produkcia);

            modelBuilder.Entity<Employee>()
            .HasMany(dl => dl.Proizvodstvo_produkci)
            .WithOne(ep => ep.Employee)
            .HasForeignKey(ep => ep.employee);

            modelBuilder.Entity<Employee>()
            .HasMany(dl => dl.Prodaja_produkci)
            .WithOne(ep => ep.Employee)
            .HasForeignKey(ep => ep.employee);

            modelBuilder.Entity<Employee>()
            .HasMany(dl => dl.Zakupka_syria)
            .WithOne(ep => ep.Employee)
            .HasForeignKey(ep => ep.employee);

            modelBuilder.Entity<Employee>()
            .HasMany(dl => dl.Zarplata)
            .WithOne(ep => ep.Employee)
            .HasForeignKey(ep => ep.employee);

            modelBuilder.Entity<Syrio>()
            .HasMany(dl => dl.Ingridient)
            .WithOne(ep => ep.Syrio)
            .HasForeignKey(ep => ep.syrio);

            modelBuilder.Entity<Syrio>()
            .HasMany(dl => dl.Zakupka_syria)
            .WithOne(ep => ep.Syrio)
            .HasForeignKey(ep => ep.syrio);
        }
        
    }
}
