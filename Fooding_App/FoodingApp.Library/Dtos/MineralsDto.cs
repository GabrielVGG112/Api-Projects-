namespace FoodingApp.Api.Dtos
{
    public class MineralsDto
    {
        public int FoodItemId { get; set; }
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
    }
}