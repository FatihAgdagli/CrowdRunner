using UnityEngine;

public class ChunkWithGate : BaseChunk
{
    public override Component GetChunkComponent() => GetComponentInChildren<Gate>();
}
