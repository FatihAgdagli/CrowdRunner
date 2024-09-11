using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCrowd : BaseCrowd
{
    private Transform targetTransform;
    private List<CrowdUnit> targetCrowdUnitList;
    private PlayerCrowd playerCrowd;
    private float forwardSpeed = 5f;
    private float battleTimer;
    private float maxBattleTimeInSecond = 2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out PlayerCrowd playerCrowd))
        {
            this.playerCrowd = playerCrowd;
            if (playerCrowd == null)
            {
                return;
            }
            FindTargetCrowdUnits(playerCrowd.transform);
            StartCoroutine(MoveToTarget(targetTransform));
            playerCrowd.ChangeSpeedForBattle();
        }
    }

    private void FindTargetCrowdUnits(Transform targetTransform)
    {
        this.targetTransform = targetTransform;
    }

    private IEnumerator MoveToTarget(Transform c)
    {
        Vector3 dir = (targetTransform.position - crowdParent.position).normalized;

        while (playerCrowd.GetCount() != 0 && GetCount() != 0 )
        {
            crowdParent.Translate(dir * forwardSpeed * Time.deltaTime);
            battleTimer += Time.deltaTime;
            yield return new WaitForEndOfFrame();

            if (battleTimer > maxBattleTimeInSecond)
            {
                battleTimer = 0f;
                DestryCrowdUnits();
            }
        }

        if (playerCrowd == null)
        {
            yield return null;
        }
        playerCrowd.SetSpeedInitializeValue();
    }


    private void DestryCrowdUnits()
    {
        foreach (var unit in GetCrowdUnits())
        {
            unit.Die();   
        }
    }
}
