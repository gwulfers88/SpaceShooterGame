/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/30/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * BossOnePassiveAttackState.cs
 * 
 * First Enemy Boss.
 * In this state the enemy will move and follow the player
 * at a slow rate and shoot at the player.
 * After being damaged for 50% he will switch to the angry state.
 * ******************************/

using UnityEngine;
using System.Collections;

public class BossOnePassiveAttackState : BossOneBaseState
{
    GameObject target;      //this is the player.
    float speed;            //speed of enemy boss.
    float changeStateLimit; //This is the percentage of health damage to change to the agressive state.

    float shootRate;
    float shootTimer;

    public BossOnePassiveAttackState(EnemyBossOne ebo)
        : base(ebo)
    {

    }

    public override void OnEnterState()
    {
        shootRate = 1.0f;
        shootTimer = 0.0f;

        target = GameObject.Find("Player_test");
        speed = enemyBoss.getSpeed() * 0.25f;
        changeStateLimit = enemyBoss.getHealth() * 0.5f;
    }

    public override void OnUpdateState()
    {
        enemyBoss.transform.position = Vector3.MoveTowards(enemyBoss.transform.position, new Vector3(target.transform.position.x, enemyBoss.transform.position.y), speed * Time.deltaTime);

        shootTimer += Time.deltaTime;

        if (shootTimer > shootRate)
        {
            enemyBoss.Shoot();
            shootTimer = 0.0f;
        }

        if(enemyBoss.getHealth() <= changeStateLimit)
        {
            ChangeToAngryState();
        }
    }
}
