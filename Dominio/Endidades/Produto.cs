namespace Dominio.Entidades

{
    public class Produto : EntidadeBase
    {
        public string? ImageUrl { get; set; }
        public string? PriceMax { get; set; }
        public string? PriceMin { get; set; }
        public string? ProductName { get; set; }
        public string? OfferLink { get; set; }
        public long PeriodStartTime { get; set; }
        public long PeriodEndTime { get; set; }
        public int PriceDiscountRate { get; set; }
        public string RatingStar { get; set; }
    }
}
