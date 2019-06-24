using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyMaaser.data
{
    public class MaaserContext : DbContext
    {
        private string _connString;
        public MaaserContext(string connString)
        {
            _connString = connString;
        }

        public DbSet<MoneyEarned> MoneyEarned { get; set; }
        public DbSet<MaaserGiven> MaaserGiven { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<GiveToMoney> GiveToMoney { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connString);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            modelBuilder.Entity<GiveToMoney>()
                .HasKey(gm => new { gm.MoneyId, gm.MaaserGivenId });

            modelBuilder.Entity<GiveToMoney>()
                .HasOne(gm => gm.Money)
                .WithMany(m => m.GiveToMoney)
                .HasForeignKey(gm => gm.MoneyId);

            modelBuilder.Entity<GiveToMoney>()
                .HasOne(gm => gm.MaaserGiven)
                .WithMany(g => g.GiveToMoney)
                .HasForeignKey(gm => gm.MaaserGivenId);
        }

    }
}
