using TMPro;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshPro crowdCountTMPro;

    private void Awake()
    {
        Player player = GetComponent<Player>();
        player.OnCrowdCountChanged += Player_OnCrowdCountChanged;
        Player_OnCrowdCountChanged(player.GetCrowdCount());
    }

    private void Player_OnCrowdCountChanged(int count)
    {
        crowdCountTMPro.text = count.ToString();
    }
}
