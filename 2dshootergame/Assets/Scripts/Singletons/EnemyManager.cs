using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EnemyManager : MonoBehaviour 
{
    private static EnemyManager instance;
    public static EnemyManager Instance
    {
        get
        {
            if(instance == null)
            {
                GameObject obj = new GameObject("EnemyManager");
                obj.AddComponent<EnemyManager>();
            }

            return instance;
        }
    }

    public enum POOL_TYPE
    {
        ASTEROID = 0,
        GRUNT,
        DEMO,
        BOSS_ONE,
    }

    [SerializeField]
    private List<EnemyPool> enemyPools;

    public void SpawnFromPool(Vector3 position, POOL_TYPE type)
    {
        enemyPools[(int)type].SpawnObject(position, Quaternion.identity);
    }

    public void AllowSpawnFromPool(bool allowed, POOL_TYPE type)
    {
        enemyPools[(int)type].SpawnAllowed(allowed);
    }

    public void KillAll()
    {
        for (int i = 0; i < enemyPools.Count; ++i)
        {
            enemyPools[i].KillAll();
        }
    }

    public bool IsAliveInPool(POOL_TYPE type)
    {
        return enemyPools[(int)type].IsAnyLiving();
    }

    public bool IsAnyAlive()
    {
        bool anyAlive = false;

        for(int i = 0; i < enemyPools.Count; ++i)
        {
            anyAlive = enemyPools[i].IsAnyLiving();
        }

        return anyAlive;
    }

	// Use this for initialization
	void Start () 
    {
        instance = this;
	}
}
