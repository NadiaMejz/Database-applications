

class L : Container, IHazardNotifier
{
    public static int objectCount = 0;
    private List<Load> loadsAdded = new List<Load>();
    private HazardNotifier _notifier = new HazardNotifier();


//tutaj jest zle poniewaz to produkt powinnismy sprwadzac czy jest niebespieczny a nie kontener
    public L(double loadWeight, double height, double contenerWeight, double depth, double maxLoadWeight) : base(
        loadWeight, height, contenerWeight, depth, maxLoadWeight)
    {
    }


    public void LoadContainer(double loadWeight, Load load)
    {
        if (load.cargoCategory.ToString() == this.GetType().Name)
        {
            if (!loadsAdded.Any())
            {
                //PODODAWAJ TUTAJ OSTRZEZENIA
                if (load.isCargoDangerous == true)
                {
                    this.loadWeightAvailable = loadWeightAvailable * 0.5;
                }
                else
                {
                    this.loadWeightAvailable = loadWeightAvailable * 0.9;
                }

                if (this.loadWeightAvailable >= load.loadWeight)
                {
                    load.containerName = this.containerName;
                    loadsAdded.Add(load);
                    loadWeightAvailable -= load.loadWeight;
                }
                else
                {
                   throw new OverfillException( containerName);
                }

                
            }

//Jezeli mamy poprzedni i jest niebezpieczny
            if (isPreviousLoadDangerous())
            {
                if (this.loadWeightAvailable >= load.loadWeight)
                {
                    load.containerName = this.containerName;
                    loadsAdded.Add(load);
                    loadWeightAvailable -= load.loadWeight;
                } else{throw new OverfillException( containerName);}
            }
            else //jezeli mamy poprzedni i nie jest niebezpieczny i ten obecny tez nie 
            {
                if (load.isCargoDangerous == false)
                {
                    if (this.loadWeightAvailable >= load.loadWeight)
                    {
                        load.containerName = this.containerName;
                        loadsAdded.Add(load);
                        loadWeightAvailable -= load.loadWeight;
                    }
                    else
                    {
                        throw new OverfillException( containerName);
                    }
                }
                else //jezeli mamy poprzedni i nie jest niebezpieczny ale ten obecny jest
                {
                    var maxWeightForDangerous = maxLoadWeight * 0.5;
                    if (this.loadWeightAvailable + load.loadWeight <= maxWeightForDangerous)
                    {
                        load.containerName = this.containerName;
                        loadsAdded.Add(load);
                        loadWeightAvailable -= load.loadWeight;
                    }
                    else
                    {
                        throw new OverfillException( containerName);
                    }
                }
            }
        }
        else
        {
            if (loadWeightAvailable >= load.loadWeight && load.cargoCategory.ToString() == this.GetType().Name)
            {
                load.containerName = this.containerName;
                loadsAdded.Add(load);
                loadWeightAvailable -= load.loadWeight;
            }
        }
    }

    public void unloadLoad(Load load)
    {
        loadsAdded.Remove(load);
        this.loadWeightAvailable += load.loadWeight;
       

        if (!loadsAdded.Any())
        {
            unloadAllLoads();
        }
    }

    public void unloadAllLoads()
    {
        loadsAdded.Clear();
        this.loadWeightAvailable = this.maxLoadWeight;
    }

    public bool isPreviousLoadDangerous()
    {
        foreach (var load1 in this.loadsAdded)
        {
            if (load1.isCargoDangerous == true)
            {
                return true;
            }
        }

        return false;
    }

    protected override string CreateContainerName()
    {
        objectCount++;
        return "KON-" + this.GetClassName() + "-" + objectCount;
    }

    public void Notify(string type,string containerName)
    {
        _notifier.Notify(type, containerName);
    }
    
    public override void PrintContainerInfo()
    {
        Console.WriteLine("Liquid Container");
        Console.WriteLine("Liquid Containers maxLoadWeight : "+ this.maxLoadWeight);
        Console.WriteLine("Container height:  " + this.height);
        Console.WriteLine("Container depth:  " + this.depth);

    }

}