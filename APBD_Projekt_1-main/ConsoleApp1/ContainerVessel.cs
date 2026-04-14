public class ContainerVessel
{
    double maxCruiseSpeed;
    int maxNumberOfContainers;
    double maxTotalCargoWeightInTons;
    List<Container> containersOnBoard;

    public ContainerVessel(double maxCruiseSpeed, int maxNumberOfContainers, double maxTotalCargoWeightInTons,
        List<Container> containersOnBoard)
    {
        this.maxCruiseSpeed = maxCruiseSpeed;
        this.maxNumberOfContainers = maxNumberOfContainers;
        this.maxTotalCargoWeightInTons = maxTotalCargoWeightInTons;
        this.containersOnBoard = containersOnBoard;
    }

    public void LoadContainerOntoShip(Container container)
    {
        if (containersOnBoard.Count < maxNumberOfContainers &&
            getCargoWeightInTons() + container.loadWeight + container.containerWeightWithoutLoad <
            maxTotalCargoWeightInTons)
        {
            containersOnBoard.Add(container);
        }
    }

    public void LoadMultipleContainersOntoShip(List<Container> containers)
    {
        foreach (Container container in containers)
        {
            LoadContainerOntoShip(container);
        }
    }

    public void RemoveContainerFromShip(string containerName)
    {
        containersOnBoard.RemoveAll(c => c.containerName== containerName);
    }
    

    public void ReplaceContainerOnShip(string containerName, Container newContainer)
    {
        RemoveContainerFromShip(containerName);
        LoadContainerOntoShip(newContainer);
    }

    public void TransferContainerBetweenShips(ContainerVessel targetShip, string containerName)
    {
        Container container = containersOnBoard.Find(c => c.containerName == containerName);
        if (container == null)
        {
            Console.WriteLine("Cannot find container " + containerName);
            return;
        }
        RemoveContainerFromShip(containerName);
        targetShip.LoadContainerOntoShip(container);
    }

    public void PrintShipAndCargoDetails()
    {
        Console.WriteLine("Max speed: "+this.maxCruiseSpeed);
        Console.WriteLine("Max load weight: "+this.maxTotalCargoWeightInTons);
        Console.WriteLine("Max containers count: "+ this.maxNumberOfContainers);
        Console.WriteLine("Current load weight :  "+ this.getCargoWeightInTons());
        Console.WriteLine("Current containers count : "+ this.containersOnBoard.Count());
    }

    private double getCargoWeightInTons()
    {
        return containersOnBoard.Sum(c => c.containerWeightWithoutLoad + c.loadWeight);
    }
}