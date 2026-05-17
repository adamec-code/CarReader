using System.Runtime.ConstrainedExecution;
using CarReader.Application.Mappers;
using CarReader.Application.Models;
using CarReader.Application.Repositories;
using CarReader.Application.Services;
using CarReader.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarReader.Tests;

public class CarServiceTests
{
    [Fact]
    public void LoadCars_ReturnsData_WhenRepositoryReturnsCars()
    {
        var repoMock = new Mock<ICarRepository>();
        var loggerMock = new Mock<ILogger<CarService>>();
        var now = DateTime.Now;

        var cars = new List<Car>
        {
            new Car
            {
                Name = "Skoda",
                SellDate = now,
                Price = 100,
                Vat = 21,
            },
        };

        repoMock.Setup(r => r.Load(It.IsAny<string>())).Returns(cars);

        var service = new CarService(repoMock.Object, new CarMapper(), loggerMock.Object);

        var result = service.LoadCars("test.xml");

        result.IsOk.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data.First().Name.Should().Be("Skoda");
        result.Data.First().Price.Should().Be(100);
        result.Data.First().SellDate.Should().Be(now);
        result.Data.First().Vat.Should().Be(21);
    }

    [Fact]
    public void LoadCars_ReturnsErrorMessage_WhenRepositoryReturnsNoCars()
    {
        var repoMock = new Mock<ICarRepository>();
        var loggerMock = new Mock<ILogger<CarService>>();

        repoMock.Setup(r => r.Load(It.IsAny<string>())).Returns([]);

        var service = new CarService(repoMock.Object, new CarMapper(), loggerMock.Object);

        var result = service.LoadCars("test.xml");

        result.IsOk.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNullOrWhiteSpace();
    }
}
