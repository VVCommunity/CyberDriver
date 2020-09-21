using UnityEngine;

namespace Core.Abstractions
{
    public interface IChunkSpawner
    {
        IChunk CreateNewChunkCopyUnderTheParent(GameObject parent);
    }
}
