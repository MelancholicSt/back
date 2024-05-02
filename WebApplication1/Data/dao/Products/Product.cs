using WebApplication1.Data.dao.Other;

namespace WebApplication1.Data.dao.Products;

public class Product
{
    public long Id { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    
    //Список категорий, в которые продукт входит
    public List<Category> Categories { get; set; }
   
    public Material Material { get; set; } = null!;
}