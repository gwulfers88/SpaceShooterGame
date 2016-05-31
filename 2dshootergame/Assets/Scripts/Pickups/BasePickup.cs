/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/31/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * BasePickup.cs
 * 
 * This is a pickup class. Not sure if we need to
 * have a base class for this. We shall see.
 * TODO(George): Think about how we want to spawn these objects.
 * ******************************/

using UnityEngine;
using System.Collections;

public class BasePickup : MonoBehaviour
{
    public enum PICKUP_TYPE
    {
        EXTRA_LIFE = 0,
        SPEED_BOOST,
        SCORE_BOOST,
        DUAL_CANNON,
    }

    [SerializeField]
    protected float speed;
    
    [SerializeField]
    protected PICKUP_TYPE type;

	// Use this for initialization
	void Start () 
    {

    }
	
    void Update()
    {
        transform.position += -transform.up * speed * Time.deltaTime;
    }

    public PICKUP_TYPE GetPickupType()
    {
        return type;
    }

    public void SetType(PICKUP_TYPE newType)
    {
        type = newType;
    }

    void OnEnable()
    {

    }

    void OnBecameInvisible()
    {
        gameObject.SetActive(false);
    }

    void OnCollisionEnter2D(Collision2D Col)
    {
        this.gameObject.SetActive(false);
    }
}
