using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour {

	private int levelCount;
	private int page = 1;
	private Button[] buttons;
	private int[] stars;
	private const int numberOfPageFields = 8;
	private int count;
	//public delegate void TestDelegate(int index);

	[SerializeField]
	MainMenu menu;

	// Use this for initialization
	void Start () {


		
		levelCount = PlayerPrefs.GetInt ("LevelCount");
		buttons = new Button[numberOfPageFields];
		GameObject[] fields = GameObject.FindGameObjectsWithTag ("LevelButton");
		foreach (GameObject o in fields) {
			Button b = o.GetComponentInChildren<Button> (true);
			buttons [int.Parse(b.name.Substring (5))-1] = b;
		}

		stars = new int[levelCount];
		for (int i = 0; i < stars.Length; i++) {
			stars [i] = PlayerPrefs.GetInt("StarsOfLevel"+(i+1));
		}
		setFields ();
		
	}

	public void MoveLeft()
	{
		if (numberOfPageFields * page > numberOfPageFields) {
			page--;

			setFields ();
		}
	}

	public void MoveRight()
	{
		if (numberOfPageFields * page < levelCount) {
			page++; 

			setFields ();
		}
	}

	private void setFields()
	{
		count = numberOfPageFields * (page - 1);
		for (int i = 0; i < buttons.Length; i++) {
			if (count < stars.Length && stars [count] >= 0) {
				buttons [i].gameObject.SetActive (true);
				Text text = buttons [i].GetComponentInChildren<Text> (true);
				text.text = (count+1).ToString();
				buttons [i].onClick.RemoveAllListeners();
				OnClick click = new OnClick(count+1, menu);
				buttons [i].onClick.AddListener (click.onClick);
				Image[] star = buttons [i].GetComponentsInChildren<Image> (true);
				for (int j = 0; j < 3; j++) {
					if(j < stars[count])
						star [j + 1].gameObject.SetActive (true);
					else
						star [j + 1].gameObject.SetActive (false);
				}
			} else {
				buttons [i].gameObject.SetActive (false);
			}

			count++;

		}	
	}

	public class OnClick
	{
		private int index;
		MainMenu menu;
		public OnClick(int index, MainMenu menu)
		{
			this.index = index;
			this.menu = menu;
		}

		public void onClick()
		{
			menu.LoadLevel (index);
		}
	}

}
