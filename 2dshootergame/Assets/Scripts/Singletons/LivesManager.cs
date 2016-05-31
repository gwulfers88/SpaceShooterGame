/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/31/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * LivesManager.cs
 * 
 * This keeps track of the players lives.
 * There can only be one of these objects at all times.
 * ******************************/

using UnityEngine;
using System.Collections;

public class LivesManager : MonoBehaviour
{
    private static LivesManager instance;
    public static LivesManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject("LivesManager");
                obj.AddComponent<LivesManager>();
            }

            return instance;
        }
    }

    private int lives;
    public int Lives
    {
        get { return lives; }
        set { lives = value; }
    }

	// Use this for initialization
	void Start () 
    {
        instance = this;
        lives = 5;
	}
}
