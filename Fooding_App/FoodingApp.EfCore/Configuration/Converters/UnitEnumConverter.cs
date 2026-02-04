using FoodingApp.Library.Extensions;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodingApp.EfCore.Configuration.Converters
{
    public class UnitEnumConverter : ValueConverter<UnitEnum, string>
    {
        public UnitEnumConverter() : base(
            v => v.ToString(),
            v => (UnitEnum)Enum.Parse(typeof(UnitEnum), v))
        { }

    }
}
