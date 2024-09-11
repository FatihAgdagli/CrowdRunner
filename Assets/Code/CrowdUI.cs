using TMPro;
using UnityEngine;

[RequireComponent(typeof(BaseCrowd))]
public class CrowdUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro crowdCountTMPro;

    private void Awake()
    {
        BaseCrowd crowd = GetComponent<BaseCrowd>();
        crowd.OnCrowdCountChanged += Player_OnCrowdCountChanged;
        Player_OnCrowdCountChanged(crowd.GetCount());
    }

    private void Player_OnCrowdCountChanged(int count)
    {
        crowdCountTMPro.text = count.ToString();
    }
}