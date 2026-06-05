using CarReader.Application.Repositories;
using CarReader.Application.Services;
using CarReader.Domain.Entities;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace CarReader.Tests;

public class CarServiceTests
{
    private readonly Mock<ICarRepository> repoMock;
    private readonly Mock<ILogger<CarService>> loggerMock;

    public CarServiceTests()
    {
        repoMock = new Mock<ICarRepository>();
        loggerMock = new Mock<ILogger<CarService>>();
    }

    private CarService CreateService()
    {
        return new CarService(repoMock.Object, loggerMock.Object);
    }

    [Fact]
    public void LoadWeekendSales_ReturnsWeekendSalesSum_WhenRepositoryReturnsCars()
    {
        var now = DateTime.Now;

        var cars = new List<Car>
        {
            new Car
            {
                Name = "Skoda",
                SellDate = new DateTime(2026, 5, 24), // weekend sale
                Price = 100,
                Vat = 21,
            },
            new Car
            {
                Name = "Skoda",
                SellDate = new DateTime(2026, 5, 30), // weekend sale
                Price = 100,
                Vat = 21,
            },
            new Car
            {
                Name = "Skoda",
                SellDate = new DateTime(2026, 6, 3), // workday sale
                Price = 100,
                Vat = 21,
            },
        };

        repoMock.Setup(r => r.Load(It.IsAny<string>())).Returns(cars);

        var service = CreateService();

        var result = service.LoadWeekendSales("test.xml");

        repoMock.Verify(r => r.Load(It.IsAny<string>()), Times.Once);

        result.IsOk.Should().BeTrue();
        result.Data.Should().NotBeNull();
        result.Data.Should().HaveCount(1);
        result.Data.First().Name.Should().Be("Skoda");
        result.Data.First().Price.Should().Be(200);
    }

    [Fact]
    public void LoadWeekendSales_ReturnsErrorMessage_WhenRepositoryReturnsNoCars()
    {
        repoMock.Setup(r => r.Load(It.IsAny<string>())).Returns([]);

        var service = CreateService();

        var result = service.LoadWeekendSales("test.xml");

        repoMock.Verify(r => r.Load(It.IsAny<string>()), Times.Once);

        result.IsOk.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNullOrWhiteSpace();
    }

    [Fact]
    public void LoadWeekendSales_LogErrorAndReturnsErrorMessage_WhenRepositoryLoadFailed()
    {
        string errorMsg = "Failed to load cars";

        repoMock.Setup(r => r.Load(It.IsAny<string>())).Throws(new Exception(errorMsg));

        var service = CreateService();

        var result = service.LoadWeekendSales("test.xml");

        loggerMock.Verify(
            x => x.Log(LogLevel.Error, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), It.Is<Exception>(ex => ex.Message.Contains(errorMsg)), It.IsAny<Func<It.IsAnyType, Exception?, string>>()),
            Times.Once
        );

        result.IsOk.Should().BeFalse();
        result.ErrorMessage.Should().NotBeNullOrWhiteSpace();
    }
}
