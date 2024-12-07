using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.DTO;

public abstract class CountryDTO
{
    [DisplayName("Название")]
    [Required(ErrorMessage = "Название не может быть пустым")]
    public string? Name { get; set; }
}

public class CountryCreateDTO : CountryDTO
{
}

public class CountryEditDTO : CountryDTO
{
    public int Id { get; set; }
}

public class CountryDetailsDTO : CountryDTO
{
    public int Id { get; set; }
}

public class CountryDeleteDTO : CountryDTO
{
    public int Id { get; set; }
}

public class CountryListDTO : CountryDTO
{
    public int Id { get; set; }
}