/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/30/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * ScoreManager.cs
 * 
 * This keeps track of the players Score.
 * There can only be one of these objects at all times.
 * ******************************/

using UnityEngine;
using System.Collections;

public class ScoreManager : MonoBehaviour 
{
    private static ScoreManager instance;
    public static ScoreManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ScoreManager");
                obj.AddComponent<ScoreManager>();
            }

            return instance;
        }
    }

    private int score;
    public int Score
    {
        set
        {
            score = value;
        }

        get
        {
            return score;
        }
    }

    void Start()
    {
        instance = this;
    }
}
