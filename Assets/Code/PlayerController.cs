using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float forwardSpeed = 5.0f;
    [SerializeField]
    private float slideSpeed = 3.0f;

    private bool isTouched;
    private Vector3 firstTouchedPosition;
    private float deathZone = 0.1f;

    private void Update()
    {
        HandleInputTouched(out float slideDirection);

        transform.Translate( new Vector3(slideDirection * slideSpeed, 0, forwardSpeed) * Time.deltaTime );
    }

    private void HandleInputTouched(out float slideDirection)
    {
        slideDirection = 0f;

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
        }
    }
}
