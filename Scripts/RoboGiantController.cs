using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoboGiantController : MonoBehaviour {

	public GameObject lever;

    public float health = 200f;

    public Transform target;

    public float engageDistance = 9f;

    public float attackDistance = 4f;

    public float moveSpeed = 3f;

    private bool facingLeft = false;

    private Animator anim;

    public RobotController robotController;

    public float attackDamage = 10f;

    public SpriteRenderer healthBar;

    private float movementLimit = 0;
    public int leftMovementLimit = 100;
    public int rightMovementLimit = 100;

    void Start()
    {
        anim = GetComponent<Animator>();
		lever.SetActive(false);
    }

    void Update()
    {
        anim.SetBool("isIdle", true);
        anim.SetBool("isAttacking", false);
        anim.SetBool("isWalking", false);

        if (anim.GetBool("isDead") == false)
        {
            if (Vector3.Distance(target.position, this.transform.position) < engageDistance)
            {
                anim.SetBool("isIdle", false);
                Vector3 direction = target.position - this.transform.position;

                if (Mathf.Sign(direction.x) == 1 && facingLeft)
                {
                    Flip();
                }
                else if (Mathf.Sign(direction.x) == -1 && !facingLeft)
                {
                    Flip();
                }

                if (direction.magnitude >= attackDistance)
                {
                    anim.SetBool("isWalking", true);
                    Debug.DrawLine(target.transform.position, this.transform.position, Color.yellow);

                    if (facingLeft && movementLimit < leftMovementLimit)
                    {
                        this.transform.Translate(Vector3.left * (Time.deltaTime * moveSpeed));
                        movementLimit += 0.33f;
                    }
                    else if (!facingLeft && movementLimit > rightMovementLimit * -1)
                    {
                        this.transform.Translate(Vector3.right * (Time.deltaTime * moveSpeed));
                        movementLimit -= 0.33f;
                    }
                    else
                    {
                        anim.SetBool("isIdle", true);
                        anim.SetBool("isWalking", false);
                    }
                }

                if (direction.magnitude <= attackDistance)
                {
                    anim.SetBool("isAttacking", true);
                    Debug.DrawLine(target.transform.position, this.transform.position, Color.red);

                    

                }
            }
            else if (Vector3.Distance(target.position, this.transform.position) > engageDistance)
            {
                //do nothing
                Debug.DrawLine(target.position, this.transform.position, Color.green);
            }
        }
    }

    private void Flip()
    {
        facingLeft = !facingLeft;

        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
			health -= robotController.bulletDamage;
            healthBar.GetComponent<Transform>().localScale -= new Vector3(.04f * (robotController.bulletDamage / 10), 0, 0);

            if (health <= 0)
            {
                healthBar.enabled = false;
                anim.SetBool("isDead", true);
                Destroy(gameObject, 1.5f);
            }
        }
    }

    public void AttackPlayer()
    {
        if (robotController.GetComponentInChildren<PlayerHealth>().curHealth > 0)
            robotController.GetComponentInChildren<PlayerHealth>().DecreaseHealth(attackDamage);
    }

	public void ShowLever()
	{
		lever.SetActive(true);
	}
}
