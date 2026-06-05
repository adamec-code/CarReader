namespace CarReader.Domain.Entities
{
    public class Car
    {
        public string Name { get; set; } = string.Empty;

        public DateTime SellDate { get; set; }

        public bool IsWeekendSell => SellDate.DayOfWeek == DayOfWeek.Saturday || SellDate.DayOfWeek == DayOfWeek.Sunday;

        public double Price { get; set; }

        public double Vat { get; set; }

        public double PriceWithVat => Price * (1 + Vat / 100);
    }
}
