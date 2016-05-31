/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/07/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * SimpleTouchButton.cs
 * 
 * This is a simple implementation
 * for the 'button' touch events
 * ******************************/

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections;

public class SimpleTouchButton : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool touched;   // are we touched?
    private int pointerID;  // pointer ID

    /// <summary>
    /// What to do when we awake ?
    /// </summary>
    void Awake()
    {
        touched = false;    // we are not being touched yet.
    }

    /// <summary>
    /// What are we doing when we get touched/ pressed?
    /// </summary>
    /// <param name="data">Input data</param>
    public void OnPointerDown(PointerEventData data)
    {
        if (!touched)
        {
            pointerID = data.pointerId;
            touched = true;
        }
    }

    public void OnPointerUp(PointerEventData data)
    {
        if (data.pointerId == pointerID)
        {
            touched = false;
        }
    }

    /// <summary>
    /// Are we touched?
    /// </summary>
    /// <returns>true if touched, false if not.</returns>
    public bool GetTouched()
    {
        return touched;
    }
}
