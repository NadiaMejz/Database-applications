public class Load 
{
    public char cargoCategory;
    public double loadWeight;
    public bool isCargoDangerous { get; private set; } 
    public string containerName;
    public HashSet<Product> productsInCargo = new HashSet<Product>();
    private HazardNotifier _notifier;// = new HazardNotifier();


    public Load(char cargoCategory)
    {
        this.loadWeight = 0;
        this.productsInCargo = new HashSet<Product>();
        this.cargoCategory = cargoCategory;
        isCargoDangerous = false;
        containerName = null;

    }

    public void AddProductToCargo(Product product, double weightOfAddedProducts)
    {
        if (this.cargoCategory == product.ProductCategory)
        {
            var category = this.cargoCategory;
            switch (category)
            {
                case 'L':
                    if (product.isDangerous == true)
                    {
                        productsInCargo.Add(product);
                        this.isCargoDangerous = true;
                        this.loadWeight += weightOfAddedProducts;
                    }
                    else
                    {
                        productsInCargo.Add(product);
                        this.loadWeight += weightOfAddedProducts;
                    }

                    break;
                case 'G':
                    productsInCargo.Add(product);
                    this.loadWeight += weightOfAddedProducts;
                    break;
                case 'C':
                    if (IsHomogeneousProductList(product, this) &&
                        IsProductAllowed(Product.allowedProductType, product) &&
                        IsTemperatureCompatible(product, Product.allowedProductType))
                    {
                        this.productsInCargo.Add(product);
                        this.loadWeight += weightOfAddedProducts;
                    }


                    break;
            }
        }
    }


    public bool IsProductAllowed(HashSet<Product> productTypeAllowed, Product product)
    {
        string nameOfTheProduct = product.Name;
        foreach (var product1 in productTypeAllowed)
        {
            if (nameOfTheProduct == product1.Name)
            {
                return true;
            }
        }

        Notify("NotAllowed");
        return false;
    }

    public bool IsHomogeneousProductList(Product product, Load load)
    {
        //p=> znaczy dla kazdego p w liscie zrob cos z p
        bool isHomogeneous = load.productsInCargo.All(p => p.Name == product.Name);
        if (!isHomogeneous)
        {
            Notify("NotHomogeneousProductLoad");
        }

        return isHomogeneous;
    }

    public double GetTemperature(HashSet<Product> productTypeAllowed, Product product)
    {
        string nameOfTheProduct = product.Name; //bierzemy nazwe 
        foreach (var variable in productTypeAllowed)
        {
            if (nameOfTheProduct == variable.Name) //jezeli nazwa pokrywa sie z produktem dozwolonym
            {
                return variable.Temperature; //zwracamy jego temperature
            }
        }

        throw new Exception("Product not found");
    }

    public bool IsTemperatureCompatible(Product product,
        HashSet<Product> productTypeAllowed) //ZROB COS Z TYM LISTOFPRODUCTSALLOWED 
    {
        double requireTemperature = GetTemperature(productTypeAllowed, product);
        if (product.Temperature == requireTemperature)
        {
            return true;
        }

        Console.WriteLine(
            $"⚠️ WARNING: Temperature mismatch detected. Required: {requireTemperature}°C, but got: {product.Temperature}°C.");
        return false;
    }

    public void Notify(string type)
    {
        _notifier.Notify(type);
    }
}