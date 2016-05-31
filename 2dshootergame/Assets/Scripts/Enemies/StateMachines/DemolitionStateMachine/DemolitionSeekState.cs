/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * DemolitionSeekState.cs
 * 
 * Inherits from the base state of the demolition enemy.
 * This is the state where this enemy will seek a new position
 * to lay a bomb or a mine in.
 * ******************************/

using UnityEngine;
using System.Collections;

public class DemolitionSeekState : DemolitionBaseState
{
    Vector3 target;

    public DemolitionSeekState(EnemyDemolition ed)
        : base(ed)
    {

    }

    public override void OnEnterState()
    {
        target = new Vector3(Random.Range(-12, -8), Random.Range(-2, 3), 0);
    }

    public override void OnUpdateState()
    {
        if (Vector3.Distance(enemyDemo.transform.position, target) > 0.5f)
        {
            enemyDemo.transform.position = Vector3.MoveTowards(enemyDemo.transform.position, target, enemyDemo.getSpeed() * Time.deltaTime);
        }
        else
        {
            ChangeToBombState();
        }
    }
}
