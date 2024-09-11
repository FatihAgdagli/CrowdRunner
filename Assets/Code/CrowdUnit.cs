using System;
using UnityEngine;

public class CrowdUnit : MonoBehaviour
{
    public event EventHandler OnDie;
    public bool IsPlayer { get; set; }

    private void OnTriggerEnter(Collider other)
    {
        CrowdUnit crowdUnit = other.GetComponent<CrowdUnit>();
        if (crowdUnit == null)
        {
            return;
        }

        if (crowdUnit.IsPlayer != IsPlayer)
        {
            crowdUnit.Die();
            Die();
        }
    }

    public void Die()
    {
        OnDie?.Invoke(this, EventArgs.Empty);
        Destroy(gameObject);
    }
}
