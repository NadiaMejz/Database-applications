class ConsoleApp1
{
    static void Main(string[] args)
    {
        Product helium = new Product("Hel", 'G');
        Product milk = new Product("Mleko", 'L', false);
        Product fuel = new Product("Paliwo", 'L', true);
        Product bananas = new Product("Banany", 'C', false);

        Product banany = new Product("Banany", 13.3);
        Product czekolada = new Product("Czekolada", 18);
        Product ryby = new Product("Ryby", 2);
        Product mięso = new Product("Mięso", -15);
        Product lody = new Product("Lody", -18);
        Product mrożonaPizza = new Product("Mrożona pizza", -3);
        Product ser = new Product("Ser", 7.2);
        Product kiełbasy = new Product("Kiełbasy", 5);
        Product masło = new Product("Masło", 20.5);
        Product jajka = new Product("Jajka", 19);

        Product.allowedProductType.Add(banany);
        Product.allowedProductType.Add(czekolada);
        Product.allowedProductType.Add(ryby);
        Product.allowedProductType.Add(mięso);
        Product.allowedProductType.Add(lody);
        Product.allowedProductType.Add(mrożonaPizza);
        Product.allowedProductType.Add(ser);
        Product.allowedProductType.Add(kiełbasy);
        Product.allowedProductType.Add(masło);
        Product.allowedProductType.Add(jajka);

        Load gasLoad = new Load('G');
        gasLoad.AddProductToCargo(helium, 100);

        Load milkLoad = new Load('L');
        milkLoad.AddProductToCargo(milk, 200);

        Load fuelLoad = new Load('L');
        fuelLoad.AddProductToCargo(fuel, 150);

        Load bananaLoad = new Load('C');
        bananaLoad.AddProductToCargo(bananas, 300);

        G gasContainer = new G(0, 250, 4000, 300, 500, 2.5);
        L liquidContainer = new L(0, 250, 3500, 300, 500);
        C coolingContainer = new C(0, 250, 4500, 300, 500, 4.0);

        try
        {
            Console.WriteLine("\nŁadowanie kontenera gazowego (pierwsze ładowanie)...");
            gasContainer.LoadContainer(100, gasLoad);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            Console.WriteLine("\nPonowne ładowanie kontenera gazowego bez opróżnienia...");
            gasContainer.LoadContainer(100, gasLoad);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }

        Console.WriteLine("\nOpróżnianie kontenera gazowego (Purge)...");
        gasContainer.Purge();
        try
        {
            Console.WriteLine("Ładowanie kontenera gazowego po opróżnieniu...");
            gasContainer.LoadContainer(100, gasLoad);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            Console.WriteLine("\nŁadowanie kontenera płynnego ładunkiem mleka...");
            liquidContainer.LoadContainer(200, milkLoad);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            Console.WriteLine("Ładowanie kontenera płynnego ładunkiem paliwa...");
            liquidContainer.LoadContainer(150, fuelLoad);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }

        try
        {
            Console.WriteLine("\nŁadowanie kontenera chłodniczego ładunkiem bananów...");
            coolingContainer.LoadContainer(bananaLoad, 4.0);
        }
        catch (OverfillException e)
        {
            Console.WriteLine(e.Message);
        }

        List<Container> initialContainers = new List<Container>();
        ContainerVessel ship1 = new ContainerVessel(30, 10, 2000, initialContainers);

        Console.WriteLine("\nZaładowanie kontenerów na statek 1...");
        ship1.LoadContainerOntoShip(gasContainer);
        ship1.LoadContainerOntoShip(liquidContainer);
        ship1.LoadContainerOntoShip(coolingContainer);

        Console.WriteLine("\nStan statku 1 po załadunku:");
        ship1.PrintShipAndCargoDetails();

        Console.WriteLine("\nUsuwanie kontenera płynnego ze statku 1...");
        ship1.RemoveContainerFromShip(liquidContainer.containerName);
        ship1.PrintShipAndCargoDetails();

        Console.WriteLine("\nZastępowanie kontenera chłodniczego na statku 1...");
        C newCoolingContainer = new C(0, 260, 4600, 310, 550, 5.0);
        ship1.ReplaceContainerOnShip(coolingContainer.containerName, newCoolingContainer);
        ship1.PrintShipAndCargoDetails();

        List<Container> secondShipContainers = new List<Container>();
        ContainerVessel ship2 = new ContainerVessel(25, 8, 1500, secondShipContainers);

        Console.WriteLine("\nTransfer kontenera gazowego ze statku 1 do statku 2...");
        ship1.TransferContainerBetweenShips(ship2, gasContainer.containerName);

        Console.WriteLine("\nStan statku 1 po transferze:");
        ship1.PrintShipAndCargoDetails();
        Console.WriteLine("\nStan statku 2 po transferze:");
        ship2.PrintShipAndCargoDetails();

        Console.WriteLine("\nInformacje o poszczególnych kontenerach:");
        gasContainer.PrintContainerInfo();
        liquidContainer.PrintContainerInfo();
        newCoolingContainer.PrintContainerInfo();

        Console.WriteLine("\nDemo zakończone. Naciśnij dowolny klawisz, aby zakończyć.");
    }
}
