using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 5.0f;
    [SerializeField]
    private float slideSpeed = 3.0f;

    private bool isTouched;
    private Vector3 firstTouchedPosition;
    private float deathZone = 0.1f;
    private float roadwidth = 10f;
    private float rightBoundry, leftBoundry;
    private float horizontalMovement, verticalMovement;

    private void Awake()
    {
        GetComponent<Player>().OnRightLeftBoundryChanged += Player_OnRightLeftBoundryChanged;    
    }

    private void Update()
    {
        HandleMovement();
    }

    private void HandleMovement()
    {
        float slideDirection = 0f;

        if (Input.GetMouseButtonDown(0))
        {
            isTouched = true;
            firstTouchedPosition = Input.mousePosition;
        }
        if (Input.GetMouseButtonUp(0))
        {
            isTouched = false;
        }

        if (isTouched)
        {
            float moveDifferance = (Input.mousePosition - firstTouchedPosition).normalized.x;
            if (moveDifferance < deathZone &&
                moveDifferance > -deathZone)
            {
                return;
            }

            slideDirection = moveDifferance > 0 ? 1f : -1f;

            horizontalMovement = slideDirection * slideSpeed * Time.deltaTime;
            horizontalMovement = Mathf.Clamp(horizontalMovement, leftBoundry, rightBoundry);
            verticalMovement = forwardSpeed * Time.deltaTime;

            transform.Translate(horizontalMovement, 0f, verticalMovement);
        }
    }

    private void Player_OnRightLeftBoundryChanged(float arg1, float arg2)
    {
        rightBoundry = (roadwidth / 2f) - arg1;
        leftBoundry = -(roadwidth / 2f) - arg2;
    }
}
