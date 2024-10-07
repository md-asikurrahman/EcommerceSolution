using ECommerceSolution.Domain.Common;

namespace ECommerceSolution.Domain.Entities
{
    public class SliderDetails : BaseEntity
    {
        public string? SliderTitle { get; set; }

        public string? TitleColor { get; set; }

        public string? SliderSubTitle { get; set; }

        public string? SubTitleColor { get; set; }

        public string? SliderInfo { get; set; }

        public string? LinkText { get; set; }
        public string? SliderImage { get; set; }

        public Guid PromoSliderId { get; set; }

        public PromoSlider? PromoSlider { get; set; }
    }
}
