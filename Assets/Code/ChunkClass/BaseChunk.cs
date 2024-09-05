using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseChunk : MonoBehaviour, IChunk
{
    private Transform cubeTransform;
    private Vector3 dimension;

    public virtual Component GetChunkComponent() => null;

    public virtual Vector3 GetDiemension()
    {
        if (cubeTransform == null)
        {
            cubeTransform = transform.Find("Cube");
        }
        dimension = cubeTransform.localScale;
        dimension.x *= transform.localScale.x;
        dimension.y *= transform.localScale.y;
        dimension.z *= transform.localScale.z;

        return dimension;
    }

    public GameObject GetGameObject() => gameObject;

}
