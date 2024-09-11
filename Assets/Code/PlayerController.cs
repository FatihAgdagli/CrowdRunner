using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerCrowd))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 5.0f;
    [SerializeField]
    private float slideSpeed = 3.0f;

    private bool isPlayerAtBattleMode;
    private bool isMovingValid = true;
    private bool isTouched;
    private Vector3 firstTouchedPosition;

    private float roadwidth = 10f;
    private float rightBoundry, leftBoundry;
    private float horizontalMovement, verticalMovement;
    private float forwardSpeedBuffer;

    private void Awake()
    {
        PlayerCrowd player = GetComponent<PlayerCrowd>();
        player.OnRightLeftBoundryChanged += Player_OnRightLeftBoundryChanged;
        player.OnReachedFinishLine += Player_OnReachedFinishLine;
    }

    private void Player_OnReachedFinishLine()
    {
        isMovingValid = false;
    }

    private void Update()
    {
        if (!isMovingValid)
        {
            return;
        }
        CalculateHorizontalMovement();
        CalculateVerticalMovement();
        HandleMovement();
    }

    private void HandleMovement()
    {
        if ((transform.position.x + horizontalMovement) > rightBoundry)
        {
            horizontalMovement = rightBoundry - transform.position.x;
        }
        else if ((transform.position.x + horizontalMovement) < leftBoundry)
        {
            horizontalMovement = leftBoundry - transform.position.x;
        }
        transform.Translate(horizontalMovement, 0f, verticalMovement);
    }

    private void CalculateVerticalMovement()
    {
        verticalMovement = forwardSpeed * Time.deltaTime;
    }

    private void CalculateHorizontalMovement()
    {
        if (isPlayerAtBattleMode)
        {
            return;
        }

        horizontalMovement = 0f;

        if (Input.GetMouseButtonDown(0) && !isTouched)
        {
            isTouched = true;
            firstTouchedPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouched = false;
        }

        if (!Input.GetMouseButton(0))
        {
            return;
        }

        horizontalMovement = Input.mousePosition.x/ Screen.width;
        horizontalMovement = Input.mousePosition.x > firstTouchedPosition.x ? 1f : -1f;
        horizontalMovement *= (slideSpeed * Time.deltaTime);
    }

    private void Player_OnRightLeftBoundryChanged(float right, float left)
    {
        rightBoundry = (roadwidth / 2f) - right;
        leftBoundry = -(roadwidth / 2f) - left;
    }

    public void ChangeSpeedForBattle()
    {
        forwardSpeedBuffer = forwardSpeed;
        forwardSpeed = 1f;
        isPlayerAtBattleMode = true;
    }

    public void SetSpeedInitializeValue()
    {
        forwardSpeed = forwardSpeedBuffer;
        isPlayerAtBattleMode = false;
    }
}
