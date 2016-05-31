using UnityEngine;
using System.Collections;

public class ExplosionBase : MonoBehaviour
{
    float timer;
    float limit;
    
	// Use this for initialization
	void Start ()
    {
        timer = 0;
        limit = 1.5f;
	}
	
    void OnEnable()
    {
        timer = 0;
    }

	// Update is called once per frame
	void Update () 
    {
        timer += Time.deltaTime;

        if(timer > limit)
        {
            gameObject.SetActive(false);
        }
	}
}
