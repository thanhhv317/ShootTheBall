using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletScripts : MonoBehaviour {

    private Rigidbody2D myBody;

    private float speed = 5f;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void FixedUpdate () {
        myBody.velocity = new Vector2(0,speed);
	}

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Top")
        {
            Destroy(gameObject);
        }

        string[] name = target.name.Split();
        //for (int i = 0; i < name.Length; i++)
        //{
        //    Debug.Log(" The array contains: " + name[i]);
        //}

        if (name.Length > 1)
        {
            if (name[1] == "Ball")
            {
                Destroy(gameObject);
                ScoreScripts.scoreValue += 10;
            }
        }

    }
}
