using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstStart : MonoBehaviour {

    public int levels;
	// Use this for initialization
	void Start () {
        if (!PlayerPrefs.HasKey("Currency"))
        {
            PlayerPrefs.SetInt("Currency", 0);
        }
        // Optionaly Achievments
        for(int i = 1; i <= levels;i++)
        {
            for(int j = 1; j < 3; j++)
            {
                if(!PlayerPrefs.HasKey("Level"+i+"."+j))
                {
                    PlayerPrefs.SetFloat("Level" + i + "." + j, 0);
                }
            }
        }
        //initialize all levels (lock / unlock)


        for (int i = 2; i <= levels; i++)
        {
            if (!PlayerPrefs.HasKey("StarsOfLevel"+i))
            {
                PlayerPrefs.SetInt("StarsOfLevel"+i,-1);
            }
        }

        if (!PlayerPrefs.HasKey("StarsOfLevel1"))
        {
            PlayerPrefs.SetInt("StarsOfLevel1", 0);
        }
        //PlayerPrefs.SetInt("StarsOfLevel1", 1);   //test stars
        //PlayerPrefs.SetInt("StarsOfLevel2", 2);   //test stars
        //PlayerPrefs.SetInt("StarsOfLevel3", 3);   //test stars
        //PlayerPrefs.SetInt("StarsOfLevel4", 0);   //test stars

        //initialize all level times made by player if not present
        for (int i = 1; i <= levels;i++)
        {
            if (!PlayerPrefs.HasKey("Level" + i + "s"))
            {
                PlayerPrefs.SetFloat("Level" + i + "s", 0);
            }
            if (!PlayerPrefs.HasKey("EndLevel" + i + "s"))
            {
                PlayerPrefs.SetFloat("EndLevel" + i + "s", 0);
            }
        }
        
        PlayerPrefs.SetInt("LevelCount", levels);

		for (int i = 0; i < 12; i++)
		{
			if (!PlayerPrefs.HasKey("Item"+i))
			{
				PlayerPrefs.SetInt("Item"+i,0);
			}
		}

		if (!PlayerPrefs.HasKey("MoneyMultiplier"))
		{
			PlayerPrefs.SetInt ("MoneyMultiplier", 10);
		}
		if (!PlayerPrefs.HasKey("DeathCheat"))
		{
			PlayerPrefs.SetInt ("DeathCheat", 0);
		}
		if (!PlayerPrefs.HasKey("HealthRegen"))
		{
			PlayerPrefs.SetInt ("HealthRegen", 0);
		}
		if (!PlayerPrefs.HasKey("MoreHealth"))
		{
			PlayerPrefs.SetInt ("MoreHealth", 0);
		}
		if (!PlayerPrefs.HasKey("SpeedUpgrade"))
		{
			PlayerPrefs.SetFloat ("SpeedUpgrade", 10f);
		}
        //End

	}
	
}
