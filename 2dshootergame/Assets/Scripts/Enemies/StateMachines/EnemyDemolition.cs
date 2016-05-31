/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * EnemyDemolition.cs
 * 
 * Enemy state machine for the demolition guy.
 * He creates a field of mines or bombs to disable the player.
 * ******************************/

using UnityEngine;
using System.Collections;

public class EnemyDemolition : BaseEnemy
{
    DemolitionBaseState currentState;
    [HideInInspector]
    public DemolitionSpawnState spawnState;
    [HideInInspector]
    public DemolitionSeekState seekState;
    [HideInInspector]
    public DemolitionBombState bombState;

    ObjectPool bombPool;

	// Use this for initialization
	void Start () 
    {
        bombPool = GetComponent<ObjectPool>();

        Reset();
	}

    public float getSpeed()
    {
        return speed;
    }

    protected override void Reset()
    {
        base.Reset();

        spawnState = new DemolitionSpawnState(this);
        seekState = new DemolitionSeekState(this);
        bombState = new DemolitionBombState(this);

        ChangeState(spawnState);
    }
    
    protected override void Collision()
    {
        base.Collision();

        PickupManager.Instance.SpawnPickup(transform.position);
    }

    public void SpawnBomb()
    {
        bombPool.SpawnObject(transform.position, transform.rotation);
    }

    public void ChangeState(DemolitionBaseState newState)
    {
        currentState = newState;
        currentState.OnEnterState();
    }

	// Update is called once per frame
	void Update () 
    {
        currentState.OnUpdateState();	
	}
}
