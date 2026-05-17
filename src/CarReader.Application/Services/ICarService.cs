using CarReader.Application.Common;
using CarReader.Application.Models;

namespace CarReader.Application.Services
{
    public interface ICarService
    {
        public DataSource<CarDto> LoadCars(string path);
    }
}
