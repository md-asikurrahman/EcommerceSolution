using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class PromoSlider : BaseEntity
    {
        public string? SliderName { get; set; }

        public bool IsActive { get; set; }

        public int ProductId { get; set; }

        public Product? Product { get; set; }

        public List<SliderDetails>? SliderDetails { get; set; }
    }
}
