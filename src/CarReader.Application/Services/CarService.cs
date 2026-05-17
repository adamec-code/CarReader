using CarReader.Application.Common;
using CarReader.Application.Mappers;
using CarReader.Application.Models;
using CarReader.Application.Repositories;
using CarReader.Domain.Entities;
using Microsoft.Extensions.Logging;

namespace CarReader.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository repository;
        private readonly CarMapper mapper;
        private readonly ILogger<CarService> logger;

        public CarService(ICarRepository repository, CarMapper mapper, ILogger<CarService> logger)
        {
            this.repository = repository;
            this.mapper = mapper;
            this.logger = logger;
        }

        public DataSource<CarDto> LoadCars(string path)
        {
            try
            {
                logger.LogInformation($"Načítám data ze souboru: {path}");
                IReadOnlyCollection<Car> cars = repository.Load(path);

                if (cars == null || !cars.Any())
                {
                    return DataSource<CarDto>.CreateError("Načtená data jsou prázdná.");
                }

                var data = cars.OrderByDescending(x => x.Price).ToList();

                return DataSource<CarDto>.CreateSuccess(mapper.ToDtos(data));
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Při načítání dat došlo k chybě.");
                return DataSource<CarDto>.CreateError("Při načítání dat došlo k chybě.");
            }
        }
    }
}
