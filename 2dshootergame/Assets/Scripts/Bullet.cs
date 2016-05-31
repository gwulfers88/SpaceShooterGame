/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/07/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * Bullet.cs
 * 
 * This is the player bullet class.
 * We can inherit from this class for
 * enemies to use bullets.
 * ******************************/

using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour 
{
    [SerializeField]
    private float speed;    // speed of the bullet
    [SerializeField]
    private Rigidbody2D rb; // rigidBody used to add acceleration to the object.

    private Transform bulletTrans;  // transform
    
	/// <summary>
	/// Initialize 
	/// </summary>
	void Start () 
    {
        bulletTrans = this.transform;   // get the transform
	}
	
    /// <summary>
    /// Called when ever we get set to active by the object pool system
    /// </summary>
    void OnEnable()
    {
        bulletTrans = this.transform;   // we need to re aquire this every time.
        rb.AddForce(bulletTrans.up * speed, ForceMode2D.Force);  // add force to bullet
    }

    /// <summary>
    /// Called when bullet goes off the screen.
    /// Give back to Object Pool to be reused.
    /// </summary>
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    /// <summary>
    /// Collision. It should set inactive (die)
    /// when it collides with anything.
    /// </summary>
    /// <param name="Col">Other object that we collided with</param>
    void OnCollisionEnter2D(Collision2D Col)
    {
        if(gameObject.CompareTag("EnemyBullet"))
        {
            ExplosionManager.Instance.SpawnExplosion(transform.position, ExplosionManager.EXPLOSION_TYPE.RED_LASER);
        }
        else if (gameObject.CompareTag("PlayerBullet"))
        {
            ExplosionManager.Instance.SpawnExplosion(transform.position, ExplosionManager.EXPLOSION_TYPE.BLUE_LASER);
        }

        this.gameObject.SetActive(false);
    }
}
