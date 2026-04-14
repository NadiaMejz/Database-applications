class G : Container, IHazardNotifier
{
    public static int objectCount = 0;
    double _conainerPressure;
    private HazardNotifier _notifier = new HazardNotifier();
    private List<Load> loadsAdded = new List<Load>();
    bool wasPurged;


    public G(double loadWeight, double height, double containerWeight, double depth, double maxLoadWeight,
        double conainerPressure) : base(
        loadWeight, height, containerWeight, depth, maxLoadWeight)
    {
        this._conainerPressure = conainerPressure;
        wasPurged = true;
    }

    

    public override void PrintContainerInfo()
    {
Console.WriteLine("Gas Container");
Console.WriteLine("Gas Containers maxLoadWeight : "+ this.maxLoadWeight);
Console.WriteLine("Gas Containers pressure : "+ this._conainerPressure);
Console.WriteLine("Container height:  " + this.height);
Console.WriteLine("Container depth:  " + this.depth);

    }

    public void LoadContainer(double loadWeight, Load load)
    {
        if (load.cargoCategory.ToString() == this.GetType().Name)
        {
            if (wasPurged)
            {
                if (this.loadWeightAvailable >= load.loadWeight)
                {
                    load.containerName = this.containerName;
                    loadsAdded.Add(load);
                    loadWeightAvailable -= load.loadWeight;
                    wasPurged = false;
                }
                else
                {
                    throw new OverfillException(containerName);
                }
            }
            else
            {
               Notify("notPurged", containerName);
            }
        }
    }


    public void Purge()
    {
        this.loadWeightAvailable = this.maxLoadWeight;
        loadsAdded.Clear();
        wasPurged = true;
        Console.WriteLine($"Container {containerName} has been purged.");
    }


    public void UnloadContainer(Load load)
    {
        double remaining = load.loadWeight * 0.05;
        loadWeightAvailable += load.loadWeight * 0.95;
        loadsAdded.Remove(load);
        wasPurged = false;
        Console.WriteLine(
            $"Unloaded. 5% of the load ({remaining}kg) remains in container {this.containerName}.");
    }


    protected override string CreateContainerName()
    {
        objectCount++;
        return "KON-" + this.GetClassName() + "-" + objectCount;
    }

    public void Notify(String type, String containerName)
    {
        _notifier.Notify(type, containerName);
    }
    
}