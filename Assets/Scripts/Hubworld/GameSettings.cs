using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// This option is used to access the games settings.

[CreateAssetMenu(menuName = "ScriptableObject/GameSettings")]
public class GameSettings : ScriptableObject {
    public bool musicMute;

    [Range(0f, 1f)] public float musicVolume;

    public bool soundMute;

    [Range(0f,1f)] public float soundVolume;

    public bool tutorial;

}
