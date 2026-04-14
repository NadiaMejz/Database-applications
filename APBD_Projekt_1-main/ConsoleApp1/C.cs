class C : Container
{
    public static int objectCount = 0;
    public double maintainedTemperature;
    private HazardNotifier _notifier = new HazardNotifier();
    private List<Load> loadsAdded = new List<Load>();



    public C(double loadWeight, double height, double containerWeight, double depth, double maxLoadWeight,
        double maintainedTemperature) : base(loadWeight, height, containerWeight, depth,
        maxLoadWeight)
    {
        this.maintainedTemperature = maintainedTemperature;
    }

   

    protected override string CreateContainerName()
    {
        objectCount++;
        return "KON-" + this.GetClassName() + "-" + objectCount;
    }

    public void LoadContainer(Load load, double maintainedTemperature)
    {
        if (!loadsAdded.Any())
        {
            if (load.cargoCategory.ToString() == this.GetType().Name && //PODODAWAJ TUTAJ OSTRZEZENIA
                this.IsTemperatureCompatible(this.maintainedTemperature, load))
            {
                if (this.loadWeightAvailable <= load.loadWeight)
                {
                    load.containerName = this.containerName;
                    this.loadWeightAvailable -= load.loadWeight;
                    this.loadsAdded.Add(load);
                }
                else
                {
                    throw new OverfillException(containerName);
                }
            }
        }
        else

        {
            foreach (var load1 in loadsAdded)
            {
                var referenceProductName = load1.productsInCargo.FirstOrDefault()?.Name;
                bool isHomogeneous = load.productsInCargo.All(p => p.Name == referenceProductName);
                if (isHomogeneous)
                {
                    if (load.cargoCategory.ToString() == this.GetType().Name &&
                        this.IsTemperatureCompatible(this.maintainedTemperature, load))
                    {
                        if (this.maxLoadWeight <= load.loadWeight)
                        {
                            load.containerName = this.containerName;
                            this.loadWeightAvailable -= load.loadWeight;
                            this.loadsAdded.Add(load);
                        }
                        else
                        {
                            throw new OverfillException(containerName);
                        }
                    }
                }

                _notifier.Notify("notCompatibleType",containerName);
            }
        }  
    }

    public void unloadContainer(Load load)
    {
        loadsAdded.Remove(load);
        this.loadWeightAvailable += load.loadWeight;
    }

    public void unloadAllLoads()
    {
        loadsAdded.Clear();
        this.loadWeightAvailable = this.maxLoadWeight;
    }

    public bool IsProductAllowed(HashSet<Product> productTypeAllowed, List<Product> listOfProductsToLoad)
    {
        string nameOfTheProduct = listOfProductsToLoad[0].Name;
        foreach (var product in productTypeAllowed)
        {
            if (nameOfTheProduct == product.Name)
            {
                return true;
            }
        }

        return false;
    }


    public bool IsTemperatureCompatible(double maintainedTemperature, Load load) 
    {
        if (load.productsInCargo.All(p => p.Temperature > maintainedTemperature))
        {
            return true;
        }
        return false;
    }
    
    public override void PrintContainerInfo()
    {
        Console.WriteLine("Cooling Container");
        Console.WriteLine("Cooling Containers maxLoadWeight : "+ this.maxLoadWeight);
        Console.WriteLine("Cooling Containers maintainingTemperature : "+ this.maintainedTemperature);
        Console.WriteLine("Container height:  " + this.height);
        Console.WriteLine("Container depth:  " + this.depth);

    }

}