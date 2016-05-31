/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/30/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * EnemyBossOne.cs
 * 
 * First Enemy Boss.
 * The enemy will spawn and get close to the player.
 * Then he will Follow the player at a slow rate and attack him.
 * After he has been damaged 50% health he will change to angry attack state.
 * Then he will keep his distance from the player and will move faster.
 * Shoot faster, and spawn other enemies.
 * ******************************/

using UnityEngine;
using System.Collections;

public class EnemyBossOne : BaseEnemy
{
    BossOneBaseState currentState;
    [HideInInspector]
    public BossOneSpawnState spawnState;
    [HideInInspector]
    public BossOnePassiveAttackState passiveAttackState;
    [HideInInspector]
    public BossOneAngryAttackState angryAttackState;

    public ObjectPool enemyBulletPool;

    protected override void Reset()
    {
        base.Reset();

        spawnState = new BossOneSpawnState(this);
        passiveAttackState = new BossOnePassiveAttackState(this);
        angryAttackState = new BossOneAngryAttackState(this);

        ChangeState(spawnState);
    }

    public void ChangeState(BossOneBaseState newState)
    {
        currentState = newState;
        currentState.OnEnterState();
    }

    public void Shoot()
    {
        enemyBulletPool.SpawnObject(transform.position, transform.rotation);
    }

    public void SpawnMinion()
    {
        EnemyManager.Instance.SpawnFromPool(transform.position, EnemyManager.POOL_TYPE.DEMO);
    }

    public float getSpeed()
    {
        return speed;
    }

    public float getHealth()
    {
        return health;
    }

	// Use this for initialization
	void Start () 
    {
        Reset();
	}
	
	// Update is called once per frame
	void Update () 
    {
        currentState.OnUpdateState();
	}
}
