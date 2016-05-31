using UnityEngine;
using System.Collections;

public class Level_one : MonoBehaviour 
{
    int stage = 0;
    const int MAX_STAGES = 3;

    int level = 1;
    int maxNumberOfEnemies = 10;

    public static bool cleared = false;

    private bool bossFight;
    public bool BossFight
    {
        set { bossFight = value; }
        get { return bossFight; }
    }

    /// <summary>
    /// The start of the stage
    /// </summary>
    void StageStart()
    {
        // TODO: LET THE PLAYER KNOW WE ARE IN STAGE #.
    }

    /// <summary>
    /// Reset stage
    /// </summary>
    void ResetStage()
    {
        // TODO: RESET STAGE
    }

    /// <summary>
    /// Spawn enemies
    /// </summary>
    void StageSpawnEnemies()
    {
        // TODO: SPAWN WAVES OF ENEMIES.
    }

    /// <summary>
    /// Boss fight
    /// </summary>
    void StageBossFight()
    {
        // TODO: BOSS FIGHT
    }

    /// <summary>
    /// stage end
    /// </summary>
    void StageEnd()
    {
        // TODO: LET THE PLAYER KNOW IT HAS BEEN CLEARED.
    }

    /// <summary>
    /// Transition to next stage
    /// </summary>
    void StageTransition()
    {
        stage++;

        if (stage <= maxNumberOfEnemies)
        {
            ResetStage();
        }
        else
        {
            // TODO: Change to world view.
        }
    }
}
