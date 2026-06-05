using CarReader.Application.Common;
using CarReader.Application.Models;
using CarReader.Application.Repositories;
using CarReader.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarReader.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository repository;
        private readonly ILogger<CarService> logger;

        public CarService(ICarRepository repository, ILogger<CarService> logger)
        {
            this.repository = repository;
            this.logger = logger;
        }

        public DataSource<CarDto> LoadWeekendSales(string path)
        {
            try
            {
                logger.LogInformation($"Načítám data ze souboru: {path}");
                IReadOnlyCollection<Car> cars = repository.Load(path);

                if (cars.Count == 0)
                {
                    return DataSource<CarDto>.CreateError("Načtená data jsou prázdná.");
                }

                var data = cars.GroupBy(x => x.Name)
                    .Select(g => new CarDto
                    {
                        Name = g.Key,
                        Price = g.Sum(c => c.IsWeekendSell ? c.Price : 0),
                        PriceWithVat = g.Sum(c => c.IsWeekendSell ? c.PriceWithVat : 0),
                    })
                    .ToList();

                return DataSource<CarDto>.CreateSuccess(data);
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Při načítání dat došlo k chybě.");
                return DataSource<CarDto>.CreateError("Při načítání dat došlo k chybě.");
            }
        }
    }
}
