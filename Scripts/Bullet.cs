using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour 
{
	public float bulletSpeed;

    public RobotController player;

    void Start()
    {
        player = FindObjectOfType<RobotController>();

        if(player.transform.localScale.x < 0)
        {
            bulletSpeed = -bulletSpeed;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }
    }
	
	void Update()
	{
		GetComponent<Rigidbody2D>().velocity = new Vector2(bulletSpeed, GetComponent<Rigidbody2D>().velocity.y);

        Destroy(gameObject, 0.7f);
	}

    void OnTriggerStay2D(Collider2D col)
    {
        Destroy(gameObject);
        if (col.gameObject.tag == "Box")
        {
            Destroy(col.gameObject);
        }
    }
	
}
