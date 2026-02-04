using FoodingApp.Library.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodingApp.EfCore.Configuration.Converters
{
    public class PreparationMethodEnumConverter : ValueConverter<PreparationMethodEnum, string>
    {
        public PreparationMethodEnumConverter()
            : base(
                  v => v.ToString(),
                  v => (PreparationMethodEnum)Enum.Parse(typeof(PreparationMethodEnum), v))
        { }
    }
}
