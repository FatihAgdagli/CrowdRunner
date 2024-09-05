using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Player : MonoBehaviour, IInteractable
{
    public event Action<float, float> OnRightLeftBoundryChanged;
    public event Action<int> OnCrowdCountChanged;
    public event Action OnCrowdFinished;
    public event Action OnReachedFinishLine;

    [Header("Spiral Parameters")]
    [SerializeField] private float radius = 2f;
    [SerializeField] private float angleInDegree = 30f;

    [Header("Crowd Parameters")]
    [SerializeField] private Transform crowdPrefabTransform;
    [SerializeField] private int startQuanties = 5;
    [SerializeField] private Transform crowdParent;

    List<Transform> crowdTransfromList = new List<Transform>();
    private float rightBoundry;
    private float leftBoundry;

    private void Awake()
    {
        crowdTransfromList.Clear();
        Addition(startQuanties);
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

        for (int i = 0; i < crowdTransfromList.Count; i++)
        {
            x = radius * Mathf.Sqrt(i) * Mathf.Cos(Mathf.Deg2Rad * i * angleInDegree);
            z = radius * Mathf.Sqrt(i) * Mathf.Sin(Mathf.Deg2Rad * i * angleInDegree);

            rightBoundry = x > rightBoundry ? x : rightBoundry;
            leftBoundry = x < leftBoundry ? x : leftBoundry;
            crowdTransfromList[i].localPosition = new Vector3(x, 0, z);
        }

        OnRightLeftBoundryChanged?.Invoke(rightBoundry, leftBoundry);
        OnCrowdCountChanged?.Invoke(crowdTransfromList.Count);
    }

    private void Substraction(int count)
    {
        count = count > crowdTransfromList.Count ? crowdTransfromList.Count : count;
        for (int i = 0; i < count; i++)
        {
            Destroy(crowdTransfromList.Last().gameObject);
            crowdTransfromList.RemoveAt(crowdTransfromList.Count - 1);
        }

        if (crowdTransfromList.Count == 0)
        {
            OnCrowdFinished?.Invoke();
        }
 
        ReOrderCrowded();
    }

    private void Addition(int count)
    {
        for (int i = 0; i < count; i++)
        {
            crowdTransfromList.Add(Instantiate(crowdPrefabTransform, crowdParent));
            CrowdAnimationController animationController = crowdTransfromList[i].GetComponentInChildren<CrowdAnimationController>();
            animationController.SetPlayer(this);
        }
        ReOrderCrowded();
    }

    public void Interact(GateIdentifier gateIdentifier)
    {
        int count = gateIdentifier.factor;
        switch (gateIdentifier.operation)
        {
            case GateOper.Add:
                Addition(count);
                break;
            case GateOper.Sub:
                Substraction(count);
                break;
            case GateOper.Multiply:
                count = gateIdentifier.factor * crowdTransfromList.Count;
                Addition(count);
                break;
            case GateOper.Divide:
                count = Mathf.CeilToInt(((float)crowdTransfromList.Count) / gateIdentifier.factor);
                Substraction(count);
                break;
        }
    }

    public void Interact(FinishLine finishLine)
    {
        OnReachedFinishLine?.Invoke();
    }

    public int GetCrowdCount() => crowdTransfromList.Count;
}
