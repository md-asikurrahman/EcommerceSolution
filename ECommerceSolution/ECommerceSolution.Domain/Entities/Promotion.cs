using ECommerceSolution.Domain.Common;


namespace ECommerceSolution.Domain.Entities
{
    public class Promotion : BaseEntity
    {
        public string Title { get; set; } = default!;

        public string? Description { get; set; }

        public DateTimeOffset StartDate { get; set; }

        public DateTimeOffset EndDate { get; set; }

        public double DiscountRate { get; set; }

        public List<ProductPromotion>? ProductPromotions { get; set; }
    }
}
