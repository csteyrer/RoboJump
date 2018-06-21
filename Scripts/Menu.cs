using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    [SerializeField]
    GameObject PauseWindow;

    [SerializeField]
    GameObject OptionsWindow;

    [SerializeField]
    GameObject HelpWindow;

    [SerializeField]
    GameObject MenuUI;

    [SerializeField]
    GameObject EndScreen;

    [SerializeField]
    GameObject DeathMenu;

    [SerializeField]
    AudioMixer audioMixer;

    enum MenuStates { Playing, Pause, Options, Help, EndScreen, Dead }
    static MenuStates currentState;

    void Start()
    {
        currentState = MenuStates.Playing;
    }

    void Update()
    {
        if(Input.GetKeyDown("escape") && currentState == MenuStates.Pause)
        {
            currentState = MenuStates.Playing;
        }
        else if(Input.GetKeyDown("escape") && currentState == MenuStates.Playing)
        {
            currentState = MenuStates.Pause;
        }

        switch (currentState)
        {
            case MenuStates.Playing:
                currentState = MenuStates.Playing;
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(false);
                ActivateTimeScale();
                DeathMenu.SetActive(false);
                EndScreen.SetActive(false);
                break;
            case MenuStates.Pause:
                PauseWindow.SetActive(true);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                DeactivateTimeScale();
                break;
            case MenuStates.Options:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(true);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(true);
                DeactivateTimeScale();
                break;
            case MenuStates.Help:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(true);
                MenuUI.SetActive(true);
                DeactivateTimeScale();
                break;
            case MenuStates.EndScreen:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(false);
                Timer.Stop();
                LevelMoney.trigger = true;
                DeactivateTimeScale();
                EndScreen.SetActive(true);
                break;
            case MenuStates.Dead:
                PauseWindow.SetActive(false);
                OptionsWindow.SetActive(false);
                HelpWindow.SetActive(false);
                MenuUI.SetActive(false);
                DeactivateTimeScale();
                DeathMenu.SetActive(true);
                break;
        } 
    }

    public void Restart()
    {
        currentState = MenuStates.Playing;
        string x = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(x);
        Timer.Restart();
    }

    public void DisplayOptions()
    {
        currentState = MenuStates.Options;
    }

    public void DisplayHelp()
    {
        currentState = MenuStates.Help;
    }

    public void Resume()
    {
        currentState = MenuStates.Playing;
    }

    public void Exit()
    {
        //stop the application in the unity editor or quit the application (after build and run)
        //UnityEditor.EditorApplication.isPlaying = false;
        SceneManager.LoadScene("MainMenu");
    }

    public void Back()
    {
        currentState = MenuStates.Pause;
    }

    public void SetSFXVolume(float sfxlv)
    {
        AudioManager.instance.SetSFXLv(sfxlv);
    }

    public void SetMusicVolume(float musiclv)
    {
        AudioManager.instance.SetMusicLv(musiclv);
    }

    public void SetMasterVolume(float masterlv)
    {
        AudioManager.instance.SetMasterLv(masterlv);
    }

    public static void ActivateTimeScale()
    {
        Time.timeScale = 1;
    }

    public static void DeactivateTimeScale()
    {
        Time.timeScale = 0;
    }

    public static void Finished()
    {
        currentState = MenuStates.EndScreen;
    }

    public static void Dead()
    {
        currentState = MenuStates.Dead;
    }

    public void NextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene(0);
    }
    public bool InEndScreen()
    {
        if (currentState.Equals(MenuStates.EndScreen))
            return true;
        return false;
    }
    public static bool IsPlaying()
    {
        if(!currentState.Equals(MenuStates.Playing))
        {
            return false;
        }
        return true;
    }
}
