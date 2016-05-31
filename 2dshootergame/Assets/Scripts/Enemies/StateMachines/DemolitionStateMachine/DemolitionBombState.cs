/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * DemolitionBombState.cs
 * 
 * Inherits from the base state of the demolition enemy.
 * Bomb state of the enemy. Spawns bombs or mines.
 * ******************************/

using UnityEngine;
using System.Collections;

public class DemolitionBombState : DemolitionBaseState
{
    float bombRate;
    float bombTimer;

    public DemolitionBombState(EnemyDemolition ed)
        : base(ed)
    {

    }

    public override void OnEnterState()
    {
        bombRate = 0.5f;
        bombTimer = 0;
    }

    public override void OnUpdateState()
    {
        bombTimer += Time.deltaTime;

        if (bombTimer > bombRate)
        {
            bombTimer = 0;

            enemyDemo.SpawnBomb();

            ChangeToSeekState();
        }
    }
}
