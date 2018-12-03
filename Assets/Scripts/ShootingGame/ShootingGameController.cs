using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGameController : MonoBehaviour {

    public TargetSequence targetSequence;
    public Target[] targets;
    int sequenceIndex = 0;
    int targetIndex = 0;
    [HideInInspector] public int score;
    public Text scoreText;
    public Text highscoreText;

    GameManager gameManager;

    public void Start()
    {
        gameManager = FindObjectOfType<GameManager>();
        ResetTargets();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            StartCycle();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            StopCycle();
        }
    }

    void StartCycle()
    {
        ResetTargets();
        score = 0;
        sequenceIndex = 0;
        targetIndex = 0;
        Cycle();
    }

    void StopCycle()
    {
        ResetTargets();
    }

    void Cycle()
    {
        if (sequenceIndex < targetSequence.sequence.Count)
        {
            Sequence sequence = targetSequence.sequence[sequenceIndex];
            if (sequence.randomize)
            {
                targets[sequence.targetID[Random.Range(0,sequence.targetID.Count)]].FlipUp(sequence.targetTime);
                targetIndex = 0;
                sequenceIndex++;
                Invoke("Cycle", sequence.exitTime);
            }
            else if (targetIndex < sequence.targetID.Count)
            {
                targets[sequence.targetID[targetIndex]].FlipUp(sequence.targetTime);
                targetIndex++;
                Invoke("Cycle", sequence.timeInterval);
            }
            else
            {
                targetIndex = 0;
                sequenceIndex++;
                Invoke("Cycle", sequence.exitTime);
            }
        }
        else sequenceIndex = 0;
    }

    private void ResetTargets()
    {
        CancelInvoke();
        foreach (Target target in targets)
        {
            target.FlipDown();
        }
    }

    public void Addscore(int points)
    {
        score += points;
        scoreText.text = score.ToString("0000");
    }

    // Function to restart the scene.
    public void RestartMiniGame(string minigame)
    {
        gameManager.RestartMiniGame(minigame);
    }
}