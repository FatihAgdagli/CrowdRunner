using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine.UIElements;

[CustomEditor(typeof(LevelDesignSO))]
public class LevelDesignEditor : Editor
{
    public VisualTreeAsset VisualTree;

    private VisualElement root;
    private EnumField leftOpr, rightOpr;
    private ColorField leftColor, rightColor;
    private IntegerField leftScalar, rightScalar;
    private Button createPlayerButton, chunkButton, chunkWithGateButton;
    private GameObject chunkFactoryPrefab;
    private ChunkFactory chunkFactory;
    private LevelManager levelManager;

    public override VisualElement CreateInspectorGUI()
    {
        root = new VisualElement();
        VisualTree.CloneTree(root);

        leftColor = root.Q<ColorField>("ColorLeft");
        rightColor = root.Q<ColorField>("ColorRight");
        leftOpr = root.Q<EnumField>("OprLeft");

        rightOpr = root.Q<EnumField>("OprRight");
        leftScalar = root.Q<IntegerField>("ScalarLeft");
        rightScalar = root.Q<IntegerField>("ScalarRight");
        chunkButton = root.Q<Button>("ChunkAddButton");
        chunkButton.RegisterCallback<ClickEvent>(ChunkButtonAdd_Click);
        chunkWithGateButton = root.Q<Button>("ChunkGateAddButton");
        chunkWithGateButton.RegisterCallback<ClickEvent>(ChunkWithGateButtonAdd_Click);
        createPlayerButton = root.Q<Button>("CreatePlayerButton");
        createPlayerButton.RegisterCallback<ClickEvent>(CreatePlayerButtonAdd_Click);

        LevelDesignSO levelDesign = target as LevelDesignSO;
        chunkFactoryPrefab = levelDesign.chunkFactory.gameObject;

        return root;
    }

    private void CheckExistenceOfChunkFactory()
    {
        chunkFactory = FindObjectOfType<ChunkFactory>();
        if (chunkFactory == null)
        {
            GameObject chunkFactoryGO = Instantiate(chunkFactoryPrefab, Vector3.zero, Quaternion.identity);
            chunkFactoryGO.name = "ChunkFactory";
            chunkFactory = chunkFactoryGO.GetComponent<ChunkFactory>();
        }
    }

    private void CreatePlayerButtonAdd_Click(ClickEvent e)
    {
        if (levelManager != null)
        {
            return;
        }

        levelManager = FindObjectOfType<LevelManager>();
        if (levelManager == null)
        {
            LevelDesignSO levelDesign = target as LevelDesignSO;
            GameObject levelManagerGO = Instantiate(levelDesign.levelManager.gameObject, 
                                                    Vector3.zero, Quaternion.identity);

            levelManager = levelManagerGO.GetComponent<LevelManager>();
            levelManager.CreateCameraAndPlayer();
        }
    }

    private void ChunkButtonAdd_Click(ClickEvent e)
    {
        CheckExistenceOfChunkFactory();
        chunkFactory.CreateChunk();
    }

    private void ChunkWithGateButtonAdd_Click(ClickEvent e)
    {
        CheckExistenceOfChunkFactory();

        GateIdentifier left = new GateIdentifier(leftColor.value, (GateOper)leftOpr.value, leftScalar.value);
        GateIdentifier right = new GateIdentifier(rightColor.value, (GateOper)rightOpr.value, rightScalar.value);
        chunkFactory.CreateChunkWithGate(left, right);
    }
}
