using FoodingApp.Api.Dtos;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace FoodingApp.Library.Nutrition;

public class Minerals
{
    public int Id { get; set; }
    public double Calcium { get; set; }          // Ca
    public double Chloride { get; set; }         // Cl
    public double Magnesium { get; set; }        // Mg
    public double Phosphorus { get; set; }       // P
    public double Potassium { get; set; }        // K
    public double Sodium { get; set; }           // Na

    // Micro-minerals
    public double Iron { get; set; }             // Fe
    public double Zinc { get; set; }             // Zn
    public double Copper { get; set; }           // Cu
    public double Manganese { get; set; }        // Mn
    public double Selenium { get; set; }         // Se
    public double Iodine { get; set; }           // I


    public Minerals()
    {

    }
    public Minerals(MineralsDto dto)
    {
        Calcium = dto.Calcium;
        Chloride = dto.Chloride;
        Magnesium = dto.Magnesium;
        Phosphorus = dto.Phosphorus;
        Potassium = dto.Potassium;
        Sodium = dto.Sodium;
        Iron = dto.Iron;
        Zinc = dto.Zinc;
        Copper = dto.Copper;
        Manganese = dto.Manganese;
        Selenium = dto.Selenium;
        Iodine = dto.Iodine;
        Zinc = dto.Zinc;
        Manganese = dto.Manganese;
        Selenium = dto.Selenium;
        Iodine= dto.Iodine;
    }
    public static explicit operator  Minerals (MineralsDto dto) => new Minerals(dto);

}
