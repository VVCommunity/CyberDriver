namespace Core.Abstractions
{
    public interface IChunk : ISpawnable
    {
        float Depth { get; }

        float Z { get; }
    }
}
