/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * GruntBaseState.cs
 * 
 * Base state of all of the grunt states.
 * ******************************/

using UnityEngine;
using System.Collections;

public class GruntBaseState
{
    protected EnemyGrunt enemyGrunt;

    public GruntBaseState(EnemyGrunt eg)
    {
        enemyGrunt = eg;
    }

    public virtual void OnEnterState()
    {

    }

    public virtual void OnUpdateState()
    {

    }

    public void ChangeToSpawnState()
    {
        enemyGrunt.ChangeState(enemyGrunt.spawnState);
    }

    public void ChangeToSeekState()
    {
        enemyGrunt.ChangeState(enemyGrunt.seekState);
    }

    public void ChangeToAttackState()
    {
        enemyGrunt.ChangeState(enemyGrunt.attackState);
    }
}
