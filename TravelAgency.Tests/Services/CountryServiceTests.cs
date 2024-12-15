using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.Services;

namespace TravelAgency.Tests.Services;

[TestClass]
public class CountryServiceTests
{
    private DataContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }

    [TestMethod]
    public async Task GetAllForSelectAsync_ReturnsAllCountriesInOrder()
    {
        var context = CreateInMemoryContext();
        context.Countries.AddRange(
            new Country { Id = 1, Name = "Россия" },
            new Country { Id = 2, Name = "Китай" }
        );
        await context.SaveChangesAsync();

        var mapperMock = new Mock<IMapper>();
        var service = new CountryService(context, mapperMock.Object);

        var result = await service.GetAllForSelectAsync();

        Assert.AreEqual(2, result.Count);
        Assert.AreEqual("Китай", result[0].Name);
        Assert.AreEqual("Россия", result[1].Name);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsCountryDetails_WhenCountryExists()
    {
        var context = CreateInMemoryContext();
        context.Countries.Add(new Country { Id = 1, Name = "Россия" });
        await context.SaveChangesAsync();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Country, CountryDetailsDTO>();
        }));

        var service = new CountryService(context, mapperMock.Object);

        var result = await service.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Россия", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Страна не найдена")]
    public async Task GetByIdAsync_ThrowsException_WhenCountryDoesNotExist()
    {
        var context = CreateInMemoryContext();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.ConfigurationProvider).Returns(new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Country, CountryDetailsDTO>();
        }));

        var service = new CountryService(context, mapperMock.Object);

        await service.GetByIdAsync(999);
    }

    [TestMethod]
    public async Task CreateAsync_AddsCountryToDatabase()
    {
        var context = CreateInMemoryContext();
        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(m => m.Map<Country>(It.IsAny<CountryCreateDTO>()))
            .Returns((CountryCreateDTO dto) => new Country { Name = dto.Name });

        var service = new CountryService(context, mapperMock.Object);
        var countryDto = new CountryCreateDTO { Name = "Россия" };

        await service.CreateAsync(countryDto);

        Assert.AreEqual(1, context.Countries.Count());
        Assert.AreEqual("Россия", context.Countries.First().Name);
    }

    [TestMethod]
    public async Task DeleteAsync_RemovesCountryFromDatabase()
    {
        var context = CreateInMemoryContext();
        context.Countries.Add(new Country { Id = 1, Name = "Украина" });
        await context.SaveChangesAsync();

        var mapperMock = new Mock<IMapper>();
        var service = new CountryService(context, mapperMock.Object);

        await service.DeleteAsync(1);

        Assert.AreEqual(0, context.Countries.Count());
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Страна не найдена")]
    public async Task DeleteAsync_ThrowsException_WhenCountryDoesNotExist()
    {
        var context = CreateInMemoryContext();
        var mapperMock = new Mock<IMapper>();
        var service = new CountryService(context, mapperMock.Object);

        await service.DeleteAsync(999);
    }
}