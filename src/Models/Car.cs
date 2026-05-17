namespace CarReader.Models
{
    public class Car
    {
        public string Name { get; set; } = string.Empty;

        public DateTime SellDate { get; set; }

        public double Price { get; set; }

        public double Vat { get; set; }

        public double PriceWithVat => Price * (1 + Vat / 100);
    }
}
