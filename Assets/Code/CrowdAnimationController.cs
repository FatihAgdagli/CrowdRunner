using System;
using UnityEngine;


public class CrowdAnimationController : MonoBehaviour
{
    private Animator animator;
    [SerializeField] private BaseCrowd player;
    private enum PlayerAnimation
    {
        Idle = 0,
        Run,
        Victory,
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();
        PlayRun();
    }
    private void OnDestroy()
    {
        if (player != null)
        {
            this.player.OnReachedFinishLine -= Player_OnReachedFinishLine;
            this.player.OnCrowdCountChanged -= Player_OnCrowdCountChanged;
        }
    }

    public void SetPlayer(BaseCrowd player)
    {
        this.player = player;
        this.player.OnReachedFinishLine += Player_OnReachedFinishLine;
        this.player.OnCrowdCountChanged += Player_OnCrowdCountChanged;
    }

    private void Player_OnCrowdCountChanged(int obj)
    {
        PlayRun();
    }

    private void Player_OnReachedFinishLine()
    {
        PlayVictory();
    }

    private void PlayRun()
    {
        ChangeAnimation(PlayerAnimation.Run);
    }

    private void PlayIdle()
    {
        ChangeAnimation(PlayerAnimation.Idle);
    }

    private void PlayVictory()
    {
        ChangeAnimation(PlayerAnimation.Victory);
    }

    private void ChangeAnimation(PlayerAnimation animationToPlay)
    {
        foreach (PlayerAnimation animation in Enum.GetValues(typeof(PlayerAnimation)))
        {
            animator.SetBool(animation.ToString(), (animation == animationToPlay));
        }
    }

}


