namespace CarReader.Application.Models
{
    public record CarDto
    {
        public required string Name { get; init; }

        public bool IsWeekendSell { get; init; }

        public double Price { get; init; }

        public double PriceWithVat { get; init; }
    }
}
