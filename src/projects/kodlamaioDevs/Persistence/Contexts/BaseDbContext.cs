using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Contexts
{
    public class BaseDbContext : DbContext
    {
        protected IConfiguration Configuration { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<Technology> Technologies { get; set; }
        public DbSet<Github> Githubs { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<OperationClaim> OperationClaims { get; set; }
        public DbSet<UserOperationClaim> UserOperationClaims { get; set; }


        public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
        {
            Configuration = configuration;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //if (!optionsBuilder.IsConfigured)
            //    base.OnConfiguring(
            //        optionsBuilder.UseSqlServer(Configuration.GetConnectionString("SomeConnectionString")));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Language>(a =>
            {
                a.ToTable("Languages").HasKey(k => k.Id);
                a.Property(p => p.Id).HasColumnName("Id");
                a.Property(p => p.Name).HasColumnName("Name");
                a.HasMany(p => p.Technologies);
            });

            modelBuilder.Entity<Technology>(t =>
            {
                t.ToTable("Technologies").HasKey(t => t.Id);
                t.Property(t => t.Id).HasColumnName("Id");
                t.Property(t => t.Name).HasColumnName("Name");
                t.Property(t => t.LanguageId).HasColumnName("LanguageId");
                t.HasOne(t => t.Language);
            });

            modelBuilder.Entity<Github>(g =>
            {
                g.ToTable("Githubs").HasKey(g => g.Id);
                g.Property(g => g.Id).HasColumnName("Id");
                g.Property(g => g.ProfileUrl).HasColumnName("ProfileUrl");
                g.Property(g => g.UserId).HasColumnName("UserId");
            });

            modelBuilder.Entity<User>(u =>
            {
                u.ToTable("Users").HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnName("Id");
                u.Property(u => u.FirstName).HasColumnName("FirstName");
                u.Property(u => u.LastName).HasColumnName("LastName");
                u.Property(u => u.Email).HasColumnName("Email");
                u.Property(u => u.PasswordSalt).HasColumnName("PasswordSalt");
                u.Property(u => u.PasswordHash).HasColumnName("PasswordHash");
                u.Property(u => u.Status).HasColumnName("Status");
                u.Property(u => u.AuthenticatorType).HasColumnName("AuthenticatorType");
                u.HasMany(c => c.UserOperationClaims);
                u.HasMany(c => c.RefreshTokens);

            });

            modelBuilder.Entity<OperationClaim>(o =>
            {
                o.ToTable("OperationClaims").HasKey(o => o.Id);
                o.Property(o => o.Id).HasColumnName("Id");
                o.Property(o => o.Name).HasColumnName("Name");

            });
            modelBuilder.Entity<UserOperationClaim>(u =>
            {
                u.ToTable("UserOperationClaims").HasKey(u => u.Id);
                u.Property(u => u.Id).HasColumnName("Id");
                u.Property(u => u.UserId).HasColumnName("UserId");
                u.Property(u => u.OperationClaimId).HasColumnName("OperationClaimId");
                u.HasOne(u => u.User);
                u.HasOne(u => u.OperationClaim);
            });



            Language[] brandEntitySeeds = { new(1, "C#"), new(2, "Java") };
            modelBuilder.Entity<Language>().HasData(brandEntitySeeds);

            Technology[] technologiesEntitySeeds = { new(1, "ASP.NET", 1), new(2, "Spring", 2) };
            modelBuilder.Entity<Technology>().HasData(technologiesEntitySeeds);

            OperationClaim[] operationClaimsEntitySeeds = { new(1, "Admin"), new(2, "User") };
            modelBuilder.Entity<OperationClaim>().HasData(operationClaimsEntitySeeds);

            Github[] githubEntitySeeds = { new(1, 1, "https://github.com/efkanbakanay") };
            modelBuilder.Entity<Github>().HasData(githubEntitySeeds);


        }
    }
}
