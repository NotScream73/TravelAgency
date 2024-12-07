using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel;
using System.Reflection;
using TravelAgency.Models;

namespace TravelAgency.Helpers;

public static class EnumHelper
{
    public static SelectList GetEnumSelectList<T>() where T : Enum
    {
        return new SelectList(Enum.GetValues(typeof(T)).Cast<T>().Select(e => new SelectListItem
        {
            Value = e.ToString(),
            Text = GetEnumDescription(e)
        }), "Value", "Text");
    }
    public static SelectList GetEnumSelectList<T>(T currentValue) where T : Enum
    {
        return new SelectList(Enum.GetValues(typeof(T)).Cast<T>().Select(e => new SelectListItem
        {
            Value = e.ToString(),
            Text = GetEnumDescription(e)
        }), "Value", "Text", currentValue);
    }

    private static string GetEnumDescription<T>(T enumValue) where T : Enum
    {
        var fieldInfo = enumValue.GetType().GetField(enumValue.ToString());
        var descriptionAttribute = fieldInfo?.GetCustomAttributes(typeof(DescriptionAttribute), false)
                                             .Cast<DescriptionAttribute>()
                                             .FirstOrDefault();
        return descriptionAttribute?.Description ?? enumValue.ToString();
    }
    public static string GetDescription(this ResortType value)
    {
        var field = value.GetType().GetField(value.ToString());
        var attribute = field?.GetCustomAttribute<DescriptionAttribute>();
        return attribute?.Description ?? value.ToString();
    }
}
