using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemController : MonoBehaviour {

    [SerializeField]
    private Text counterText;
	public RobotController robotController;
	private int currItem = 0;
	private int[] itemAmount;
    private Image[] images;
	private float normalBulletDamage;
	private bool speedPotionUsed = false;
	private bool jumpPotionUsed = false;


	void Start () {
		normalBulletDamage = robotController.bulletDamage;
        images = new Image[6];
		itemAmount = new int[12];
		for (int i = 0; i < 12; i++)
		{
			itemAmount[i] = PlayerPrefs.GetInt ("Item" + i);
		}
        SetItemImages();
        images[currItem].gameObject.SetActive(true);
        counterText.text = itemAmount[currItem].ToString();
    }

	public void Item()
	{
		if (itemAmount [currItem] > 0) {
			bool itemUsed = false;
			switch (currItem) {
			case 0:
				HealingPotion ();
				itemUsed = true;
				break;
			case 1:
				if (!speedPotionUsed) {
					speedPotionUsed = true;
					StartCoroutine ("SpeedPotion");
					itemUsed = true;
				}
				break;
			case 2:
				if (!jumpPotionUsed) {
					jumpPotionUsed = true;
					StartCoroutine ("JumpPotion");
					itemUsed = true;
				}
				break;
			case 3:
				if(robotController.numberOfBullets == 1)
				{
					DoubleShot ();
					itemUsed = true;
				}
				break;
			case 4:
				if(robotController.numberOfBullets == 1)
				{
					TripleShot ();
					itemUsed = true;
				}
				break;
			case 5:
				if (robotController.bulletDamage == normalBulletDamage) {
					HeavyShot ();
					itemUsed = true;
				}
				break;
			}

			if(itemUsed)
			{
				DecreaseItemCount ();
				itemAmount [currItem]--;
				counterText.text = itemAmount [currItem].ToString ();
			}
		}
	}

	public void NextItem()
	{
        if(currItem == 5) {
            currItem = 0;
        }
        else {
            currItem++;
        }
        DisableAllImages();
        images[currItem].gameObject.SetActive(true);
        counterText.text = itemAmount[currItem].ToString();
    }

	public void PrevItem()
	{
        if (currItem == 0)
        {
            currItem = 5;
        }
        else
        {
            currItem--;
        }
        DisableAllImages();
        images[currItem].gameObject.SetActive(true);
        counterText.text = itemAmount[currItem].ToString();
    }

	private void HealingPotion()
	{
		Debug.Log ("Healing Potion!");
		robotController.GetComponentInChildren<PlayerHealth>().IncreaseHealth(20f);

	}

	private IEnumerator SpeedPotion()
	{
		Debug.Log ("Speed Potion!");
		float value = robotController.topSpeed;
		robotController.topSpeed = value*1.5f;
		yield return new WaitForSeconds (5);
		robotController.topSpeed = value;
		speedPotionUsed = false;
	}

	private IEnumerator JumpPotion()
	{
		Debug.Log ("Jump Potion!");
		float value = robotController.jumpForce;
		robotController.jumpForce = value*1.5f;
		yield return new WaitForSeconds (5);
		robotController.jumpForce = value;
		jumpPotionUsed = false;
	}

	private void TripleShot ()
	{
		Debug.Log ("Triple Shot!");
		robotController.numberOfBullets = 3;
	}

	private void DoubleShot()
	{
		Debug.Log ("Double Shot!");
		robotController.numberOfBullets = 2;
	}

	private void HeavyShot()
	{
		Debug.Log ("Heavy Shot!");
		robotController.bulletDamage *= 4;

	}

    public void SetCounterText(Text counter) {
        counterText = counter;
    }

    public void SetItemImages() {
        Image[] imageArray = GetComponentsInChildren<Image>(true);
        int count = 0;
        for (int i = 0; i < imageArray.Length; i++)
        {
            if(imageArray[i].tag == "Item") {
                images[count++] = imageArray[i];
            }

        }
    }

    private void DisableAllImages() {
        for (int i = 0; i < images.Length; i++) {
            images[i].gameObject.SetActive(false);
        }
    }

    private void DecreaseItemCount() {
	    int value = PlayerPrefs.GetInt("Item" + currItem);
		PlayerPrefs.SetInt ("Item" + currItem, --value);
    }
}
