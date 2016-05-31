using UnityEngine;
using System.Collections;

public class PickupManager : MonoBehaviour 
{
    private static PickupManager instance;
    public static PickupManager Instance
    {
        get 
        {
            if(instance == null)
            {
                GameObject obj = new GameObject("PickupManager");
                obj.AddComponent<PickupManager>();
            }

            return instance;
        }
    }

    [SerializeField]
    private ObjectPool SpeedBoostPool;
    [SerializeField]
    private ObjectPool ScoreBoostPool;
    [SerializeField]
    private ObjectPool ExtraLifePool;
    [SerializeField]
    private ObjectPool DualCannonPool;

    public void SpawnPickup(Vector3 position)
    {
        BasePickup.PICKUP_TYPE type;
        type = (BasePickup.PICKUP_TYPE)Random.Range(0, 6);

        switch(type)
        {
            case BasePickup.PICKUP_TYPE.EXTRA_LIFE:
                {
                    ExtraLifePool.SpawnObject(position, Quaternion.identity);
                } break;

            case BasePickup.PICKUP_TYPE.SCORE_BOOST:
                {
                    ScoreBoostPool.SpawnObject(position, Quaternion.identity);
                } break;

            case BasePickup.PICKUP_TYPE.SPEED_BOOST:
                {
                    SpeedBoostPool.SpawnObject(position, Quaternion.identity);
                } break;

            case BasePickup.PICKUP_TYPE.DUAL_CANNON:
                {
                    DualCannonPool.SpawnObject(position, Quaternion.identity);
                } break;

            default:
                break;
        }
    }

	// Use this for initialization
	void Start () 
    {
        instance = this;
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
