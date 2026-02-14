namespace FoodingApp.Library.Models;

public class PrimaryCategory
{

    public int Id { get; set; }
    public string Name { get; set; }
    public PrimaryCategory()
    {

    }
    public PrimaryCategory(int id, string name)
    {
        Id = id;
        Name = name;
    }
}
