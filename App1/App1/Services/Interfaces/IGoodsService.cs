namespace App1.Services.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Models;

    /// <summary>
    /// IGoodsService
    /// </summary>
    public interface IGoodsService
    {
        /// <summary>
        /// Gets the asynchronous.
        /// </summary>
        /// <returns></returns>
        Task<ICollection<GoodsModel>> GetAsync();

        /// <summary>
        /// Updates the specified model.
        /// </summary>
        /// <param name="model">The models.</param>
        /// <returns></returns>
        Task UpdateAsync(GoodsModel model);

        /// <summary>
        /// Adds the record asynchronous.
        /// </summary>
        /// <param name="record">The record.</param>
        Task AddRecordAsync(GoodsModel record);

        /// <summary>
        /// Deletes the specified record.
        /// </summary>
        /// <param name="recordId">The record.</param>
        /// <returns></returns>
        Task DeleteRecordAsync(Guid recordId);

        /// <summary>
        /// Deletes the specified records.
        /// </summary>
        /// <param name="recordIds">The record.</param>
        /// <returns></returns>
        Task DeleteSeveralRecordsAsync(ICollection<Guid> recordIds);
    }
}
