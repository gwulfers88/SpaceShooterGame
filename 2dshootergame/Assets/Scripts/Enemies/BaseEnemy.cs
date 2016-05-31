/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         3/29/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * BaseEnemy.cs
 * 
 * This is the base class for all enemies and other objects
 * That might take damage from the player.
 * ******************************/

using UnityEngine;
using System.Collections;

public class BaseEnemy : MonoBehaviour 
{
    /// <summary>
    /// Set this to one in order for the enemy to die with one hit!
    /// </summary>
    [SerializeField]
    protected float maxHealth;

    /// <summary>
    /// This is the rate in which the enemy moves.
    /// </summary>
    [SerializeField]
    protected float speed;

    /// <summary>
    /// What should be the score value for each enemy?
    /// </summary>
    [SerializeField]
    protected int scoreValue;

    protected float health;
    protected Transform enemyTrans;

    void OnEnable()
    {
        Reset();
    }

    /// <summary>
    /// This happens when the enemy or object 'dies'.
    /// Child class can override this.
    /// </summary>
    protected virtual void Reset()
    {
        health = maxHealth;
    }

	// Use this for initialization
	void Start () 
    {
        enemyTrans = transform;
        health = maxHealth;
	}

    /// <summary>
    /// Moves the enemy
    /// Override this in child classes to move other enemies in a different way.
    /// </summary>
    protected virtual void Move()
    {
        enemyTrans.position -= enemyTrans.up * speed * Time.deltaTime;
    }

	// Update is called once per frame
	void Update () 
    {
        Move();
	}

    /// <summary>
    /// This is called when the enemy goes off the screen.
    /// </summary>
    void OnBecameInvisible()
    {
        this.gameObject.SetActive(false);
    }

    protected virtual void Collision()
    {
        ScoreManager.Instance.Score += scoreValue;
    }

    /// <summary>
    /// Collision between objects and this enemy
    /// </summary>
    /// <param name="col"></param>
    void OnCollisionEnter2D(Collision2D col)
    {
        health--;

        if(health < 1)
        {
            Collision();

            this.gameObject.SetActive(false);
        }
    }
}
