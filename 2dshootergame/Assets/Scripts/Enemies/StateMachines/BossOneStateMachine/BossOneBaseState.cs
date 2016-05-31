/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/30/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * BossOneBaseState.cs
 * 
 * First Enemy Boss
 * This is the base state that the other states will inherit from
 * for the boss one fight.
 * ******************************/

using UnityEngine;
using System.Collections;

public class BossOneBaseState
{
    protected EnemyBossOne enemyBoss;

    public BossOneBaseState(EnemyBossOne ebo)
    {
        enemyBoss = ebo;
    }

    public virtual void OnEnterState()
    {

    }

    public virtual void OnUpdateState()
    {

    }

    protected void ChangeToSpawn()
    {
        enemyBoss.ChangeState(enemyBoss.spawnState);
    }

    protected void ChangeToPassiveState()
    {
        enemyBoss.ChangeState(enemyBoss.passiveAttackState);
    }

    protected void ChangeToAngryState()
    {
        enemyBoss.ChangeState(enemyBoss.angryAttackState);
    }
    
}
