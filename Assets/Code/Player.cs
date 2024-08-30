using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public event Action<float, float> OnRightLeftBoundryChanged;

    [Header("Spiral Parameters")]
    [SerializeField] private float radius = 2f;
    [SerializeField] private float angleInDegree = 30f;

    [Header("Crowded Parameters")]
    [SerializeField] private Transform crowdedPrefabTransform;
    [SerializeField] private float startQuanties = 5f;
    [SerializeField] private Transform crowdedParent;

    List<Transform> crowdedTransfromList = new List<Transform>();
    private float rightBoundry;
    private float leftBoundry;

    private void Awake()
    {
        crowdedTransfromList.Clear();
        for (int i = 0; i < startQuanties; i++)
        {
            crowdedTransfromList.Add(Instantiate(crowdedPrefabTransform, crowdedParent));
        }

        ReOrderCrowded();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            ReOrderCrowded();
        }
    }


    private void ReOrderCrowded()
    {
        float x, z;
        rightBoundry = leftBoundry = 0;

        for (int i = 0; i < crowdedTransfromList.Count; i++)
        {
            x = radius * Mathf.Sqrt(i) * Mathf.Cos(Mathf.Deg2Rad * i * angleInDegree);
            z = radius * Mathf.Sqrt(i) * Mathf.Sin(Mathf.Deg2Rad * i * angleInDegree);

            rightBoundry = x > rightBoundry ? x : rightBoundry;
            leftBoundry = x < leftBoundry ? x : leftBoundry;
            crowdedTransfromList[i].localPosition = new Vector3(x, 0, z);
        }

        OnRightLeftBoundryChanged?.Invoke(rightBoundry, leftBoundry);
    }
}
