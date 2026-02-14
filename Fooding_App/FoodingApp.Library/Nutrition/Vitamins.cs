using FoodingApp.Api.Dtos;

namespace FoodingApp.Library.Nutrition;

public class Vitamins
{

    public double VitaminA { get; set; }
    public double VitaminB1 { get; set; }
    public double VitaminB2 { get; set; }
    public double VitaminB3 { get; set; }
    public double VitaminB5 { get; set; }
    public double VitaminB6 { get; set; }
    public double VitaminB7 { get; set; }
    public double VitaminB9 { get; set; }
    public double VitaminB12 { get; set; }
    public double VitaminC { get; set; }
    public double VitaminD { get; set; }
    public double VitaminE { get; set; }
    public double VitaminK { get; set; }
    public Vitamins()
    {

    }

    public Vitamins(VitaminsDto dto)
    {
        VitaminA= dto.VitaminA;
        VitaminB1 = dto.VitaminB1;
        VitaminB2 = dto.VitaminB2;
        VitaminB3 = dto.VitaminB3;
        VitaminB5 = dto.VitaminB5;
        VitaminB6 = dto.VitaminB6;
        VitaminB7 = dto.VitaminB7;
        VitaminB9 = dto.VitaminB9;
        VitaminB12 = dto.VitaminB12;
        VitaminC = dto.VitaminC;
        VitaminD = dto.VitaminD;
        VitaminE = dto.VitaminE;
        VitaminK = dto.VitaminK;
        
    }
    public static explicit operator Vitamins(VitaminsDto dto) => new Vitamins(dto); 
}
