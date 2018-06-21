using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour {
    [SerializeField]
    Text timerText;
    private static float startTime;
    private static float currTime = 0;

    void Awake()
    {
        Restart();
    }

    void Update()
    {
        currTime = startTime - Time.time;

        int minutes = (int)(currTime / 60) * -1;
        int seconds = (int)(currTime % 60) * -1;

        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }

    public static void Restart()
    {
        startTime = Time.time;
    }

    public static float GetTime()
    {
        return currTime;
    }
    public static void Stop()
    {
        float endTime = startTime - Time.time;
        PlayerPrefs.SetFloat("RecentTime", endTime);
    }
}