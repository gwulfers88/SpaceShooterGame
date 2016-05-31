/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * DemolitionSpawnState.cs
 * 
 * Inherits from the base state of the demolition enemy.
 * Spawn state of the enemy.
 * ******************************/

using UnityEngine;
using System.Collections;

public class DemolitionSpawnState : DemolitionBaseState
{
    Vector3 transitionPos;

    public DemolitionSpawnState(EnemyDemolition ed)
        : base(ed)
    {

    }

    public override void OnEnterState()
    {
        float yOffset = Random.Range(3.0f, 5.0f);

        transitionPos = new Vector3(enemyDemo.transform.position.x, enemyDemo.transform.position.y - yOffset, 0);
    }

    public override void OnUpdateState()
    {
        if(Vector3.Distance(enemyDemo.transform.position, transitionPos) > 0.5f)
        {
            enemyDemo.transform.position -= enemyDemo.transform.up * enemyDemo.getSpeed() * Time.deltaTime;
        }
        else
        {
            ChangeToBombState();
        }
    }
}
