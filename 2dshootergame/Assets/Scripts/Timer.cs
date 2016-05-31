/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/31/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * Timer.cs
 * 
 * You can use this class to time events.
 * ******************************/

using UnityEngine;
using System.Collections;

public class Timer : MonoBehaviour 
{
    float timer;
    bool started;

	// Use this for initialization
	void Start () 
    {
        timer = 0;
        started = false;
	}
	
    public void StartTimer()
    {
        started = true;
    }

    public void StopTimer()
    {
        started = false;
    }

    public void Reset()
    {
        timer = 0;
    }

    public float GetTimeElapsed()
    {
        return timer;
    }

	// Update is called once per frame
	void Update () 
    {
	    if(started)
        {
            timer += Time.deltaTime;
        }
	}
}
