/* ******************************
 * 2DSpaceShooter
 * ******************************
 * programmer:      George Wulfers
 * Created:         2/28/2016
 * collaborators:   (other who contributed to writing this system).
 * ******************************
 * PlayerController.cs
 * 
 * This is the player controller that lets
 * the user control the player.
 * Shoot, move or whatever the mechanics
 * are required for this game.
 * ******************************/

using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour 
{
    [SerializeField, Range(0, 10)]
    private float speed;                // Speed of the character
    private float regularSpeed;

    // TODO(George): Centralize weapons ??
    bool dualCannon;
    int dualAmmo;

    bool speedBoost;
    float speedBoostTimer;
    float speedBoostLimit;
    
    [SerializeField]
    private SimpleTouchPad touchPad;    // Touchpad input
    [SerializeField]
    private SimpleTouchButton touchButtonShoot;
    [SerializeField]
    private float shootRate;
    private float shootTimer;
    private float width;
    private float height;

    private Transform playerTrans;      // playerTransform
    private Vector3 startingPos;        // starting position.
    private Rigidbody2D rb;             // Rigid body 2D
    private float hInput;               // horizontal Input (mobile).
    private ObjectPool bulletPool;      // bullet pool

    private Quaternion calibrationQuaternion;

	/// <summary>
	/// Initialize this object
	/// </summary>
	void Start () 
    {
        dualCannon = true;
        dualAmmo = 0;

        regularSpeed = speed;
        speedBoost = false;
        speedBoostLimit = 4;
        speedBoostTimer = 0;

        CalibrateAccelerometer();

        bulletPool = GetComponent<ObjectPool>(); // Get the ObjectPool system

        playerTrans = transform;    // lets get the player transform
        startingPos = playerTrans.position; // lets set the starting position.
        rb = GetComponent<Rigidbody2D>();   // lets get the rigid body

        //get the width and the height of the sprite
        width = GetComponent<SpriteRenderer>().bounds.extents.x;
        height = GetComponent<SpriteRenderer>().bounds.extents.y;
	}
	
    void SpeedBoost()
    {
        if(speedBoost)
        {
            speedBoostTimer += Time.deltaTime;
            if(speedBoostTimer > speedBoostLimit)
            {
                speedBoost = false;
                speed = regularSpeed;
                speedBoostTimer = 0;
            }
        }
    }

	/// <summary>
	/// Update loop for this player
	/// </summary>
	void Update () 
    {
        if (LivesManager.Instance.Lives != 0)
        {
            shootTimer += Time.deltaTime; // Step timer

            SpeedBoost();

            if(dualAmmo < 1)
            {
                dualCannon = false;
            }

            //Make sure we dont go out of the screen
            CheckBoundaries(new Rect(0, 0, Screen.width, (Screen.height * 0.5f)));

            // NOTE: This block can be deleted for shipping version if we dont want to
            // release a PC version.
#if UNITY_STANDALONE_WIN || UNITY_EDITOR    // compile for editor and windows

            // If we hit the left ctrl btn or left mouse button
            if (Input.GetButtonDown("Fire1") && shootTimer > shootRate)
            {
                Shoot();    // we shoot
                
                shootTimer = 0; // reset timer
            }

            // Move player left to right, up and down
            Move(Input.GetAxis("Horizontal"), playerTrans.right);
            Move(Input.GetAxis("Vertical"), playerTrans.up);

#elif UNITY_ANDROID     // compile for android
        
            // Move the player if the hInput is not 0
            Vector2 direction = touchPad.GetDirection(); // Get the direction we want to go from the touchPad
            Vector3 movement = new Vector3(direction.x, direction.y, 0.0f);    // Only move on the X direction.
            Move(movement); // Move player

            // if we want to shoot and we have reached the shoot rate
            if(touchButtonShoot.GetTouched() && shootTimer > shootRate)
            {
                Shoot();    // shoot
                shootTimer = 0; // reset timer
            }
#endif
        }
    }

    /// <summary>
    /// Shoots a bullet from our object pool
    /// </summary>
    public void Shoot()
    {
        // TODO(George): Should we add a delay for the shooting rate? or is this fine as it is?
        if (dualCannon)
        {
            Vector3 right = new Vector3(playerTrans.position.x + 0.25f, playerTrans.position.y);
            Quaternion rightRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, 10.0f));

            Vector3 left = new Vector3(playerTrans.position.x - 0.25f, playerTrans.position.y);
            Quaternion leftRotation = Quaternion.Euler(new Vector3(0.0f, 0.0f, -10.0f));

            bulletPool.SpawnObject(right, playerTrans.rotation * rightRotation);
            bulletPool.SpawnObject(playerTrans.position, playerTrans.rotation);
            bulletPool.SpawnObject(left, playerTrans.rotation * leftRotation);

            dualAmmo--;
        }
        else
        {
            bulletPool.SpawnObject(playerTrans.position, playerTrans.rotation);
        }
    }

    /// <summary>
    /// Moves the player Left or right
    /// </summary>
    /// <param name="value">1: Moves right, -1: moves left, 0: doesnt move</param>
    void Move(float value, Vector3 direction)
    {
        // scale the player position based on the speed and the value and the time elapsed on the right or left direction
        playerTrans.position += (direction * value * speed * Time.deltaTime);
    }

    /// <summary>
    /// Moves the player Left or right.
    /// Used with the SimpleTouchPad on Mobile.
    /// </summary>
    /// <param name="moveDirection">movement direction(Vector3)</param>
    void Move(Vector3 moveDirection)
    {
        // scale the player position based on the speed and the value and the time elapsed on the right or left direction
        playerTrans.position += (moveDirection * speed * Time.deltaTime);
    }

    /// <summary>
    /// For use on mobile only with SimpleTouchButton.
    /// </summary>
    /// <param name="horizInput">It is set to one by the buttons or -1 to move right or left respectively.</param>
    public void StartMoving(float horizInput)
    {
        hInput = horizInput;
    }

    /// <summary>
    /// If we are not on screen. We call this function to reset
    /// </summary>
    void OnBecameInvisible()
    {
        // TODO(George): Do something interesting for the player to reset its position.
        // Should GameManager do this bit and manage the player as well?
        ResetPosition();
    }

    /// <summary>
    /// Collision 2D
    /// </summary>
    /// <param name="collision">Other object</param>
    void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO(George): We can add damage code here. when we hit enemies or something interesting.
        if (collision.collider.CompareTag("Pickup"))
        {
            BasePickup.PICKUP_TYPE type = collision.collider.gameObject.GetComponent<BasePickup>().GetPickupType();

            switch(type)
            {
                case BasePickup.PICKUP_TYPE.EXTRA_LIFE:
                    {
                        LivesManager.Instance.Lives += 1;
                    } break;

                case BasePickup.PICKUP_TYPE.SCORE_BOOST:
                    {
                        ScoreManager.Instance.Score += Random.Range(50, 100);
                    } break;

                case BasePickup.PICKUP_TYPE.SPEED_BOOST:
                    {
                        if (!speedBoost)
                        {
                            speed += (speed * Random.Range(0.5f, 1f));
                            speedBoost = true;
                        }
                    } break;

                case BasePickup.PICKUP_TYPE.DUAL_CANNON:
                    {
                        dualCannon = true;
                        dualAmmo += Random.Range(10, 15);
                    }break;
            }
        }
        else
        {
            if (LivesManager.Instance.Lives > 0)
            {
                LivesManager.Instance.Lives--;
            }
        }
    }

    /// <summary>
    /// Reset the player position.
    /// </summary>
    void ResetPosition()
    {
        CalibrateAccelerometer(); // recalibrate on restart
        playerTrans.position = startingPos; // reset position
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="min"></param>
    /// <param name="max"></param>
    void CheckBoundaries(Rect bounds)
    {
        Vector2 WorldToScreen = Camera.main.WorldToScreenPoint(playerTrans.position);
        float Offset = speed * Time.deltaTime;

        //Check horizontal
        if(WorldToScreen.x - width < bounds.x)
        {
            playerTrans.position = new Vector3(playerTrans.position.x + Offset, playerTrans.position.y, 0);
        }
        else if(WorldToScreen.x + width > bounds.width)
        {
            playerTrans.position = new Vector3(playerTrans.position.x - Offset, playerTrans.position.y, 0);
        }

        //Check vertical
        if (WorldToScreen.y - height < bounds.y)
        {
            playerTrans.position = new Vector3(playerTrans.position.x, playerTrans.position.y + Offset, 0);
        }
        else if (WorldToScreen.y + height > bounds.height)
        {
            playerTrans.position = new Vector3(playerTrans.position.x, playerTrans.position.y - Offset, 0);
        }
    }

    /// <summary>
    /// Calibrate the accelerometer based on tilt/rotation of device.
    /// </summary>
    void CalibrateAccelerometer()
    {
        Vector3 accelerometerSnapshot = Input.acceleration;
        Quaternion rotateQuaternion = Quaternion.FromToRotation(new Vector3(0.0f, 0.0f, -1.0f), accelerometerSnapshot);
        calibrationQuaternion = Quaternion.Inverse(rotateQuaternion);
    }
}
