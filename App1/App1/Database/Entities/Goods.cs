namespace App1.Database.Entities
{
    using System;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public class Goods : IEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid GoodsId { get; set; }

        public string Name { get; set; }

        public decimal UsualPrice { get; set; }

        public decimal ActualPrice { get; set; }

        public DateTimeOffset LastUpdate { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }
    }
}
