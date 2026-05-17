using CarReader.Application.Common;
using CarReader.Application.Mappers;
using CarReader.Application.Models;
using CarReader.Application.Repositories;
using CarReader.Domain.Entities;

namespace CarReader.Application.Services
{
    public class CarService : ICarService
    {
        private readonly ICarRepository repository;
        private readonly CarMapper mapper;

        public CarService(ICarRepository repository, CarMapper mapper)
        {
            this.repository = repository;
            this.mapper = mapper;
        }

        public DataSource<CarDto> LoadCars(string path)
        {
            try
            {
                IReadOnlyCollection<Car> cars = repository.Load(path);

                if (cars == null || !cars.Any())
                {
                    return DataSource<CarDto>.CreateError("Načtená data jsou prázdná.");
                }

                var data = cars.OrderByDescending(x => x.Price).ToList();

                return DataSource<CarDto>.CreateSuccess(mapper.ToDtos(data));
            }
            catch
            {
                return DataSource<CarDto>.CreateError("Při načítání dat došlo k chybě.");
            }
        }
    }
}
