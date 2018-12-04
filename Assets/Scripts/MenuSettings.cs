using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

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
        soundVolumeSlider.onValueChanged.AddListener(delegate { SoundVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        tutorialToggle.onValueChanged.AddListener(delegate { TutorialToggleChange(tutorialToggle.isOn); });

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
    void TutorialToggleChange(bool toggle)
    {
        int tutorialToggle = toggle ? 1 : 0;
        gameSettings.tutorial = toggle;
        PlayerPrefs.SetInt("Tutorial", tutorialToggle);
    }
    
    // Adjust volumes for Sounds...
    void SoundVolumeChange()
    {
        gameSettings.soundVolume = soundVolumeSlider.value;
        UpdateSFXVolume();
        PlayerPrefs.SetFloat("SoundVolume", gameSettings.soundVolume);
    }

    // ... and Music.
    void MusicVolumeChange()
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

}
