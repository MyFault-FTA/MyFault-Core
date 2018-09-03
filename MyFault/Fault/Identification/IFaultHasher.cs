namespace MyFault.Fault.Identification
{
    public interface IFaultHasher
    {
        FaultHash GetFaultHash(Fault fault);
    }
}