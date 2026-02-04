using FoodingApp.Library.Enums;

namespace FoodingApp.Library.Models;

public class RecipeModel
{
    private double _kcal;
    private DificultyLevelEnum _dificulty;
    public RecipeModel()
    {

    }
    public RecipeModel(string name, int preparationTimeInMinutes)
    {
        Name = name;
        PreparationTime = preparationTimeInMinutes;
        Ingredients = new HashSet<RecipeIngredient>();
        GetDificulty();
    }
    public int Id { get; set; }
    public string Name { get; set; }

    public string Instructions { get; set; }
    public int PreparationTime { get; set; }
    public PreparationMethodEnum PreparationMethod { get; set; }

    public DificultyLevelEnum Difficulty => _dificulty;
    public double Kcal => _kcal;

    //--- relationships --- 
    public ICollection<RecipeIngredient> Ingredients { get; set; }





    // --- > predefiend ----

    public void GetKcal()
    {
        _kcal = Ingredients?.Sum(x => x.Kcal) ?? 0;
    }


    public void GetDificulty()
    {
        int ingredientScore = Ingredients.Count() switch
        {
            <= 5 => 0,
            <= 10 => 1,
            _ => 2
        };

        int timeScore = PreparationTime switch
        {
            < 30 => 0,
            <= 60 => 1,
            _ => 2
        };

        int total = ingredientScore + timeScore;

        _dificulty = total switch
        {
            <= 1 => DificultyLevelEnum.Easy,
            2 => DificultyLevelEnum.Medium,
            _ => DificultyLevelEnum.Hard
        };
    }
}