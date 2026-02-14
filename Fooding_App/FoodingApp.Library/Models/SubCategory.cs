using System.Security.Cryptography.X509Certificates;

namespace FoodingApp.Library.Models;





public class SubCategory

{
    public SubCategory()
    {

    }
    public SubCategory(int id ,string groupName)
    {
        Id = id;
        Name = groupName;
    }
    public int Id { get; set; }
    public string Name { get; set; }


}