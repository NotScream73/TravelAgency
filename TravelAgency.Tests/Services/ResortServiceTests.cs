using AutoMapper;
using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Models.DTO;
using TravelAgency.Services;
using TravelAgency.ViewModels.Resorts;

namespace TravelAgency.Tests.Services;

[TestClass]
public class ResortServiceTests
{
    private DataContext CreateInMemoryContext()
    {
        var options = new DbContextOptionsBuilder<DataContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        return new DataContext(options);
    }

    [TestMethod]
    public async Task GetAllAsync_ReturnsFilteredAndPagedResults()
    {
        var context = CreateInMemoryContext();
        context.Resorts.AddRange(
            new Resort { Id = 1, Name = "Beach Resort" },
            new Resort { Id = 2, Name = "Mountain Resort" },
            new Resort { Id = 3, Name = "City Resort" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Resort, ResortListDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        var filter = new ResortIndexFilterViewModel { Name = "Resort", Page = 0, Size = 2 };

        var (list, totalCount) = await service.GetAllAsync(filter);

        Assert.AreEqual(2, list.Count);
        Assert.AreEqual(3, totalCount);
        Assert.IsTrue(list.All(r => r.Name.Contains("Resort")));
    }

    [TestMethod]
    public async Task GetByIdAsync_ReturnsResortDetails_WhenExists()
    {
        var context = CreateInMemoryContext();
        context.Resorts.Add(new Resort { Id = 1, Name = "Beach Resort" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Resort, ResortDetailsDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        var result = await service.GetByIdAsync(1);

        Assert.IsNotNull(result);
        Assert.AreEqual(1, result.Id);
        Assert.AreEqual("Beach Resort", result.Name);
    }

    [TestMethod]
    public async Task GetByIdAsync_ThrowsException_WhenResortNotFound()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Resort, ResortDetailsDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        await Assert.ThrowsExceptionAsync<Exception>(async () => await service.GetByIdAsync(999));
    }

    [TestMethod]
    public async Task GetAllForSelectAsync_ReturnsAllResortsInOrder()
    {
        var context = CreateInMemoryContext();
        context.Resorts.AddRange(
            new Resort { Id = 1, Name = "Zebra Resort" },
            new Resort { Id = 2, Name = "Apple Resort" },
            new Resort { Id = 3, Name = "Ocean Resort" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<Resort, ResortForSelectDTO>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        var result = await service.GetAllForSelectAsync();

        Assert.AreEqual(3, result.Count);
        Assert.AreEqual("Apple Resort", result[0].Name);
        Assert.AreEqual("Ocean Resort", result[1].Name);
        Assert.AreEqual("Zebra Resort", result[2].Name);
    }

    [TestMethod]
    public void GetForCreate_ReturnsNewResortCreateDTO()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg => { });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        var result = service.GetForCreate();

        Assert.IsNotNull(result);
        Assert.AreEqual(ResortType.None, result.Type);
    }

    [TestMethod]
    public async Task CreateAsync_AddsNewResortToDatabase()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg =>
        {
            cfg.CreateMap<ResortCreateDTO, Resort>();
        });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        var dto = new ResortCreateDTO { Name = "New Resort" };

        await service.CreateAsync(dto);

        var resort = context.Resorts.FirstOrDefault();
        Assert.IsNotNull(resort);
        Assert.AreEqual("New Resort", resort.Name);
    }

    [TestMethod]
    public async Task DeleteAsync_RemovesResortFromDatabase_WhenExists()
    {
        var context = CreateInMemoryContext();
        context.Resorts.Add(new Resort { Id = 1, Name = "Beach Resort" });
        await context.SaveChangesAsync();

        var mapperConfig = new MapperConfiguration(cfg => { });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        await service.DeleteAsync(1);

        var resort = context.Resorts.FirstOrDefault();
        Assert.IsNull(resort);
    }

    [TestMethod]
    public async Task DeleteAsync_ThrowsException_WhenResortNotFound()
    {
        var context = CreateInMemoryContext();
        var mapperConfig = new MapperConfiguration(cfg => { });
        var mapper = mapperConfig.CreateMapper();
        var service = new ResortService(context, mapper);

        await Assert.ThrowsExceptionAsync<Exception>(async () => await service.DeleteAsync(999));
    }
}
