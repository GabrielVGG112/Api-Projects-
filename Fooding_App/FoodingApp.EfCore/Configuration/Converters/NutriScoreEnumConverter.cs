using FoodingApp.Library.Enums;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace FoodingApp.EfCore.Configuration.Converters
{
    public class NutriScoreEnumConverter : ValueConverter<NutriScoreEnum, string>
    {


        public NutriScoreEnumConverter()
            : base(v => v.ToString(),
                  v => (NutriScoreEnum)Enum.Parse(typeof(NutriScoreEnum), v)
                  )
        { }
    }
}
