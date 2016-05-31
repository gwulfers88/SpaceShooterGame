/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/30/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * BossOneAngryAttackState.cs
 * 
 * First Enemy Boss.
 * The Angry state of the boss. 
 * This is where he will stay away from the player,
 * move faster, shoot faster, and spawn enemies.
 * ******************************/

using UnityEngine;
using System.Collections;

public class BossOneAngryAttackState : BossOneBaseState
{
    Vector3 targetPos;
    GameObject target;      //this is the player.
    float speed;            //speed of enemy boss.

    float distTimer;
    float distRate;

    float shootTimer;
    float shootRate;

    float spawnMinionRate;
    float spawnMinionTimer;

    public BossOneAngryAttackState(EnemyBossOne ebo)
        : base(ebo)
    {

    }

    public override void OnEnterState()
    {
        distTimer = 0;
        distRate = 3;

        shootRate = 0.5f;
        shootTimer = 0.0f;

        spawnMinionRate = 5;
        spawnMinionTimer = 0;

        targetPos = new Vector3(enemyBoss.transform.position.x, enemyBoss.transform.position.y + 5, 0);
        target = GameObject.Find("Player_test");
        speed = enemyBoss.getSpeed() * 0.35f;
    }

    public override void OnUpdateState()
    {
        if(distTimer < distRate)
        {
            distTimer += Time.deltaTime;
            enemyBoss.transform.position = Vector3.MoveTowards(enemyBoss.transform.position, targetPos, speed * Time.deltaTime);
        }
        else
        {
            enemyBoss.transform.position = Vector3.MoveTowards(enemyBoss.transform.position, new Vector3(target.transform.position.x, enemyBoss.transform.position.y), speed * Time.deltaTime);

            shootTimer += Time.deltaTime;
            spawnMinionTimer += Time.deltaTime;

            if(shootTimer > shootRate)
            {
                enemyBoss.Shoot();
                shootTimer = 0.0f;
            }

            if (spawnMinionTimer > spawnMinionRate)
            {
                enemyBoss.SpawnMinion();
                spawnMinionTimer = 0.0f;
            }
        }
    }
}
