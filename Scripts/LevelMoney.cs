using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class LevelMoney : MonoBehaviour {

    public static bool trigger = false;
    public Text nextBestTimer;
    public Text moneyText;
    public Animator anim1;
    public Animator anim2;
    public Animator anim3;

    float endTime;  //beste vom Spieler erreichte Zeit
    float bestTime; //beste Zeit erreichbar für 3 sterne
    float midTime;  //beste Zeit für 2 Sterne
    float recentTime;   //letzte erreichte Zeit
    int level;
    int thisPoints;
    // Use this for initialization
    private void Start()
    {
    }
    private void Update()
    {
        if(trigger)
        {
            Initiate();
            trigger = false;
            Time.timeScale = 0;
        }
    }
    void Initiate()
    {
        string SceneName = SceneManager.GetActiveScene().name;

        if (SceneName == "TestLevel")
            level = 0;
        else if(!SceneName.Contains("Old"))
            level = int.Parse(SceneName.Split('l')[1]);
        bestTime = PlayerPrefs.GetFloat("Level" + level + ".1");
        midTime = PlayerPrefs.GetFloat("Level" + level + ".2");
        int stars = 1;
        Time.timeScale = 1;
        thisPoints = 0;
        bestTime = PlayerPrefs.GetFloat("Level" + level + ".1");
        midTime = PlayerPrefs.GetFloat("Level" + level + ".2");
        //get actual time ended
        recentTime = PlayerPrefs.GetFloat("RecentTime"); //Zeit von Timer erhalten
        recentTime *= -1;
        recentTime = Mathf.Round(recentTime);
        endTime = PlayerPrefs.GetFloat("EndLevel" + level + "s");

        

        if (recentTime < endTime || endTime == 0)
        {
            if (recentTime <= bestTime)    //beste Zeit
            {
                thisPoints = 3;
                int minutes = (int)(recentTime / 60);
                int seconds = (int)(recentTime % 60);
                nextBestTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                stars = 3;                
            }
            else if (recentTime <= midTime)    //mittlere Zeit
            {
                thisPoints = 2;
                int minutes = (int)(bestTime / 60);
                int seconds = (int)(bestTime % 60);
                nextBestTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                stars = 2;
            }
            else if (recentTime > midTime)     //schlechteste Zeit
            {
                thisPoints = 1;
                int minutes = (int)(midTime / 60);
                int seconds = (int)(midTime % 60);
                nextBestTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
            PlayerPrefs.SetFloat("EndLevel" + level + "s", recentTime);
            int prevStars = PlayerPrefs.GetInt("StarsOfLevel" + level);
            if(prevStars != 0)
            {
                thisPoints -= prevStars;
            }
			int multiplier = PlayerPrefs.GetInt ("MoneyMultiplier");
			int calcPoints = thisPoints * multiplier;
            moneyText.text = ""+calcPoints;
            PlayerPrefs.SetFloat("Level" + level + "s", recentTime);
            PlayerPrefs.SetInt("StarsOfLevel" + level, thisPoints+prevStars);    //Speichert die Sterne pro Level
            PlayerPrefs.SetInt("StarsOfLevel" + (level + 1), 0);            //Unlockt das nächste Level
            PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + calcPoints);    //addet die Sterne in die Currency
        }
        else
        {
            if (recentTime <= bestTime)    //beste Zeit
            {
                int minutes = (int)(recentTime / 60);
                int seconds = (int)(recentTime % 60);
                nextBestTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                stars = 3;
            }
            else if (recentTime <= midTime)    //mittlere Zeit
            {
                int minutes = (int)(bestTime / 60);
                int seconds = (int)(bestTime % 60) ;
                nextBestTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                stars = 2;
            }
            else if (recentTime > midTime)     //schlechteste Zeit
            {
                int minutes = (int)(midTime / 60);
                int seconds = (int)(midTime % 60);
                nextBestTimer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
            }
        }
        anim1.SetInteger("Stars", stars);
        anim2.SetInteger("Stars", stars);
        anim3.SetInteger("Stars", stars);
    }
}

