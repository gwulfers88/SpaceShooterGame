/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * EnemyGrunt.cs
 * 
 * State Machine for the Enemy Grunts.
 * 
 * ******************************/

using UnityEngine;
using System.Collections;

public class EnemyGrunt : BaseEnemy
{
    GruntBaseState currentState;
    
    [HideInInspector]
    public GruntSpawnState spawnState;
    
    [HideInInspector]
    public GruntSeekState seekState;
    
    [HideInInspector]
    public GruntAttackState attackState;
    
    ObjectPool enemyBulletPool;

    public void ChangeState(GruntBaseState newState)
    {
        currentState = newState;
        currentState.OnEnterState();
    }

    public float getSpeed()
    {
        return speed;
    }

    protected override void Reset()
    {
        base.Reset();

        spawnState = new GruntSpawnState(this);
        seekState = new GruntSeekState(this);
        attackState = new GruntAttackState(this);

        ChangeState(spawnState);
    }

    protected override void Collision()
    {
        base.Collision();

        PickupManager.Instance.SpawnPickup(transform.position);
    }

	// Use this for initialization
	void Start () 
    {
        enemyBulletPool = GetComponent<ObjectPool>();

        Reset();
	}
	
    public void Shoot()
    {
        enemyBulletPool.SpawnObject(transform.position, transform.rotation);
    }

    void Awake()
    {
        
    }

	// Update is called once per frame
	void Update () 
    {
        currentState.OnUpdateState();
	}
}
