using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RobotController : MonoBehaviour
{
    bool sliding = false;
    float slideTimer = 0;
    public float maxSlideTime = 1f;

    [SerializeField]
    GameObject healthCollider;
    [SerializeField]
    GameObject slideCollider;

	public int numberOfBullets = 1;
	private bool bulletWait = false;
	public float bulletDamage = 10;

    //how fast the robot can move
    public float topSpeed;
    //tell the sprite which direction it is pointing
    bool facingRight = true;

    //get reference to animator
    Animator anim;

    //not grounded
    bool grounded = false;

    //transform at robots foot to see if he is touching the ground
    public Transform groundCheck;

    //how big the circle is going to be when we check distance to the ground
    float groundRadius = 0.2f;

    //force of the jump
    public float jumpForce = 700f;

    //what layer is concidered the ground
    public LayerMask whatIsGround;

    //variable to check double jump
    bool doubleJump = false;
	
	public Transform muzzle;
	
	public GameObject bullet;

	[SerializeField]
	ItemController itemController;

    AudioManager audioManager;


    void Start()
    {
		topSpeed =  PlayerPrefs.GetFloat("SpeedUpgrade");

        audioManager = AudioManager.instance;
        anim = GetComponent<Animator>();
        //sanity check to make sure the player is not dead
        anim.SetBool("isDead", false);
    }

    //physics in fixed update
    void FixedUpdate()
    {
        //true or false, did the ground transform hit the whatIsGround with the groundRadius
        grounded = Physics2D.OverlapCircle(groundCheck.position, groundRadius, whatIsGround);

        //tell the animator that you're grounded
        anim.SetBool("Ground", grounded);

        //reset double jump
        if (grounded)
            doubleJump = false;
        
        //get how fast we are moving up or down from the rigidbody
        anim.SetFloat("vSpeed", GetComponent<Rigidbody2D>().velocity.y);

        //get move direction
        float move = Input.GetAxis("Horizontal");

        //add velocity to the rigidbody in the move direction * our speed
        GetComponent<Rigidbody2D>().velocity = new Vector2(move * topSpeed, GetComponent<Rigidbody2D>().velocity.y);

        anim.SetFloat("Speed", Mathf.Abs(move));

        //if we're facing the negative direction and not facing right, flip
        if (move > 0 && !facingRight)
            Flip();
        else if (move < 0 && facingRight)
            Flip();
    }

    void Update()
    {
        GetInputMovement();
		GetInputItem ();
    }
	void GetInputItem()
	{
		if(Input.GetButtonDown("Item") && Menu.IsPlaying())
		{
			itemController.Item();
		}
		if(Input.GetButtonDown("ItemUp") && Menu.IsPlaying())
		{
			itemController.NextItem();
		}
		if(Input.GetButtonDown("ItemDown") && Menu.IsPlaying())
		{
			itemController.PrevItem();
		}
		
	}
    void GetInputMovement()
    {
        if ((grounded || !doubleJump) && Input.GetButtonDown("Jump") && !sliding)
        {
            //not on the ground
            anim.SetBool("Ground", false);

            //add jump force to the Y axis of the rigidbody
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jumpForce));

            if (!doubleJump && !grounded)
                doubleJump = true;
        }

        if(Input.GetButtonDown("Slide") && !sliding && grounded)
        {
            slideTimer = 0f;

            anim.SetBool("isSliding", true);

            gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            healthCollider.GetComponent<CapsuleCollider2D>().enabled = false;
            foreach(CapsuleCollider2D col in slideCollider.GetComponents<CapsuleCollider2D>())
            {
                col.enabled = true;
            }
            sliding = true;
        }

        if(sliding)
        {
            slideTimer += Time.deltaTime;

            if(slideTimer >= maxSlideTime)
            {
                sliding = false;
                anim.SetBool("isSliding", false);
                gameObject.GetComponent<CapsuleCollider2D>().enabled = true;
                healthCollider.GetComponent<CapsuleCollider2D>().enabled = true;
                foreach (CapsuleCollider2D col in slideCollider.GetComponents<CapsuleCollider2D>())
                {
                    col.enabled = false;
                }
            }
        }
		
		if(Input.GetButtonDown("Fire1") && Menu.IsPlaying())
		{
			if(!bulletWait)
				StartCoroutine ("FireBullet");
		}

        if(Input.GetButtonUp("Fire1"))
        {
            anim.SetBool("isShooting", false);
            anim.SetBool("isRunningAndShooting", false);
        }

        if(Input.GetButtonDown("Fire1") && GetComponent<Rigidbody2D>().velocity.x > 0)
        {
            anim.SetBool("isRunningAndShooting", true);
        }
    }

	private IEnumerator FireBullet()
	{
		bulletWait = true;
		anim.SetBool ("isShooting", true);

		for (int i = 0; i < numberOfBullets; i++) {
			GameObject mBullet = Instantiate (bullet, muzzle.position, muzzle.rotation);

			audioManager.PlaySound ("GunShot");

			mBullet.transform.parent = GameObject.Find ("GameManager").transform;

			mBullet.GetComponent<Renderer> ().sortingLayerName = "Player";

			float wait = 1.5f;
			if (numberOfBullets > 1)
				wait = 0.2f;

			yield return new WaitForSeconds(wait);

			bulletDamage = 10;
		}

		numberOfBullets = 1;
		bulletWait = false;
	}
		

    void Flip()
    {
        //saying we are facing the opposite direction
        facingRight = !facingRight;

        //get the local scale
        Vector3 theScale = transform.localScale;

        //flip on x axis
        theScale.x *= -1;

        //apply that to the local scale
        transform.localScale = theScale;
    }

}
