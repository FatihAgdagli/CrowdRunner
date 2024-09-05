using System;
using UnityEngine;

public class GateCollider : MonoBehaviour
{
    public event Action<GateIdentifier, Collider> OnCollide;


    private GateIdentifier gateIdentifier;

    private void OnTriggerEnter(Collider other)
    {
        OnCollide?.Invoke(gateIdentifier, other);
    }

    public void SetGateIdentifier(GateIdentifier gateIdentifier)
    {
        this.gateIdentifier = gateIdentifier;
    }
}