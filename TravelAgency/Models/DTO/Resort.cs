using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace TravelAgency.Models.DTO;

public abstract class ResortDTO
{
    [DisplayName("Название")]
    [Required(ErrorMessage = "Название не может быть пустым")]
    public string? Name { get; set; }
}

public class ResortCreateDTO : ResortDTO
{
    [DisplayName("Описание")]
    public string? Description { get; set; }
    [DisplayName("Тип курорта")]
    public ResortType Type { get; set; }
}

public class ResortEditDTO : ResortDTO
{
    public int Id { get; set; }
    [DisplayName("Описание")]
    public string? Description { get; set; }
    [DisplayName("Тип курорта")]
    public ResortType Type { get; set; }
}

public class ResortDetailsDTO : ResortDTO
{
    public int Id { get; set; }
    [DisplayName("Описание")]
    public string? Description { get; set; }
    [DisplayName("Тип курорта")]
    public ResortType Type { get; set; }
}

public class ResortDeleteDTO : ResortDTO
{
    public int Id { get; set; }
}

public class ResortListDTO : ResortDTO
{
    public int Id { get; set; }
    [DisplayName("Тип курорта")]
    public ResortType Type { get; set; }
}