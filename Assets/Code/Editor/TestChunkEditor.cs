using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using UnityEditor.UIElements;


[CustomEditor(typeof(TestChunk))]
public class TestChunkEditor : Editor
{
    public VisualTreeAsset  VisualTree;

    private Label label;
    private PropertyField testIntField;
    private PropertyField chunkObj;

    private VisualElement visiableElements;


    public override VisualElement CreateInspectorGUI()
    {
        VisualElement root = new VisualElement();
        VisualTree.CloneTree(root);
        
        label = root.Q<Label>("TestLabel");

        testIntField = root.Q<PropertyField>("TestIntegerField");
        testIntField.RegisterCallback<ChangeEvent<int>>(OnIntFieldChanged);

        chunkObj = root.Q<PropertyField>("ChunkField");
        chunkObj.RegisterCallback<SerializedPropertyChangeEvent>(OnChunkObjChanged, TrickleDown.TrickleDown);

        visiableElements = root.Q<VisualElement>("VisiableElements");
        return root;
    }

    private void OnIntFieldChanged(ChangeEvent<int> evt)
    {     
        if (evt.newValue == 0)
        {
            return;
        }
    }

    private void OnChunkObjChanged(SerializedPropertyChangeEvent e)
    {
        BaseChunk baseChunk = e.changedProperty.boxedValue as BaseChunk;
        Debug.Log(baseChunk.GetChunkComponent());
        if (baseChunk == null || baseChunk.GetChunkComponent() == null)
        {
            visiableElements.style.display = DisplayStyle.None;
        }
        else
        {
            visiableElements.style.display = DisplayStyle.Flex;

            //Debug.Log(baseChunk.GetComponentInChunk().ToString());
            //PropertyField gateObj = visiableElements.Q<PropertyField>("GateIdentifierLeft");
            //gateObj.bindingPath = baseChunk.GetComponentInChunk().ToString();
            //visiableElements.Q<ColorField>("ColorLeft").binding = ((BaseChunk)e.changedProperty.boxedValue).GetGameObjectInChunk();
        }

    }
 }
