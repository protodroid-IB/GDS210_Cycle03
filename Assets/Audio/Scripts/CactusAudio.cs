using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusAudio : MonoBehaviour
{
    [SerializeField]
    private float timeBetweenPhrases = 15f;
    private float timer = 0f;
    private Dictionary<int, string> dialogueNames;
    private AudioSource thisAudio;
    private void Start()
    {
        thisAudio = GetComponent<AudioSource>();
        dialogueNames = new Dictionary<int, string>();
        dialogueNames.Add(0, "Cactus_ByeNow");
        dialogueNames.Add(1, "Cactus_Howdy");
        dialogueNames.Add(2, "Cactus_Serving");
        dialogueNames.Add(3, "Cactus_Shooting");
        dialogueNames.Add(4, "Cactus_ShootingThrowingServing");
        dialogueNames.Add(5, "Cactus_Silly");
        dialogueNames.Add(6, "Cactus_NotVeryYeeHaw");
        dialogueNames.Add(7, "Cactus_Throwing");
    }
    // Update is called once per frame
    void Update()
    {
        if (timer >= timeBetweenPhrases)
        {
            timer = 0f;
            int index = Random.Range(0, 8);
            string dialogue;
            dialogueNames.TryGetValue(index, out dialogue);
            AudioManager.instance.PlaySound(dialogue, ref thisAudio);
        }
        else
            timer += Time.deltaTime;
    }
}