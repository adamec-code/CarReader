using System.Xml.Linq;
using CarReader.Application.Repositories;
using CarReader.Domain.Entities;

namespace CarReader.Infrastructure.Repositories
{
    public class XmlCarRepository : ICarRepository
    {
        public IReadOnlyCollection<Car> Load(string path)
        {
            var doc = XDocument.Load(path);

            var cars = doc.Root!.Elements("car")
                .Select(x => new Car
                {
                    Name = x.Element("name")?.Value ?? "",
                    SellDate = DateTime.Parse(x.Element("sellDate")?.Value ?? DateTime.MinValue.ToString()),
                    Price = double.Parse(x.Element("price")?.Value ?? "0"),
                    Vat = double.Parse(x.Element("vat")?.Value ?? "0"),
                })
                .ToList();

            return cars;
        }
    }
}
