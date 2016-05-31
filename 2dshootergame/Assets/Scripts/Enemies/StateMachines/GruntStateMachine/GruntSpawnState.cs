/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * GruntSpawnState.cs
 * 
 * Inherits from the base state of the grunt.
 * Spawn state of the grunt.
 * ******************************/

using UnityEngine;
using System.Collections;

public class GruntSpawnState : GruntBaseState
{
    Vector3 transitionPos;

    public GruntSpawnState(EnemyGrunt eg)
        : base(eg)
    {
        
    }

    public override void OnEnterState()
    {
        float yOffset = Random.Range(3.0f, 5.0f);
        transitionPos = new Vector3(enemyGrunt.transform.position.x, enemyGrunt.transform.position.y - yOffset, 0);
    }

    public override void OnUpdateState()
    {
        if(Vector3.Distance(enemyGrunt.transform.position, transitionPos) > 0.5f)
        {
            enemyGrunt.transform.position -= enemyGrunt.transform.up * enemyGrunt.getSpeed() * Time.deltaTime;
        }
        else
        {
            ChangeToSeekState();
        }
    }
}
