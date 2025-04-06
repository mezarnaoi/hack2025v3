using System;
using UnityEngine;
using UnityEngine.UI;

public class SettingManager : MonoBehaviour
{
    public AudioSource soundAudioSource;
    public AudioSource musicAudioSource;

    public Toggle soundToggle;
    public Toggle musicToggle;

    public Text controlText;
    public int controlState;

    private void Start()
    {
        soundToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("sound_audio", 1));
        musicToggle.isOn = Convert.ToBoolean(PlayerPrefs.GetInt("music_audio", 1));
        controlState = PlayerPrefs.GetInt("control_state", 0);

        if (controlState == 0)
            controlText.text = "JOYSTICK";
        else if (controlState == 1)
            controlText.text = "TOUCH";
        
        soundAudioSource.mute = !soundToggle.isOn;
        musicAudioSource.mute = !musicToggle.isOn;
    }

    public void OnSoundToggleChange()
    {
        PlayerPrefs.SetInt("sound_audio", Convert.ToInt32(soundToggle.isOn));
        soundAudioSource.mute = !soundToggle.isOn;
    }

    public void OnMusicToggleChange()
    {
        PlayerPrefs.SetInt("music_audio", Convert.ToInt32(musicToggle.isOn));
        musicAudioSource.mute = !musicToggle.isOn;
    }

    public void ControlStateChange()
    {
        if (controlState == 0)
        {
            controlText.text = "TOUCH";
            controlState = 1;
        }
        else if (controlState == 1)
        {
            controlText.text = "JOYSTICK";
            controlState = 0;
        }

        PlayerPrefs.SetInt("control_state", controlState);
    }
    
    public void OnClickLeftButton()
    {
        ControlStateChange();
    }

    public void OnClickRightButton()
    {
        ControlStateChange();
    }
}
