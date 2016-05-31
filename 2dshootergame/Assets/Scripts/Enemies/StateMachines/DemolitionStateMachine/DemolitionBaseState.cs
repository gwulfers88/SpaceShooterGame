/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * DemolitionBaseState.cs
 * 
 * This is the base state of the demolition enemy.
 * ******************************/

using UnityEngine;
using System.Collections;

public class DemolitionBaseState
{
    protected EnemyDemolition enemyDemo;

    public DemolitionBaseState(EnemyDemolition ed)
    {
        enemyDemo = ed;
    }

    public virtual void OnEnterState()
    {

    }

    public virtual void OnUpdateState()
    {

    }

    protected void ChangeToSpawnState()
    {
        enemyDemo.ChangeState(enemyDemo.spawnState);
    }

    protected void ChangeToSeekState()
    {
        enemyDemo.ChangeState(enemyDemo.seekState);
    }

    protected void ChangeToBombState()
    {
        enemyDemo.ChangeState(enemyDemo.bombState);
    }
}
