/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         2/28/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * GameController.cs
 * 
 * This is the State of the game.
 * This object will manage the score 
 * and every other object that is not 
 * part of the player.
 * ******************************/

using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class GameController : MonoBehaviour 
{
    public Text txtScore;
    public Text txtLives;
    public Text txtTimer;
    public Text txtMessage;

    float counter;

    public Timer timer;

    float levelLengthInMinutes;
    float MinutesToSeconds;

    bool gameStart = true;
    bool bossFightReached = false;

	/// <summary>
	/// Initialzier
	/// </summary>
	void Start () 
    {
        counter = 5;

        levelLengthInMinutes = 1;
        MinutesToSeconds = 60;

        timer = GetComponent<Timer>();
        timer.StartTimer();
	}
	
    /// <summary>
    /// Update loop of the game state
    /// </summary>
	void Update () 
    {
        txtLives.text = "LIVES: " + LivesManager.Instance.Lives.ToString();
        txtTimer.text = "TIMER: " + (int)timer.GetTimeElapsed();

        if (LivesManager.Instance.Lives > 0)
        {
            timer.StartTimer();

            if (gameStart)
            {
                for (int i = 0; i < 3; ++i)
                {
                    EnemyManager.Instance.AllowSpawnFromPool(false, (EnemyManager.POOL_TYPE)i);
                }

                txtMessage.text = " " + (int)counter;
                counter -= Time.deltaTime;

                if (timer.GetTimeElapsed() > 5)
                {
                    gameStart = false;
                    
                    //enemyOne.SpawnAllowed(true);
                    EnemyManager.Instance.AllowSpawnFromPool(true, EnemyManager.POOL_TYPE.ASTEROID);

                    txtMessage.text = "";

                    timer.Reset();
                }
            }
            else
            {
                if (EnemyManager.Instance.IsAnyAlive() || timer.GetTimeElapsed() < (levelLengthInMinutes * MinutesToSeconds))
                {
                    txtScore.text = "SCORE: " + ScoreManager.Instance.Score.ToString();

                    if (timer.GetTimeElapsed() > (levelLengthInMinutes * MinutesToSeconds) * 0.2f &&
                        timer.GetTimeElapsed() < (levelLengthInMinutes * MinutesToSeconds) * 0.2f + 1)
                    {
                        //enemyGrunt.SpawnAllowed(true);
                        EnemyManager.Instance.AllowSpawnFromPool(true, EnemyManager.POOL_TYPE.GRUNT);
                    }

                    if (timer.GetTimeElapsed() > (levelLengthInMinutes * MinutesToSeconds) * 0.4f &&
                        timer.GetTimeElapsed() < (levelLengthInMinutes * MinutesToSeconds) * 0.4f + 1)
                    {
                        //enemyDemo.SpawnAllowed(true);
                        EnemyManager.Instance.AllowSpawnFromPool(true, EnemyManager.POOL_TYPE.DEMO);
                    }

                    if (timer.GetTimeElapsed() > (levelLengthInMinutes * MinutesToSeconds) * 0.5f &&
                        (timer.GetTimeElapsed() < (levelLengthInMinutes * MinutesToSeconds) * 0.5f + 6))
                    {
                        EnemyManager.Instance.AllowSpawnFromPool(false, EnemyManager.POOL_TYPE.ASTEROID);
                        EnemyManager.Instance.AllowSpawnFromPool(false, EnemyManager.POOL_TYPE.GRUNT);
                        EnemyManager.Instance.AllowSpawnFromPool(false, EnemyManager.POOL_TYPE.DEMO);

                        EnemyManager.Instance.AllowSpawnFromPool(true, EnemyManager.POOL_TYPE.BOSS_ONE);
                        bossFightReached = true;
                    }
                    else
                    {
                        EnemyManager.Instance.AllowSpawnFromPool(false, EnemyManager.POOL_TYPE.BOSS_ONE);
                    }

                    if(bossFightReached)
                    {
                        if(!EnemyManager.Instance.IsAliveInPool(EnemyManager.POOL_TYPE.BOSS_ONE))
                        {
                            
                        }
                    }
                }
                else if (!EnemyManager.Instance.IsAnyAlive())
                {
                    EnemyManager.Instance.KillAll();
                    timer.StopTimer();
                    txtMessage.text = "AREA CLEARED!";

                    Application.LoadLevel("main");
                }
            }
        }
        else
        {
            timer.StopTimer();
            txtMessage.text = "GAME OVER!";

            EnemyManager.Instance.KillAll();

            Application.LoadLevel("main");
        }
	}
}
