using CarReader.Domain.Entities;

namespace CarReader.Application.Repositories
{
    public interface ICarRepository
    {
        IReadOnlyCollection<Car> Load(string path);
    }
}
