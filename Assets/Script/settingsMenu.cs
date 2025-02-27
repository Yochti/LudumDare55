using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using System.Linq;
public class settingsMenu : MonoBehaviour
{
    public AudioMixer audioMixerGlobal;
    public TMPro.TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;
    public saveSytem save;
    public GameObject optionPanel;
  

    private void Start()
    {
        audioMixerGlobal.SetFloat("GlobalVolume", (5 + ((save.GlobalVolume - 10) / 10) * (75)));
        audioMixerGlobal.SetFloat("MusicVolume", (5 + ((save.MusicVolume - 10) / 10) * (75)));
        audioMixerGlobal.SetFloat("SFXVolume", (5 + ((save.SFXVolume - 10) / 10) * (75)));


        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;

    }
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            optionPanel.SetActive(false);
        }
    }
    public void setVolumeGlobal (float volume)
    {
        audioMixerGlobal.SetFloat("GlobalVolume",( 5+ ((volume-10) / 10)* (75) ));
        save.GlobalVolume = volume;
        save.SaveData();

    }
    public void setFPS(int fps)
    {
        QualitySettings.vSyncCount = fps * 10;
    }
    public void SetFullScreen(bool isFullScreen)
    {
        Screen.fullScreen = isFullScreen;
    }
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setVolumeMusic (float volume)
    {
        audioMixerGlobal.SetFloat("MusicVolume",( 5+ ((volume-10) / 10)* (75) ));
        save.MusicVolume = volume;
        save.SaveData();
        
        resolutions = Screen.resolutions.Select(resolution => new Resolution { width = resolution.width, height = resolution.height }).Distinct().ToArray();
        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + "x" + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.width && resolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Screen.fullScreen = true;

    }
    public void setVolumeSFX (float volume)
    {
        audioMixerGlobal.SetFloat("SFXVolume",( 5+ ((volume-10) / 10)* (75) ));
        save.SFXVolume = volume;
        save.SaveData();

    }


}
