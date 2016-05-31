/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/30/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * BossOneSpawnState.cs
 * 
 * First Enemy Boss.
 * This is what happens once the enemy spawns for the first time.
 * He will get close to the player and then change to the passive attack state
 * ******************************/

using UnityEngine;
using System.Collections;

public class BossOneSpawnState : BossOneBaseState
{
    Vector3 target;
    float speed;

    public BossOneSpawnState(EnemyBossOne ebo)
        : base(ebo)
    {

    }

    public override void OnEnterState()
    {
        speed = enemyBoss.getSpeed();
        target = new Vector3(enemyBoss.transform.position.x, enemyBoss.transform.position.y - 6, 0);
    }

    public override void OnUpdateState()
    {
        if(Vector3.Distance(enemyBoss.transform.position, target) > 0.5f)
        {
            enemyBoss.transform.position = Vector3.MoveTowards(enemyBoss.transform.position, target, speed * Time.deltaTime);
        }
        else
        {
            ChangeToPassiveState();
        }
    }
}
