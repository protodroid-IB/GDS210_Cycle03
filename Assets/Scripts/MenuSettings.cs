using UnityEngine;
using UnityEngine.UI;

// This will control the settings in the options menu.

public class MenuSettings : MonoBehaviour {
    [SerializeField] GameSettings gameSettings;
    [SerializeField] Soundscapes soundscapes;

    public Slider soundVolumeSlider;
    public Slider musicVolumeSlider;
    public Toggle tutorialToggle;

    private void Start()
    {
        soundVolumeSlider.onValueChanged.AddListener(delegate { SoundVolumeChange(); });
        musicVolumeSlider.onValueChanged.AddListener(delegate { MusicVolumeChange(); });
        tutorialToggle.onValueChanged.AddListener(delegate { TutorialToggleChange(tutorialToggle.isOn); });
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
        UpdateSoundScape();
        PlayerPrefs.SetFloat("SoundVolume", gameSettings.soundVolume);
    }

    // ... and Music.
    void MusicVolumeChange()
    {
        gameSettings.musicVolume = musicVolumeSlider.value;
        UpdateSoundScape();
        PlayerPrefs.SetFloat("MusicVolume", gameSettings.musicVolume);
    }

    // Update the SoundScapes Volume based on the values on the Sliders.
    void UpdateSoundScape()
    {
        for (int i = 0; i < soundscapes.outsideAudioSources.Length; i++)
        {
            AudioManager.instance.PlaySound("Soundscape_Outside", ref soundscapes.outsideAudioSources[i]);
        }

        //AudioManager.instance.PlaySound("Soundscape_Inside", ref soundscapes.insideAudioSource);
    }


}
