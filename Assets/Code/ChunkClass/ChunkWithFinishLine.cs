using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChunkWithFinishLine : BaseChunk
{
    public override Component GetChunkComponent() => GetComponentInChildren<FinishLine>();
}
