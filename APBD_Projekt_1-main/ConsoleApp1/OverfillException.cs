
public class OverfillException : Exception
{
    public OverfillException(String containerName)
        : base("Overfill Exception " + containerName)
    {
        
    }
}