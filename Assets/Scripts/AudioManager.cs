using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

/// <summary>
/// This script contains all the logic for how audio is handled.
/// </summary>
public class AudioManager : MonoBehaviour
{
    #region singleton instance
    public static AudioManager instance;

    private void MakeSingleton()
    {
        if (FindObjectsOfType(GetType()).Length > 1)
        {
            Destroy(gameObject);
        }
        else instance = this;
    }
    #endregion

    #region sound struct
    [System.Serializable]
    public struct Sound
    {
        public string clipName;
        public AudioClip clip;
        public AudioMixerGroup audioMixerGroup;

        [Range(0f, 1f)]
        public float volume;

        [Range(0f, 3f)]
        public float pitch;

        public bool loop;
        public bool playOnAwake;
        public bool oneShot;

        [Range(0f, 1f)]
        public float spatial;

        public bool setDistancesHere;
        public float minDistance3Dsound;
        public float maxDistance3DSound;
    }
    #endregion




    public Sound[] sounds;

    // use a dictionary because it is implemented as a hashtable and provides greater efficiency when searching by a string key than simply
    // looking through an array, finding the element and comparing it's name with another - plus it only needs to be set up once at game start up
    private Dictionary<string, int> soundDictionary = new Dictionary<string, int>();





    void Awake ()
    {
        MakeSingleton();

        // add dictionary elements with the sound name as the key and the array index of the sound as the value
        for (int i = 0; i < sounds.Length; i++)
        {
            soundDictionary.Add(sounds[i].clipName, i);
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            Debug.Log(sounds[i].clipName);
        }
    }
	




    // takes in a clip name and an audio source and plays a sound, if it exists, according to its predefined properties
	public void PlaySound(string inName, ref AudioSource inSource)
    {
        int soundIndex;

        // if the sound name exists in the dictionary, grab the the array index value
        if (soundDictionary.TryGetValue(inName, out soundIndex))
        {
            // updates the passed in audio source with the correct properties of the sound
            UpdateAudioSource(ref inSource, sounds[soundIndex]);

            // if the sound is only to be played once
            if (sounds[soundIndex].oneShot == true)
            {
                // if the sound is not playing, play it
                if (!inSource.isPlaying)
                    inSource.Play();

                Debug.Log("Play Sound!!!");
            }
            
            // if the sound can be played multiple times
            else inSource.Play();

            
        }

        // if the sound does not exist within the dictionary, display an error
        else
        {
            Debug.LogError("Sound was not found: " + inName);
        }
    }






    private void UpdateAudioSource(ref AudioSource source, Sound inSound)
    {
        source.clip = inSound.clip;
        source.volume = inSound.volume;
        source.pitch = inSound.pitch;
        source.playOnAwake = inSound.playOnAwake;
        source.loop = inSound.loop;
        source.outputAudioMixerGroup = inSound.audioMixerGroup;
        source.spatialBlend = inSound.spatial;

        if(inSound.setDistancesHere)
        {
            source.minDistance = inSound.minDistance3Dsound;
            source.maxDistance = inSound.maxDistance3DSound;
        }      
    }
}
