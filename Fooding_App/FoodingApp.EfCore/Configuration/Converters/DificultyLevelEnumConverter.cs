using FoodingApp.Library.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodingApp.EfCore.Configuration.Converters
{
    public class DificultyLevelEnumConverter : ValueConverter<DificultyLevelEnum, string>
    {

        public DificultyLevelEnumConverter() :
            base(v => v.ToString(), v => (DificultyLevelEnum)Enum.Parse(typeof(DificultyLevelEnum), v))
        {

        }
    }
}
