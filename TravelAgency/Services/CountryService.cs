using Microsoft.EntityFrameworkCore;
using TravelAgency.Data;
using TravelAgency.Models;

namespace TravelAgency.Services
{
    public class CountryService
    {
        private readonly DataContext _context;

        public CountryService(DataContext context)
        {
            _context = context;
        }

        public async Task<List<Country>> GetAllCountriesAsync()
        {
            return await _context.Country.ToListAsync();
        }

        public async Task<Country?> GetCountryByIdAsync(int id)
        {
            return await _context.Country.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> CreateCountryAsync(Country country)
        {
            try
            {
                _context.Add(country);
                await _context.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<bool> UpdateCountryAsync(Country country)
        {
            try
            {
                _context.Update(country);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!await CountryExistsAsync(country.Id))
                {
                    return false;
                }
                throw;
            }
        }

        public async Task<bool> DeleteCountryAsync(int id)
        {
            var country = await _context.Country.FindAsync(id);
            if (country == null)
            {
                return false;
            }

            _context.Country.Remove(country);
            await _context.SaveChangesAsync();
            return true;
        }

        private async Task<bool> CountryExistsAsync(int id)
        {
            return await _context.Country.AnyAsync(c => c.Id == id);
        }
    }

}
