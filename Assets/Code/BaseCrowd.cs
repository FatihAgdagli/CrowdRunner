using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public abstract class BaseCrowd : MonoBehaviour
{
    public event Action<int> OnCrowdCountChanged;
    public event Action OnCrowdFinished;
    public event Action OnReachedFinishLine;
    public event Action OnStartRunning;

    [Header("Spiral Parameters")]
    [SerializeField] protected float radius = .6f;
    [SerializeField] protected float angleInDegree = 137.5f;

    [Header("Crowd Parameters")]
    [SerializeField] protected Transform crowdPrefabTransform;
    [SerializeField] public int StartQuanties = 5;
    [SerializeField] protected Transform crowdParent;
    [SerializeField] protected bool isPlayer;

    protected List<Transform> crowdTransfromList = new List<Transform>();

    public bool IsPlayer => isPlayer;

    protected virtual void Awake()
    {
        crowdTransfromList.Clear();
        Addition(StartQuanties);
    }

    protected virtual void ReOrderCrowded()
    {
        float x, z;

        for (int i = 0; i < crowdTransfromList.Count; i++)
        {
            x = radius * Mathf.Sqrt(i) * Mathf.Cos(Mathf.Deg2Rad * i * angleInDegree);
            z = radius * Mathf.Sqrt(i) * Mathf.Sin(Mathf.Deg2Rad * i * angleInDegree);

            crowdTransfromList[i].localPosition = new Vector3(x, 0, z);
        }

        TriggerOnCrowdCountChangedEvent();
    }

    protected void TriggerOnReachedFinishLineEvent()
    {
        StartCoroutine(StartFinishEvent());
    }

    protected void TriggerOnCrowdCountChangedEvent()
    {
        OnCrowdCountChanged?.Invoke(crowdTransfromList.Count);
    }

    protected void TriggerOnCrowdFinishedEvent()
    {
        OnCrowdFinished?.Invoke();
    }

    protected void TriggerOnStartRunningEvent()
    {
        OnStartRunning?.Invoke();
    }

    protected void Substraction(int count)
    {
        count = count > crowdTransfromList.Count ? crowdTransfromList.Count : count;
        for (int i = 0; i < count; i++)
        {
            RemoveCrowdFromList(crowdTransfromList.Last());
        }

        if (crowdTransfromList.Count == 0)
        {
            TriggerOnCrowdFinishedEvent();
        }

        ReOrderCrowded();
    }

    protected void Addition(int count)
    {
        CrowdUnit crowdUnit;
        for (int i = 0; i < count; i++)
        {
            crowdTransfromList.Add(Instantiate(crowdPrefabTransform, crowdParent));
            CrowdAnimationController animationController = crowdTransfromList.Last().
                                                           GetComponentInChildren<CrowdAnimationController>();
            crowdUnit = crowdTransfromList.Last().GetComponent<CrowdUnit>();
            crowdUnit.IsPlayer = isPlayer;
            crowdUnit.OnDie += Crowd_OnDie;
            animationController.SetPlayer(this);

            if (!isPlayer)
            {
                crowdUnit.transform.eulerAngles = new Vector3(0f, 180f, 0f);
            }
        }
        ReOrderCrowded();
    }

    private void Crowd_OnDie(object sender, EventArgs e)
    {
        Transform transformToRemove = (sender as CrowdUnit).transform;
        RemoveCrowdFromList(transformToRemove);
    }

    private void RemoveCrowdFromList(Transform transformToRemove)
    {
        // there is no such a crowd at list
        if (!crowdTransfromList.Remove(transformToRemove))
        {
            return;
        }

        transformToRemove.GetComponent<CrowdUnit>().OnDie -= Crowd_OnDie;
        Destroy(transformToRemove.gameObject);
        TriggerOnCrowdCountChangedEvent();
    }

    private IEnumerator StartFinishEvent()
    {
        yield return new WaitForSeconds(.5f);
        OnReachedFinishLine?.Invoke();
    }

    public int GetCount() => crowdTransfromList.Count;

    public List<CrowdUnit> GetCrowdUnits()
    {
        List<CrowdUnit> crowdUnits = new List<CrowdUnit>();

        crowdTransfromList.ForEach
        (
            (crowd) => { crowdUnits.Add(crowd.GetComponent<CrowdUnit>()); }
        );

        return crowdUnits;
    }
}
