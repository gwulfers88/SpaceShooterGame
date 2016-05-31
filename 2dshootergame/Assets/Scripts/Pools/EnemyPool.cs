/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * EnemyPool.cs
 * 
 * This enemy pool object manages when an enemy should spawn.
 * It also manges when we should create a new enemy if the pool gets used completely.
 * ******************************/

using UnityEngine;
using System.Collections;

public class EnemyPool : ObjectPool
{
    [SerializeField]
    float spawnRate;
    float spawnTimer;

    bool spawnAllowed;

	// Use this for initialization
	void Start () 
    {
        InitializePool();
        spawnTimer = 0;
        spawnAllowed = false;
	}
	
    public bool SpawnAllowed()
    {
        return spawnAllowed;
    }

    public void SpawnAllowed(bool value)
    {
        spawnAllowed = value;
    }

	// Update is called once per frame
	void Update () 
    {
        if (spawnAllowed)
        {
            spawnTimer += Time.deltaTime;

            if (spawnTimer > spawnRate)
            {
                SpawnObject(new Vector3(Random.Range(this.transform.position.x - 3, this.transform.position.x + 3), transform.position.y, 0), this.transform.rotation);
                spawnTimer = 0;
            }
        }
	}
}
