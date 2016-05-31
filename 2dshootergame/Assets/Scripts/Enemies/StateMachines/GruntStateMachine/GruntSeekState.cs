/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * GruntSeekState.cs
 * 
 * Inherits from the base state of the grunt.
 * Enemy will traverse back and forth untill he detects the player.
 * He will then change to the AttackState.
 * ******************************/

using UnityEngine;
using System.Collections;

public class GruntSeekState : GruntBaseState
{
    int dir;

    public GruntSeekState( EnemyGrunt eg )
        : base(eg)
    {

    }

    public override void OnEnterState()
    {
        dir = 1;

        //width = enemyGrunt.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    //TODO(George): Make this simpler.
    public override void OnUpdateState()
    {
        if (dir == 1)
        {
            enemyGrunt.transform.position += enemyGrunt.transform.right * enemyGrunt.getSpeed() * Time.deltaTime;
        }
        else
        {
            enemyGrunt.transform.position -= enemyGrunt.transform.right * enemyGrunt.getSpeed() * Time.deltaTime;
        }

        Vector3 enemyScreenSpace = Camera.main.WorldToScreenPoint(enemyGrunt.transform.position);

        if(enemyScreenSpace.x > Screen.width)
        {
            dir = -1;
        }
        else if(enemyScreenSpace.x < 0)
        {
            dir = 1;
        }

        RaycastHit2D hit;

        int layer = (1 << 8);

        hit = Physics2D.Raycast(enemyGrunt.transform.position, Vector2.down, 100, layer);

        if(hit)
        {
            if(hit.collider.CompareTag("Player"))
            {
                ChangeToAttackState();
            }
        }
    }
}
