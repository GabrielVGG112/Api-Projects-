using FoodingApp.Library.Enums;
using FoodingApp.Library.Extensions;
using System.ComponentModel.DataAnnotations.Schema;

namespace FoodingApp.Library.Models;

public class RecipeIngredient
{

    private double _kcal;
    private double _grams;
    private NutriScoreEnum _nutriscore;

    public RecipeIngredient()
    {

    }
    public RecipeIngredient(FoodItemModel model, int quantity, UnitEnum unitType)
    {
        FoodItem = model;
        QuantityInUnitValue = quantity;
        UnitType = unitType;
        GetGrams();
        GetNutriScore();
        GetKcal();
    }

    [NotMapped]
    public FoodItemModel FoodItem { get; set; }


    public int Id { get; set; }
    public double QuantityInUnitValue { get; set; }


    public double Grams => _grams;

    public double Kcal => _kcal;
    public UnitEnum UnitType { get; set; }
    public NutriScoreEnum NutriScore => _nutriscore;



    //-- relationships -- 
    public ICollection<RecipeModel> Recipees { get; set; } = new HashSet<RecipeModel>();


    public void GetGrams()
    {
        _grams = QuantityInUnitValue.ConvertToUnit(UnitType);
    }
    public void GetKcal()
    {
        _kcal = FoodItem.Nutrients.Macros.CalculateKcalFor(Grams);
    }
    public void GetNutriScore()
    {
        _nutriscore = FoodItem.Nutrients.CalculateNutriScore();
    }
}
