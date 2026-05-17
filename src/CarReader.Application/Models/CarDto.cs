namespace CarReader.Application.Models
{
    public class CarDto
    {
        public string Name { get; set; } = string.Empty;

        public DateTime SellDate { get; set; }

        public double Price { get; set; }

        public double Vat { get; set; }

        public double PriceWithVat { get; set; }
    }
}
