using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ExplosionManager : MonoBehaviour
{
    private static ExplosionManager instance;
    public static ExplosionManager Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject obj = new GameObject("ExplosionManager");
                obj.AddComponent<ExplosionManager>();
            }

            return instance;
        }
    }

    public enum EXPLOSION_TYPE
    {
        BLUE_LASER = 0,
        RED_LASER,
    }

    [SerializeField]
    private List<ObjectPool> explosionPool;

    public void SpawnExplosion(Vector3 position, EXPLOSION_TYPE type)
    {
        explosionPool[(int)type].SpawnObject(position, Quaternion.identity);
    }

	// Use this for initialization
	void Start () 
    {
        instance = this;
	}
	
}
