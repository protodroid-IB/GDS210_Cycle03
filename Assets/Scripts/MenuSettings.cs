using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System.Collections.Generic;

// This will control the settings in the options menu.

public class MenuSettings : MonoBehaviour
{
    [SerializeField] GameSettings gameSettings;
    [SerializeField] Soundscapes soundscapes;

    [SerializeField]
    private string volParameterSFX, volParameterMusic; 

    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;
    public Toggle tutorialToggle;

    [SerializeField]
    private AudioMixerGroup mixerSFX, mixerMusic, mixerMaster;



    private void Start()
    {
        soundVolumeSlider.onValueChanged.AddListener(SoundVolumeChange);
        musicVolumeSlider.onValueChanged.AddListener( MusicVolumeChange);
        tutorialToggle.onValueChanged.AddListener(TutorialToggleChange);

        UpdateSFXVolume();
        UpdateMusicVolume();

    }

    private void Update()
    {
        soundVolumeSlider.value = gameSettings.soundVolume;
        musicVolumeSlider.value = gameSettings.musicVolume;
        tutorialToggle.isOn = gameSettings.tutorial;
    }

    // Set if the game should be played as a tutorial.
    public void TutorialToggleChange(bool toggle)
    {
        GameManager.instance.SetTutorialActive();

        int tutorialToggle = toggle ? 1 : 0;
        gameSettings.tutorial = toggle;
        PlayerPrefs.SetInt("Tutorial", tutorialToggle);
    }
    
    // Adjust volumes for Sounds...
    void SoundVolumeChange(float volume)
    {
        gameSettings.soundVolume = volume;
        UpdateSFXVolume();
        PlayerPrefs.SetFloat("SoundVolume", gameSettings.soundVolume);
    }

    // ... and Music.
    void MusicVolumeChange(float volume)
    {
        gameSettings.musicVolume = musicVolumeSlider.value;
        UpdateMusicVolume();
        PlayerPrefs.SetFloat("MusicVolume", gameSettings.musicVolume);
    }

    void UpdateSFXVolume()
    {
        mixerSFX.audioMixer.SetFloat(volParameterSFX, 0f + (1f - gameSettings.soundVolume) * -45f);
    }

    void UpdateMusicVolume()
    {
        mixerMusic.audioMixer.SetFloat(volParameterMusic, 0f + (1f - gameSettings.musicVolume) * -45f);
    }

	public void TutorialToggle()
	{
		tutorialToggle.isOn = !tutorialToggle.isOn;
	}

    
}
