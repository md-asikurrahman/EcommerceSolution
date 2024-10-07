using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class ProductPromotion : BaseEntity
    {
        public int ProductId { get; set; }

        public Product Product { get; set; }

        public int PromotionId { get; set; }

        public Promotion? Promotion { get; set; }
    }
}
