using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimesHardcode : MonoBehaviour {

    int levels;

	// Use this for initialization
	void Start () {
        levels = PlayerPrefs.GetInt("LevelCount");
        for (int i = 0; i <= levels; i++)
        {
            switch (i)
            {
                case 0:
                    PlayerPrefs.SetFloat("Level"+i+".1", 5);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 10);
                    break;
                case 1:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 10);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 15);
                    break;
                case 2:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 20);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 25);
                    break;
                case 3:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 15);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 20);
                    break;
                case 4:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 10);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 15);
                    break;
                case 5:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 6);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 11);
                    break;
                case 6:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 20);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 25);
                    break;
                case 7:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 15);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 20);
                    break;
                case 8:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 15);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 20);
                    break;
                case 9:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 14);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 19);
                    break;
                case 10:
                    PlayerPrefs.SetFloat("Level" + i + ".1", 40);
                    PlayerPrefs.SetFloat("Level" + i + ".2", 48);
                    break;
                default:
                   // Debug.Log("Level: " + levels + "has not been implemented into  \" TimesHardcode.cs \" - switch-Statement yet!");
                    break;
            }
        }
	}
	
}
