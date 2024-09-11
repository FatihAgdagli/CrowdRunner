using UnityEngine;

public class ChunkWithEnemy : BaseChunk
{
    public override Component GetChunkComponent() => GetComponent<EnemyCrowd>();
}

