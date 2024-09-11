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
        PlayIdle();
    }
    private void OnDestroy()
    {
        if (player != null)
        {
            player.OnReachedFinishLine -= Player_OnReachedFinishLine;
            player.OnCrowdCountChanged -= Player_OnCrowdCountChanged;
            player.OnStartRunning -= Player_OnStartRunning;
        }
    }

    public void SetPlayer(BaseCrowd player)
    {
        this.player = player;
        this.player.OnReachedFinishLine += Player_OnReachedFinishLine;
        this.player.OnCrowdCountChanged += Player_OnCrowdCountChanged;
        this.player.OnStartRunning += Player_OnStartRunning;
    }

    private void Player_OnStartRunning()
    {
        PlayRun();
    }

    private void Player_OnCrowdCountChanged(int obj)
    {
        if (player.IsPlayer)
        {
            PlayRun();
        }
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


