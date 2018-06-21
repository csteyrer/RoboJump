using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBarrel : MonoBehaviour
{
    public PlayerHealth playerHealth;
    public Animator anim;

    void OnTriggerEnter2D(Collider2D col)
    {
        StartCoroutine(KillOnAnimationEnd());
    }

    private IEnumerator KillOnAnimationEnd()
    {
        anim.SetTrigger("explosionTrigger");
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
        playerHealth.DecreaseHealth(80f);
        AudioManager.instance.PlaySound("Explosion");
    }
}
