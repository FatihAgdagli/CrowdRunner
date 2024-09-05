using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Gate : MonoBehaviour
{
    [SerializeField] private GameObject leftGameObject, rightGameObject;
    [SerializeField] private GateIdentifier leftIdentifier, rightIdentifier;

    private bool isItFirstCollide;

    private void Awake()
    {
        GateCollider leftCollider = leftGameObject.GetComponent<GateCollider>();
        leftCollider.SetGateIdentifier(leftIdentifier);   
        leftCollider.OnCollide += Gate_OnCollide;

        GateCollider rightCollider = rightGameObject.GetComponent<GateCollider>();
        rightCollider.SetGateIdentifier(rightIdentifier); 
        rightCollider.OnCollide += Gate_OnCollide;

        UpdateUI();

        isItFirstCollide = false;
    }

    public void SetGateIdentifiers(GateIdentifier leftIdentifier, GateIdentifier rightIdentifier)
    {
        this.leftIdentifier = leftIdentifier;
        this.rightIdentifier = rightIdentifier;

        UpdateUI();
    }

    private void Gate_OnCollide(GateIdentifier gateIdentifier, Collider other)
    {
        if (isItFirstCollide)
        {
            return;
        }

        if (other.TryGetComponent(out IInteractable interactable))
        {
            isItFirstCollide = true;
            interactable.Interact(gateIdentifier);
        }
    }

    private void UpdateUI()
    {
        leftGameObject.GetComponent<SpriteRenderer>().color = leftIdentifier.color;
        leftGameObject.GetComponentInChildren<TextMeshPro>().text = leftIdentifier.ToString();

        rightGameObject.GetComponent<SpriteRenderer>().color = rightIdentifier.color;
        rightGameObject.GetComponentInChildren<TextMeshPro>().text = rightIdentifier.ToString();
    }
}
