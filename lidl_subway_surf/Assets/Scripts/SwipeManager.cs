using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeManager : MonoBehaviour
{

    private bool tap, swipeLeft, swipeRight, swipeUp, swipeDown;
    private bool isDraging = false;
    private Vector2 startTouch, swipeDelta;

    public Vector2 SwipeDelta { get { return swipeDelta; } }
    public bool Tap { get { return tap; } }
    public bool SwipeLeft { get { return swipeLeft; } }
    public bool SwipeRight { get { return swipeRight; } }
    public bool SwipeUp { get { return swipeUp; } }
    public bool SwipeDown { get { return swipeDown; } }

    private void Update()
    {
        if (Time.timeScale > 0)
        {
            tap = swipeLeft = swipeRight = swipeUp = swipeDown = false;

            #region Standalone Inputs
            if (Input.GetMouseButtonDown(0))
            {
                tap = true;
                isDraging = true;
                startTouch = Input.mousePosition;
            }
            else if (Input.GetMouseButtonUp(0))
            {
                isDraging = false;
                Reset();
            }
            #endregion

            #region Mobile Input
            if (Input.touches.Length > 0)
            {
                if (Input.touches[0].phase == TouchPhase.Began)
                {
                    isDraging = true;
                    tap = true;
                    startTouch = Input.touches[0].position;
                }
                else if (Input.touches[0].phase == TouchPhase.Ended || Input.touches[0].phase == TouchPhase.Canceled)
                {
                    isDraging = false;
                    Reset();
                }
            }
            #endregion

            // Calculate the distance
            swipeDelta = Vector2.zero;
            if (isDraging)
            {
                if (Input.touches.Length > 0)
                    swipeDelta = Input.touches[0].position - startTouch;
                else if (Input.GetMouseButton(0))
                    swipeDelta = (Vector2)Input.mousePosition - startTouch;
            }

            //Did we cross the distance?
            if (swipeDelta.magnitude > 30)
            {
                //Which direction?
                float x = swipeDelta.x;
                float y = swipeDelta.y;
                if (Mathf.Abs(x) > Mathf.Abs(y))
                {
                    //Left or right
                    if (x < 0)
                        swipeLeft = true;
                    else
                        swipeRight = true;
                }
                else
                {
                    // Up or down
                    if (y < 0)
                        swipeDown = true;
                    else
                        swipeUp = true;
                }
                Reset();
            }
        }
    }

    void Reset()
    {
        startTouch = swipeDelta = Vector2.zero;
        isDraging = false;
    }
}
