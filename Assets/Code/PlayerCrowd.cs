using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerCrowd : BaseCrowd, IInteractable
{
    public event Action<float, float> OnRightLeftBoundryChanged;

    private float rightBoundry;
    private float leftBoundry;
    PlayerController playerController;

    protected override void Awake()
    {
        base.Awake();
        playerController = GetComponent<PlayerController>();
    }

    protected override void ReOrderCrowded()
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
        TriggerOnCrowdCountChangedEvent();
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
        TriggerOnReachedFinishLineEvent();
    }

    public void ChangeSpeedForBattle()
    {
        playerController.ChangeSpeedForBattle();
    }

    public void SetSpeedInitializeValue()
    {
        playerController.SetSpeedInitializeValue();
    }
}
