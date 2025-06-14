using JobPro.Core.Models;
using Microsoft.EntityFrameworkCore;

namespace JobPro.Data.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options) { }

        public DbSet<User> Users { get; set; }
        public DbSet<Vacancy> Vacancies { get; set; }
        public DbSet<Application> Applications { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настройка связей
            modelBuilder.Entity<Vacancy>()
                .HasOne(v => v.User)
                .WithMany()
                .HasForeignKey(v => v.UserId);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.Vacancy)
                .WithMany()
                .HasForeignKey(a => a.VacancyId);

            modelBuilder.Entity<Application>()
                .HasOne(a => a.User)
                .WithMany()
                .HasForeignKey(a => a.UserId);
        }
    }
}