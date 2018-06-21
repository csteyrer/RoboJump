using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour {
	


	private const int MAX_PAGES = 4;
	private int page = 1;
	private GameObject[] itemFrames;
	private Text[] titles;
	private const int numberOfPageFields = 3;
	private Image[] item1;
	private int[] item1Prices = {2, 2, 25, 25};
	private Image[] item2;
	private int[] item2Prices = {4, 4, 25, 25};
	private Image[] item3;
	private int[] item3Prices = {6, 6, 50, 50};

	void Start () {

		itemFrames = new GameObject[numberOfPageFields];
		titles = new Text[MAX_PAGES];

		GameObject[] fields = GameObject.FindGameObjectsWithTag ("ShopItem");
		itemFrames = new GameObject[numberOfPageFields];
		foreach (GameObject o in fields) {
			itemFrames [int.Parse(o.name.Substring (4))-1] = o;
		}
		for (int i = 0; i < MAX_PAGES; i++) {
			titles[i] = GameObject.FindGameObjectWithTag ("ShopTab"+(i+1)).GetComponentInChildren<Text>(true);
		}

		item1 = itemFrames [0].GetComponentsInChildren<Image> (true);
		item2 = itemFrames [1].GetComponentsInChildren<Image> (true);
		item3 = itemFrames [2].GetComponentsInChildren<Image> (true);
		setItems (true);
		setPrices ();

	}

	public void MoveLeft()
	{
		if (page > 1) {
			titles[page-1].gameObject.SetActive (false);
			setItems (false);
			page--;
			titles[page-1].gameObject.SetActive (true);
			setItems (true);
			setPrices ();

		}
	}

	public void MoveRight()
	{
		if (page < MAX_PAGES) {
			titles[page-1].gameObject.SetActive (false);
			setItems (false);
			page++;
			titles[page-1].gameObject.SetActive (true); 
			setItems (true);
			setPrices ();
		}
	}

	private void setItems(bool activate)
	{
		item1 [(page-1)+2].gameObject.SetActive (activate);
		item2 [(page-1)+2].gameObject.SetActive (activate);
		item3 [(page-1)+2].gameObject.SetActive (activate);
	}

	private void setPrices()
	{
		int id1 = (3 * (page - 1));
		int id2 = (3 * (page - 1))+1;
		int id3 = (3 * (page - 1))+2;

		if (PlayerPrefs.GetInt ("Item" + id1) == 1 && page >= 3) {
			item1 [1].gameObject.GetComponentInChildren<Text> ().text = "-X-";
		}
		else
			item1 [1].gameObject.GetComponentInChildren<Text> ().text =
			item1Prices [(page - 1)].ToString ();
		if (PlayerPrefs.GetInt ("Item" + id2) == 1 && page >= 3) {
			item2 [1].gameObject.GetComponentInChildren<Text> ().text = "-X-";
		}
		else
			item2 [1].gameObject.GetComponentInChildren<Text> ().text = 
			item2Prices [(page - 1)].ToString ();
		if (PlayerPrefs.GetInt ("Item" + id3) == 1 && page >= 3) {
			item3 [1].gameObject.GetComponentInChildren<Text> ().text = "-X-";
		}
		else
			item3 [1].gameObject.GetComponentInChildren<Text> ().text = 
			item3Prices [(page - 1)].ToString ();
	}
    
    public void LoadMain()
    {
        SceneManager.LoadScene("MainMenu");
    }
    
	private bool buyItem(int item, int itemId)
	{
		int price = 0;
		switch (item) {
			case 1:
				price = item1Prices [itemId];
				break;
			case 2:
				price = item2Prices [itemId];
				break;
			case 3:
				price = item3Prices [itemId];
				break;
		}


		if (PlayerPrefs.GetInt ("Currency") >= price) {
			PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") - price);
			return true;
		}
		else
			return false;
	}
	public void buyHealingPotion()
	{
		if (buyItem(1, 0)) {
			int value = PlayerPrefs.GetInt ("Item" + 0);
			PlayerPrefs.SetInt ("Item"+0, ++value);
		}
	}

	public void buySpeedPotion()
	{
		if (buyItem(2, 0)) {
			int value = PlayerPrefs.GetInt ("Item" + 1);
			PlayerPrefs.SetInt ("Item"+1, ++value);
		}
	}

	public void buyJumpPotion()
	{
		if (buyItem(3, 0)) {
			int value = PlayerPrefs.GetInt ("Item" + 2);
			PlayerPrefs.SetInt ("Item"+2, ++value);
		}
	}

	public void buyDoubleShot()
	{
		if (buyItem(1, 1)) {
			int value = PlayerPrefs.GetInt ("Item" + 3);
			PlayerPrefs.SetInt ("Item"+3, ++value);
		}
		
	}

	public void buyTripleShot()
	{
		if (buyItem(2, 1)) {
			int value = PlayerPrefs.GetInt ("Item" + 4);
			PlayerPrefs.SetInt ("Item"+4, ++value);
		}
	}

	public void buyHeavyShot()
	{
		if (buyItem(3, 1)) {
			int value = PlayerPrefs.GetInt ("Item" + 5);
			PlayerPrefs.SetInt ("Item"+5, ++value);
		}
	}

	public void buyMoreMoney()
	{
		if (PlayerPrefs.GetInt("Item"+6) != 1 && buyItem(1, 2)) {
			PlayerPrefs.SetInt ("Item"+6, 1);
			PlayerPrefs.SetInt ("MoneyMultiplier", 12);
			setPrices ();
		}
	}

	public void buyUnlockLevels()
	{
		if (PlayerPrefs.GetInt("Item"+7) != 1 && buyItem(2, 2)) {
			PlayerPrefs.SetInt ("Item"+7, 1);
			int levelCount = PlayerPrefs.GetInt ("LevelCount");
			for(int i = 1; i <= levelCount; i++)
			{
				if(PlayerPrefs.GetInt ("StarsOfLevel"+i) == -1)
					PlayerPrefs.SetInt ("StarsOfLevel"+i, 0);
			}
			setPrices ();
		}
	}

	public void buyDeathCheat()
	{
		if (PlayerPrefs.GetInt("Item"+8) != 1 && buyItem(3, 2)) {
			PlayerPrefs.SetInt ("Item"+8, 1);
			PlayerPrefs.SetInt ("DeathCheat", 1);
			setPrices ();
		}
	}

	public void buyHealthRegen()
	{
		if (PlayerPrefs.GetInt("Item"+9) != 1 && buyItem(1, 3)) {
			PlayerPrefs.SetInt ("Item"+9, 1);
			PlayerPrefs.SetInt ("HealthRegen", 1);
			setPrices ();
		}
		
	}

	public void buyMoreHealth()
	{
		if (PlayerPrefs.GetInt("Item"+10) != 1 && buyItem(2, 3)) {
			PlayerPrefs.SetInt ("Item"+10, 1);
			PlayerPrefs.SetInt ("MoreHealth", 1);
			setPrices ();
		}

	}

	public void buySpeedUpgrade()
	{
		if (PlayerPrefs.GetInt("Item"+11) != 1 && buyItem(3, 3)) {
			PlayerPrefs.SetInt ("Item"+11, 1);
			PlayerPrefs.SetFloat ("SpeedUpgrade", 12f);
			setPrices ();
		}
	}

       
    
}
