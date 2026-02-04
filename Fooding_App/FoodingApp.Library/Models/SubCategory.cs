namespace FoodingApp.Library.Models;





public class SubCategory

{
    public SubCategory()
    {

    }
    public SubCategory(string groupName)
    {
        Name = groupName;
    }
    public int Id { get; set; }
    public string Name { get; set; }


}