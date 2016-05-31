/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/07/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * SimpleTouchPad.cs
 * 
 * This is a simple touch pad for Joystick
 * movement on mobile devices.
 * ******************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchPad : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float smoothing; // How much do we want to smooth the movement?

    private Vector2 origin; // The starting position of the first touch
    private Vector2 direction;  // the direction in which the user wants to go
    private Vector2 smoothDirection;    // the smoothed direction
    private bool touched;   // if we touched the joystick or not.
    private int pointerID;  // what is the pointer ID ?

    void Awake()
    {
        direction = Vector2.zero;
        touched = false;
    }

    public void OnPointerDown( PointerEventData data )
    {
        // Set our Start point
        if( !touched )
        {
            touched = true; // we have been touched
            pointerID = data.pointerId; // get the 'finger ID' or 'stroke ID'
            origin = data.position; // Get the initial position.
        }
    }

    public void OnPointerUp( PointerEventData data )
    {
        // Reset everything
        if( data.pointerId == pointerID )
        {
            direction = Vector2.zero;
            touched = false;
        }
    }

    public void OnDrag( PointerEventData data )
    {
        // Compare the difference between the start point and the current pointer position
        if (data.pointerId == pointerID)
        {
            Vector2 currentPos = data.position; // get the current position we are dragging to
            Vector2 rawDirection = currentPos - origin; // get how far we moved. (delta position)
            direction = rawDirection.normalized;    // normalize it. between 0 and 1
        }
    }

    /// <summary>
    /// Gets the smoothed direction the user wants to go
    /// </summary>
    /// <returns></returns>
    public Vector2 GetDirection()
    {
        smoothDirection = Vector2.MoveTowards(smoothDirection, direction, smoothing);
        return smoothDirection;
    }
}
