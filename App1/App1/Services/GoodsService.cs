namespace App1.Services
{
    using Database.Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public class GoodsService : BaseService, IGoodsService
    {
        public GoodsService()
        {
        }

        /// <inheritdoc/>
        public async Task<ICollection<GoodsModel>> GetAsync()
        {
            var entities = await DbContext.Goods
                .AsNoTracking()
                .ToListAsync();

            return Mapper.Map<ICollection<Goods>, ICollection<GoodsModel>>(entities);
        }

        /// <inheritdoc/>
        public async Task AddRecordAsync(GoodsModel record)
        {
            var entities = await DbContext.Goods
                .AsNoTracking()
                .Where(e => e.Url == record.Url)
                .ToListAsync();

            if (entities.Any())
            {
                throw new Exception("App.Exceptions.UrlAlreadySet");
            }

            var entity = Mapper.Map<Goods>(record);

            DbContext.Goods.Add(entity);
            await DbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteRecordAsync(Guid recordId)
        {
            var entity = await DbContext.Goods.FindAsync(recordId);

            DbContext.Goods.Remove(entity);
            await DbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task DeleteSeveralRecordsAsync(ICollection<Guid> recordIds)
        {
            var entities = await DbContext.Goods.FindAsync(recordIds);

            DbContext.Goods.RemoveRange(entities);
            await DbContext.SaveChangesAsync();
        }

        /// <inheritdoc/>
        public async Task UpdateAsync(GoodsModel model)
        {
            var entity = await DbContext.Goods.FindAsync(model.GoodsId);

            Mapper.Map(model, entity);

            DbContext.Goods.Update(entity);
            await DbContext.SaveChangesAsync();
        }
    }
}
