namespace App1.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class GoodsModel
    {
        [Key]
        public Guid GoodsId { get; set; }

        public string Name { get; set; }

        public decimal UsualPrice { get; set; }

        public decimal ActualPrice { get; set; }

        public DateTimeOffset LastUpdate { get; set; }

        public string Url { get; set; }

        public string ImageUrl { get; set; }
    }
}
