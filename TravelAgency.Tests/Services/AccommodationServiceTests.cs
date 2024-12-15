using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Accommodations;

namespace TravelAgency.Tests.Services;

[TestClass]
public class AccommodationServiceTests
{
    private DataContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        return new DataContext(options);
    }

    [TestMethod]
    public async Task GetAllForSelectAsync_ReturnsAllAccommodationsInOrder()
    {
        var context = CreateInMemoryContext();
        context.Accommodations.AddRange(
            new Accommodation { Id = 1, Name = "Отель 1" },
            new Accommodation { Id = 2, Name = "Отель 2" },
            new Accommodation { Id = 3, Name = "Отель 3" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Accommodation, AccommodationForSelectDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        var result = await service.GetAllForSelectAsync();

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("Отель 1", result[0].Name);
        Assert.AreEqual("Отель 2", result[1].Name);
        Assert.AreEqual("Отель 3", result[2].Name);
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsAccommodationDetails_WhenExists()
    {
        var context = CreateInMemoryContext();
        context.Accommodations.Add(new Accommodation { Id = 1, Name = "Отель 1" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Accommodation, AccommodationDetailsDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        var result = await service.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Отель 1", result.Name);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Проживание не найдено")]
    public async Task GetByIdAsync_ThrowsException_WhenAccommodationDoesNotExist()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Accommodation, AccommodationDetailsDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        await service.GetByIdAsync(999);
    }

    [TestMethod]
    public async Task CreateAsync_AddsAccommodationToDatabase()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<AccommodationCreateDTO, Accommodation>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        var dto = new AccommodationCreateDTO
        {
            Name = "Новый отель",
            Address = "Новый адрес"
        };

        await service.CreateAsync(dto);

        var accommodation = context.Accommodations.FirstOrDefault();
        Assert.IsNotNull(accommodation);
        Assert.AreEqual("Новый отель", accommodation.Name);
    }

    [TestMethod]
    public async Task DeleteAsync_RemovesAccommodationFromDatabase()
    {
        var context = CreateInMemoryContext();
        context.Accommodations.Add(new Accommodation { Id = 1, Name = "Отель 1" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg => { });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        await service.DeleteAsync(1);

        var accommodation = context.Accommodations.FirstOrDefault();
        Assert.IsNull(accommodation);
    }

    [TestMethod]
    [ExpectedException(typeof(Exception), "Проживание не найдено")]
    public async Task DeleteAsync_ThrowsException_WhenAccommodationNotFound()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg => { });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        await service.DeleteAsync(999);
    }
    [TestMethod]
    public async Task GetAllAsync_ReturnsFilteredAccommodations()
    {
        var context = CreateInMemoryContext();
        context.Accommodations.AddRange(
            new Accommodation { Id = 1, Name = "Отель 1" },
            new Accommodation { Id = 2, Name = "Отель 3" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Accommodation, AccommodationListDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new AccommodationService(context, mapper);

        var filter = new AccommodationIndexFilterViewModel
        {
            Name = "Отель 1",
            Page = 0,
            Size = 10
        };

        var (list, totalCount) = await service.GetAllAsync(filter);

        Assert.AreEqual(1, list.Count);
        Assert.AreEqual("Отель 1", list[0].Name);
        Assert.AreEqual(1, totalCount);
    }

}
