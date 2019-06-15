using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScripts : MonoBehaviour {

    private float forceX ,forceY;

    private Rigidbody2D myBody;

    [SerializeField]
    bool moveLeft, moveRight;

    [SerializeField]
    private GameObject origanalBall;

    private GameObject ball1, ball2;
    private BallScripts ball1Script, ball2Script;

    [SerializeField]
    private AudioClip[] popSounds;

    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        setBallSpeed();
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        moveBall();

    }

    void instantiateBall()
    {
        if(this.gameObject.tag!="Smallest Ball")
        {
            ball1 = Instantiate(origanalBall);
            ball2 = Instantiate(origanalBall);

            ball1.name = origanalBall.name;
            ball2.name = origanalBall.name;

            ball1Script = ball1.GetComponent<BallScripts>();
            ball2Script = ball2.GetComponent<BallScripts>();
        }
    }

    void turnOffCurrentBall()
    {
        instantiateBall();
        Vector3 temp = transform.position;

        ball1.transform.position = temp;
        ball1Script.setMoveLeft(true);

        ball2.transform.position = temp;
        ball2Script.setMoveRight(true);

        ball1.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2.5f);
        ball2.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 2.5f);

        AudioSource.PlayClipAtPoint(popSounds[Random.Range(0, popSounds.Length)], transform.position);
        gameObject.SetActive(false);
    }

    public void setMoveLeft(bool canMoveLeft)
    {
        this.moveLeft = canMoveLeft;
        this.moveRight = !canMoveLeft;
    }

    public void setMoveRight(bool canMoveRight)
    {
        this.moveRight = canMoveRight;
        this.moveLeft = !canMoveRight;
    }

    void moveBall()
    {
        if (moveLeft)
        {
            Vector3 tmp = transform.position;
            tmp.x -= forceX * Time.deltaTime;
            transform.position = tmp;
        }
        if (moveRight)
        {
            Vector3 tmp = transform.position;
            tmp.x += forceX * Time.deltaTime;
            transform.position = tmp;
        }
    }

    void setBallSpeed()
    {
        forceX = 2.5f;

        switch (this.gameObject.tag)
        {
            case "Largest Ball":
                forceY = 11.5f;
                break;
            case "Large Ball":
                forceY = 10.5f;
                break;
            case "Medium Ball":
                forceY = 9f;
                break;
            case "Small Ball":
                forceY = 8f;
                break;
            case "Smallest Ball":
                forceY = 7f;
                break;
        }
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        if (target.tag == "Ground")
        {
            myBody.velocity = new Vector2(0, forceY);
        }
        if(target.tag=="Right Wall")
        {
            setMoveLeft(true);
        }
        if (target.tag == "Left Wall")
        {
            setMoveRight(true);
        }
        if (target.tag == "Bullet")
        {
            if(gameObject.tag!="Smallest Ball")
            {
                turnOffCurrentBall();
            }
            else
            {
                AudioSource.PlayClipAtPoint(popSounds[Random.Range(0, popSounds.Length)], transform.position);
                gameObject.SetActive(false);
            }
        }
    }

}
