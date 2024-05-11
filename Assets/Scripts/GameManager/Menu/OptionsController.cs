using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class OptionsController : MonoBehaviour
{
     public AudioMixer audioMixer;
    [SerializeField] Slider slider;
    [SerializeField] Toggle toggle;
    float t = 0;
    private void Awake()
    {
        SaveData.Instance.LoadSettings(ref t);
        Debug.Log(t);
        audioMixer.SetFloat("Volume", t);
        slider.value = t;

        toggle.isOn = Screen.fullScreen;
    }
    public void SaveSettings()
    {
        SaveData.Instance.SaveSettings(audioMixer);
    }
    private void OnEnable()
    {
        slider.value = t;

        toggle.isOn = Screen.fullScreen;
    }
    public void SetVolume(float _volume)
    {
        audioMixer.SetFloat("Volume", _volume);
    }
    /*public void SetQuality(int _qualityIndex)
    {
        QualitySettings.SetQualityLevel(_qualityIndex);
    }*/
    public void SetFullScreen(bool _isFullScreen)
    {
        Screen.fullScreen = _isFullScreen;
    }

    public void Quit()
    {
        Application.Quit();

    }
}
