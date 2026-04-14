public class Product
{
    public string Name { get; set; }
    public double Temperature { get; set; } 
    public char ProductCategory { get; set; }
    public bool isDangerous;
    public static HashSet<Product> allowedProductType = new HashSet<Product>();
    
    public Product(string name, double temperature)
    {
        this.Name = name;
        this.Temperature = temperature;
    }

    public Product(string name, char ProductCategory, bool isDangerous = false)
    {
        this.Name = name;
        this.ProductCategory = ProductCategory;
        this.isDangerous = isDangerous;
    }

    public void addProductToAllowedList(HashSet<Product> listOfProductsToAdd, Product product)
    {
        if (!listOfProductsToAdd.Any(p => p.Name == product.Name))
        {
            listOfProductsToAdd.Add(product);
            Console.WriteLine("✅ Product added");
        }
        else
        {
            Console.WriteLine("⚠️ Product with same name already exists");
        }
    }
}