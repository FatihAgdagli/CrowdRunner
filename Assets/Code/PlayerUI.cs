using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro crowdCountTMPro;

    private void Awake()
    {
        GetComponent<Player>().OnCrowdCountChanged += Player_OnCrowdCountChanged; ;
    }

    private void Player_OnCrowdCountChanged(int count)
    {
        crowdCountTMPro.text = count.ToString();
    }
}
