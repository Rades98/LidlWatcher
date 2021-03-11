namespace App1.Database
{
    using Entities;
    using Microsoft.EntityFrameworkCore;

    public interface IAppDbContext
    {
        public DbSet<Goods> Goods { get; set; }

        DbSet<TEntity> Set<TEntity>() where TEntity : class;
    }
}
