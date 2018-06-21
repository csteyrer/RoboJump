using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject OptionsWindow;

    public AudioMixer audioMixer;

    void Start()
    {
        SetSliderValues();
    }

    public void SetSFXLv(float sfxlv)
    {
        audioMixer.SetFloat("SFXVolume", sfxlv);
    }

    public void SetMusicLv(float musiclv)
    {
        audioMixer.SetFloat("MusicVolume", musiclv);
    }

    public void SetMasterLv(float masterlv)
    {
        audioMixer.SetFloat("MasterVolume", masterlv);
    }

    public void LoadLevel(int levelNr)
    {
        SceneManager.LoadScene("Level" + levelNr.ToString());
    }
    public void LoadShop()
    {
        SceneManager.LoadScene("Shop");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

	public void ResetPlayerPrefs()
	{
		PlayerPrefs.DeleteAll ();
        SceneManager.LoadScene("MainMenu");
	}

    private void SetSliderValues()
    {
        Slider[] slider = OptionsWindow.GetComponentsInChildren<Slider>();
        float masterVol;
        audioMixer.GetFloat("MasterVolume", out masterVol);
        slider[0].value = masterVol;
        float musicVol;
        audioMixer.GetFloat("MusicVolume", out musicVol);
        slider[1].value = musicVol;
        float sfxVol;
        audioMixer.GetFloat("SFXVolume", out sfxVol);
        slider[2].value = sfxVol;
    }
}
