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
                IReadOnlyCollection<Car> data = repository.Load(path);

                if (data == null || !data.Any())
                {
                    return DataSource<CarDto>.CreateError("Načtená data jsou prázdná.");
                }

                return DataSource<CarDto>.CreateSuccess(mapper.ToDtos(data));
            }
            catch
            {
                return DataSource<CarDto>.CreateError("Při načítání dat došlo k chybě.");
            }
        }
    }
}
