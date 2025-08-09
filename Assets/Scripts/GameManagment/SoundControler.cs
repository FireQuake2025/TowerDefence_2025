using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SoundControler : MonoBehaviour
{
    public Slider[] soundSlider;
    public AudioMixer mixerMaster;
    public AudioMixer mixerSFXMaster;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void SetVolume(float volume, AudioMixer mixer, int slider)
    {
        if (volume < 1)
        {
            volume = .001f;
        }
        RefreshSlider(volume,slider);
        PlayerPrefs.SetFloat("SavedMasterVolume", volume);
        mixer.SetFloat("Master", Mathf.Log10(volume / 100) * 20f);
    }

    public void SetMusicVolumeFromSlider()
    {
        SetVolume(soundSlider[0].value, mixerMaster,0);
    }

    public void SetSFXVolumeFromSlider()
    {
        SetVolume(soundSlider[1].value, mixerSFXMaster, 1);
    }

    public void RefreshSlider(float volume, int slider)
    {
        soundSlider[slider].value = volume;
        soundSlider[slider].value = volume;
    }
}
