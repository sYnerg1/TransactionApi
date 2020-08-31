using System;
using System.Collections.Generic;
using System.Text;
using LegioTest.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace LegioTest.Data.EntityFramerwork
{
    public class LegioContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Transaction> Transactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Transaction>()
                .Property(t => t.Id)
                .ValueGeneratedNever();

            base.OnModelCreating(modelBuilder);
        }

        public LegioContext(DbContextOptions<LegioContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public LegioContext()
            : base()
        {
        }

    }
}
