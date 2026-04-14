public abstract class Container

{
    public double loadWeight { get; set; }
    protected double height;
    public double containerWeightWithoutLoad { get; set; }
    protected double depth;
    protected double maxLoadWeight;
    public string containerName;
    protected int objectCount;
    protected double loadWeightAvailable;

    public Container(double loadWeight, double height, double containerWeightWithoutLoad, double depth,
        double maxLoadWeight)
    {
        this.loadWeight = loadWeight;
        this.height = height;
        this.containerWeightWithoutLoad = containerWeightWithoutLoad;
        this.depth = depth;
        this.maxLoadWeight = maxLoadWeight;
        containerName = CreateContainerName();
        loadWeightAvailable = this.maxLoadWeight;
    }

    protected abstract string CreateContainerName();
    public abstract void  PrintContainerInfo();

    public virtual void UnloadContainer(double loadWeight)
    {
        maxLoadWeight = maxLoadWeight + loadWeight;
    }


    public virtual void LoadContainer(double loadWeight)
    {
        if (loadWeight <= this.maxLoadWeight)
        {
            maxLoadWeight = maxLoadWeight - loadWeight;
        }
        else
        {
            OverfillException("load weight is greater than maxLoadWeight");
        }
    }

    public void OverfillException(string message)
    {
        Console.WriteLine(message);
    }


    protected virtual string GetClassName()
    {
        string className = this.GetType().Name;
        return className;
    }
}