using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger : MonoBehaviour {

    // Use this for initialization
    public Animator anim;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name == "HealthCollider")
        {
            this.gameObject.GetComponent<CapsuleCollider2D>().enabled = false;
            int state = anim.GetInteger("transition");
            state++;
            anim.SetInteger("transition", state);
        }
    }
}
