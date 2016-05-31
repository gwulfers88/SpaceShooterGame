/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * GruntAttackState.cs
 * 
 * Inherits from the base state of the grunt.
 * This is the state where the grunt shoots at the player.
 * Once he is done doing that. He returns to the seek state.
 * ******************************/

using UnityEngine;
using System.Collections;

public class GruntAttackState : GruntBaseState
{
    float shootRate;
    float shootTimer;

    public GruntAttackState( EnemyGrunt eg )
        : base (eg)
    {

    }

    public override void OnEnterState()
    {
        shootRate = 0.25f;
        shootTimer = 0;
    }

    public override void OnUpdateState()
    {
        shootTimer += Time.deltaTime;

        if (shootTimer > shootRate)
        {
            enemyGrunt.Shoot();
            
            shootTimer = 0;

            ChangeToSeekState();
        }
    }
}
