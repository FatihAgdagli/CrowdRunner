using UnityEngine;

public interface IChunk
{
    Component GetChunkComponent();
    GameObject GetGameObject();

    Vector3 GetDiemension();
}
