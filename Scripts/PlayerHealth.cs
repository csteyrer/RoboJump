using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField]
    Slider healthBar;
    [SerializeField]
    Text healthText;

    public Animator anim;

    float maxHealth = 100;
    public float curHealth;
	private bool deathCheat;
	private bool isRegenerating;
	private bool regen;
	private bool moreHealth;

    void Start()
    {
		if (PlayerPrefs.GetInt ("MoreHealth") == 1)
			moreHealth = true;
		else
			moreHealth = false;
		if (PlayerPrefs.GetInt ("HealthRegen") == 1)
			regen = true;
		else
			regen = false;
		isRegenerating = false;
		if (PlayerPrefs.GetInt ("DeathCheat") == 1)
			deathCheat = true;
		else
			deathCheat = false;
		healthBar.value = maxHealth;
        curHealth = healthBar.value;
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if(col.gameObject.tag == "Saw")
        {
			DecreaseHealth (2f);
        }
        if (col.gameObject.tag == "Acid")
        {
			DecreaseHealth (0.5f);
        }
        if (col.gameObject.tag == "Spikes")
        {
			DecreaseHealth (1f);
        }
    }

    void Update()
    {
        //n0 means do not show any decimal places
        healthText.text = curHealth.ToString("n0") + " %";

		if(curHealth <= 0 && deathCheat)
		{
			deathCheat = false;
			IncreaseHealth (20f);	
		}

        if (curHealth <= 0 && anim.GetBool("isDead") == false)
        {
            //play the dead animation
            anim.SetBool("isDead", true);
            Menu.Dead();

            AudioManager.instance.PlaySound("Failure");

            //stop all player movement
            GetComponentInParent<RobotController>().enabled = false;
        }
			
		if (regen && !isRegenerating && curHealth < maxHealth)
			StartCoroutine ("Regen");
    }

	private IEnumerator Regen()
	{
		isRegenerating = true;
		while(curHealth < maxHealth)
		{
			yield return new WaitForSeconds (1);
			IncreaseHealth (3f);	
		}
		isRegenerating = false;
	}

    public void DecreaseHealth(float f)
    {
		float damage = f;
		if (moreHealth)
			damage /= 2;
        healthBar.value -= damage;
        curHealth = healthBar.value;
    }

	//Item
	public void IncreaseHealth(float f)
	{
		healthBar.value += f;
		curHealth = healthBar.value;
	}
}
