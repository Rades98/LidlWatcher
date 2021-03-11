namespace App1.Database
{
    using System;
    using System.IO;
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public sealed class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Goods> Goods { get; set; }

        public AppDbContext()
        {
            this.Database.EnsureCreated();
        }

        public new DbSet<TEntity> Set<TEntity>() where TEntity : class => base.Set<TEntity>();

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var dbPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "AppDb2.sqlite");
            optionsBuilder.UseSqlite($"Filename={dbPath}");
            optionsBuilder.EnableSensitiveDataLogging();
        }
    }
}