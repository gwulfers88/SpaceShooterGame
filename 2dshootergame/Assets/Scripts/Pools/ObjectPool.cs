/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         2/28/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * ObjectPooling.cs
 * 
 * This system is used to keep track of object that are re-usable
 * for example. The bullets used by player or enemy.
 * instead of making new ones (since it can be a very expensive process) we can
 * make a few and re use them when available.
 * if we dont have any available at the moment then we can make one or two more
 * as we need them.
 * ******************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ObjectPool : MonoBehaviour
{
    #region Globals
    [SerializeField]
    private GameObject objectPrefab;    //this is the object to pool.
    [SerializeField, Range(1, 4)]
    private int poolSize;               //this is the size of the pool. (starting size must be between 1 and 4.)

    private List<GameObject> objPool;   //the actual pool of objects.
    #endregion

    /// <summary>
    /// Monobehaviour Start function
    /// </summary>
    void Start()
    {
        InitializePool();
    }

    /// <summary>
    /// Initializes the object pool based on the poolsize
    /// </summary>
    protected void InitializePool()
    {
        objPool = new List<GameObject>();

        for(int i = 0; i < poolSize; i++)
        {
            objPool.Add(Instantiate(objectPrefab)); // Create an object and store it into our pool
            objPool[i].SetActive(false);    // set that object to not active.
        }
    }

    /// <summary>
    /// Spawn object from pool list. If pool object is not available we create one and add it as needed.
    /// </summary>
    /// <param name="position">new position to spawn obj to.</param>
    /// <param name="rotation">new rotation to spawn obj in.</param>
    public void SpawnObject(Vector3 position, Quaternion rotation)
    {
        // loop through the pool of objects
        foreach ( GameObject obj in objPool )
        {
            // check to see if the objects are available and not in use
           if( obj.activeInHierarchy == false )
           {
               // if they are then get the transform
               Transform objTrans = obj.transform;

               objTrans.position = position;    // set new position
               objTrans.rotation = rotation;    // set new rotation
               obj.SetActive(true); // make active

               return;  // exit
           }
        }

        // if we reached this place in our function then we dont have any object available.
        // create a new object and add it to the list.
        objPool.Add(Instantiate(objectPrefab, position, rotation) as GameObject);
        poolSize++; // increase pool size
    }

    public virtual bool IsAnyLiving()
    {
        foreach (GameObject obj in objPool)
        {
            if (obj.activeInHierarchy == true)
            {
                return true;
            }
        }

        return false;
    }

    public virtual void KillAll()
    {
        foreach(GameObject obj in objPool)
        {
            if(obj.activeInHierarchy == true)
            {
                obj.SetActive(false);
            }
        }
    }

    /// <summary>
    /// Object Pools size
    /// </summary>
    /// <returns>the size of the object pool as integer</returns>
    public int GetPoolSize()
    {
        return poolSize;
    }
}
