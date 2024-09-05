using UnityEngine;

[CreateAssetMenu(menuName ="ScriptableObjects/LevelData")]
public class LevelDesignSO : ScriptableObject
{
    public LevelManager levelManager;
    public ChunkFactory chunkFactory;
}

