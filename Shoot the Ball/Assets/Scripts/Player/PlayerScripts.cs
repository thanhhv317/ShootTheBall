using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScripts : MonoBehaviour {

    [SerializeField]
    private GameObject bullet;

    [SerializeField]
    private AudioClip shootSound;

    [SerializeField]
    private Transform pos;

    private float speed = 8f;
    private float maxVelocity = 4f;

    private Rigidbody2D myBody;
    private Animator anim;

    private bool canShoot;
    private bool canWalk;

    private bool moveRight, moveLeft;


    private void Awake()
    {
        myBody = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        canShoot = true;
        canWalk = true;
        GameObject.Find("Shoot").GetComponent<Button>().onClick.AddListener( ()=> shootByTouch());
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        //shootByTouch();
	}



    void FixedUpdate()
    {
        //playerWalk();
        playerJoyStick();
    }

    public void setMoveLeft(bool moveLeft)
    {
        this.moveLeft = moveLeft;
        this.moveRight = !moveLeft;
    }

    public void stopMoving()
    {
        this.moveLeft = false;
        this.moveRight = false;
    }


    //void shoot()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (canShoot)
    //        {
    //            canShoot = false;
    //            StartCoroutine(shootTheBullet());
    //        }
    //    }
    //}

    public void shootByTouch()
    {
        if (canShoot)
        {
            canShoot = false;
            StartCoroutine(shootTheBullet());
        }
    }

    IEnumerator shootTheBullet()
    {
        canWalk = false;
        anim.Play("Shoot");
        yield return new WaitForSeconds(0.2f);
        Instantiate(bullet, pos.position, Quaternion.identity);

        AudioSource.PlayClipAtPoint(shootSound, transform.position);

        yield return new WaitForSeconds(0.3f);
        
        canWalk = true;

        yield return new WaitForSeconds(0.4f);
        canShoot = true;
        anim.SetBool("Shoot", false);
    }
    //di chuyen = touch
    void playerJoyStick()
    {
        var force = 0f;
        var velocity = Mathf.Abs(myBody.velocity.x);

        if (moveRight)
        {
            // moving right
            if (velocity < maxVelocity)
                force = speed;
            Vector3 scale = transform.localScale;
            scale.x = 0.5f;
            transform.localScale = scale;
            anim.SetBool("Walk", true);
        }
        else if (moveLeft)
        {
            // moving right
            if (velocity < maxVelocity)
                force = -speed;
            Vector3 scale = transform.localScale;
            scale.x = -0.5f;
            transform.localScale = scale;
            anim.SetBool("Walk", true);
        }
        else
        {
            anim.SetBool("Walk", false);
        }

        myBody.AddForce(new Vector2(force, 0));
    }


    void playerWalk()
    {
        var force = 0f;
        var velocity = Mathf.Abs(myBody.velocity.x);

        float h = Input.GetAxis("Horizontal");
        if (canWalk)
        {
            if (h > 0)
            {
                // moving right
                if (velocity < maxVelocity)
                    force = speed;
                Vector3 scale = transform.localScale;
                scale.x = 0.5f;
                transform.localScale = scale;
                anim.SetBool("Walk", true);
            }
            else if (h < 0)
            {
                //moving left
                if (velocity < maxVelocity)
                    force = -speed;
                Vector3 scale = transform.localScale;
                scale.x = -0.5f;
                transform.localScale = scale;
                anim.SetBool("Walk", true);
            }
            else if (h == 0)
            {
                anim.SetBool("Walk", false);
            }
        }

        myBody.AddForce(new Vector2(force, 0));
    }

    IEnumerator killThePlayerAndRestarTheGame()
    {
        transform.position = new Vector3(200, 200, 0);
        yield return new WaitForSeconds(1.5f);
        Application.LoadLevel(Application.loadedLevelName);
    }

    private void OnTriggerEnter2D(Collider2D target)
    {
        string[] name = target.name.Split();

        if (name.Length > 1)
        {
            if (name[1] == "Ball")
            {
                StartCoroutine(delay());
                
                //StartCoroutine(killThePlayerAndRestarTheGame());
                //panel in active


            }
        }
    }
    IEnumerator delay()
    {
        canShoot = false;
        canWalk = false;
        anim.SetBool("Die", true);
        yield return new WaitForSeconds(1f);
        Time.timeScale = 0f;
        GamePlayController.gameplay.losePanel.SetActive(true);
        GamePlayController.gameplay.pauseButton.SetActive(false);
        
    }

}
