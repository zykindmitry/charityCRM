namespace DevFactoryZ.CharityCRM
{
    public interface IAmPersistent<TKey> where TKey : struct
    {
        TKey Id { get; }
        bool CanBeDeleted { get; }
    }
}
