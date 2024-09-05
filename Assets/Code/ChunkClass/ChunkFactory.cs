using UnityEngine;

public class ChunkFactory : MonoBehaviour
{
    [SerializeField] private Chunk chunk;
    [SerializeField] private ChunkWithGate chunkWithGate;

    private Vector3 instantiatePosition = Vector3.zero;
    private float firstOffset = 5f;
    private bool isFisrtCreation = true;
    

    public void ResetLevel()
    {
        instantiatePosition = Vector3.zero;
        isFisrtCreation = true;
    }

    public void CreateChunk()
    {
        Transform prefabTransform = Instantiate(chunk.transform, transform);
        prefabTransform.position = GetInstantiatePosition(chunk);

        ReCalculateInstantiatePosition(chunk);
    }

    public void CreateChunkWithGate(GateIdentifier left, GateIdentifier right)
    {
        Transform prefabTransform = Instantiate(chunkWithGate.transform, transform);
        prefabTransform.position = GetInstantiatePosition(chunkWithGate);
        Gate gate = prefabTransform.GetComponent<ChunkWithGate>().GetChunkComponent() as Gate;
        gate.SetGateIdentifiers(left, right);

        ReCalculateInstantiatePosition(chunkWithGate);
    }
    private Vector3 GetInstantiatePosition(BaseChunk baseChunk)
    {
        CheckIfFirstCreation();
        instantiatePosition.z += (baseChunk.GetDiemension().z / 2f);
        return instantiatePosition;
    }

    private void ReCalculateInstantiatePosition(BaseChunk baseChunk)
    {
        instantiatePosition.z += (baseChunk.GetDiemension().z / 2f);
    }

    private void CheckIfFirstCreation()
    {
        if (isFisrtCreation)
        {
            instantiatePosition.z -= firstOffset;
            isFisrtCreation = false;
        }
    }
}
