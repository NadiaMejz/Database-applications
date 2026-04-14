public class HazardNotifier
{
    public void Notify(string type, string containerName)
    {
        if (type == "Hazard")
        {
            Console.WriteLine(
                $"‼️ ALERT ‼️ A hazardous situation has occurred in container {containerName}. Immediate action required.");
        }
        else if (type == "Overload")
        {
            Console.WriteLine($"⚠️ Overload alert in container {containerName}!");
        }
        else if (type == "notPurged")
        {
            Console.WriteLine(
                $"⚠️ WARNING: Make sure the container {containerName} has been fully purged before adding new cargo. Residual materials from the previous load may remain inside.\n");
        }
        else if (type == "notCompatibleType")
        {
            Console.WriteLine(
                $"⚠️ WARNING: Incompatible products detected. Container #{containerName} already holds different product types than the incoming cargo.");
        }
    }


    public void Notify(string type)
    {
        if (type == "NotAllowed")
        {
            Console.WriteLine(
                $"⚠️ WARNING: Products in the loading list are not allowed based on the list of permitted products for container C.");
        }
        else if (type == "NotHomogeneousProductLoad")
        {
            Console.WriteLine(
                $"⚠️ WARNING: Category C containers must be loaded with products of the same type only.");
        }
    }
}