using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;
using TravelAgency.Services;

namespace TravelAgency.Tests.Services
{
    [TestClass]
    public class CountryServiceTests
    {
        [TestMethod]
        public async Task GetAllCountriesAsync_ShouldReturnListOfCountries()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);
            var _service = new CountryService(_context);

            _context.Countries.AddRange(new List<Country>
            {
                new Country { Id = 1, Name = "Россия" },
                new Country { Id = 2, Name = "Китай" }
            });

            await _context.SaveChangesAsync();

            var service = new CountryService(_context);

            var result = await service.GetAllCountriesAsync();

            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count);
            Assert.AreEqual("Россия", result.First().Name);
        }

        [TestMethod]
        public async Task GetCountryByIdAsync_ShouldReturnCountry_WhenIdExists()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);
            var _service = new CountryService(_context);

            _context.Countries.AddRange(new List<Country>
            {
                new Country { Id = 1, Name = "Россия" },
                new Country { Id = 2, Name = "Китай" }
            });
            await _context.SaveChangesAsync();

            var service = new CountryService(_context);

            var result = await service.GetCountryByIdAsync(1);

            Assert.IsNotNull(result);
            Assert.AreEqual("Россия", result.Name);
        }

        [TestMethod]
        public async Task GetCountryByIdAsync_ShouldReturnNull_WhenIdDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);

            _context.Countries.AddRange(new List<Country>
            {
                new Country { Id = 1, Name = "Россия" }
            });
            await _context.SaveChangesAsync();

            var service = new CountryService(_context);

            var result = await service.GetCountryByIdAsync(99);

            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task CreateCountryAsync_ShouldReturnTrue_WhenCountryIsCreated()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);
            var _service = new CountryService(_context);

            var country = new Country
            {
                Id = 1,
                Name = "Россия"
            };

            var result = await _service.CreateCountryAsync(country);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateCountryAsync_ShouldReturnTrue_WhenCountryIsUpdated()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);
            var _service = new CountryService(_context);

            var country = new Country
            {
                Id = 1,
                Name = "Россия"
            };

            _context.Add(country);

            await _context.SaveChangesAsync();

            country.Name = "Россия топ";
            var result = await _service.UpdateCountryAsync(country);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteCountryAsync_ShouldReturnTrue_WhenCountryIsDeleted()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);
            var _service = new CountryService(_context);

            var country = new Country
            {
                Id = 1,
                Name = "Украина"
            };

            _context.Add(country);

            await _context.SaveChangesAsync();

            var result = await _service.DeleteCountryAsync(1);

            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task DeleteCountryAsync_ShouldReturnFalse_WhenCountryDoesNotExist()
        {
            var options = new DbContextOptionsBuilder<DataContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            var _context = new DataContext(options);
            var _service = new CountryService(_context);

            var countryId = 99;
            var result = await _service.DeleteCountryAsync(countryId);

            Assert.IsFalse(result);
        }
    }
}