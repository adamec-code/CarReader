using CarReader.Application.Models;
using CarReader.Domain.Entities;
using Riok.Mapperly.Abstractions;

namespace CarReader.Application.Mappers
{
    [Mapper]
    public partial class CarMapper
    {
        public partial CarDto ToDto(Car car);

        public partial IReadOnlyCollection<CarDto> ToDtos(IReadOnlyCollection<Car> cars);
    }
}
