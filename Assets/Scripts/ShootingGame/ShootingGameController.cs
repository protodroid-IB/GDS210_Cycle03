using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class ShootingGameController : MonoBehaviour {

    public SequencePool sequencePool;
    public Target[] targets;
    int poolIndex = 0;
    int sequenceIndex = 0;
    int targetIndex = 0;
    [HideInInspector] public int score;
    public Text scoreText;
    public Text highscoreText;
    
    [SerializeField] ScoreRecords scoreRecord;

    public void Start()
    {
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

    public void StartCycle()
    {
        ResetTargets();
        sequencePool.pool = sequencePool.pool.OrderBy(x => Random.value).ToList();
        score = 0;
        poolIndex = 0;
        sequenceIndex = 0;
        targetIndex = 0;
        Invoke("Cycle", 3f);
    }

    void Cycle()
    {
        if (poolIndex < sequencePool.pool.Count)
        {
            if (sequenceIndex < sequencePool.pool[poolIndex].sequence.Count)
            {
                var subSequence = sequencePool.pool[poolIndex].sequence[sequenceIndex];
                if (subSequence.randomize)
                {
                    targets[subSequence.targetID[Random.Range(0, subSequence.targetID.Count)]].FlipUp(subSequence.targetTime);
                    targetIndex = 0;
                    sequenceIndex++;
                    Invoke("Cycle", subSequence.exitTime);
                }
                else if (targetIndex < subSequence.targetID.Count)
                {
                    targets[subSequence.targetID[targetIndex]].FlipUp(subSequence.targetTime);
                    targetIndex++;
                    Invoke("Cycle", subSequence.timeInterval);
                }
                else
                {
                    targetIndex = 0;
                    sequenceIndex++;
                    Invoke("Cycle", subSequence.exitTime);
                }
            }
            else
            {
                sequenceIndex = 0;
                targetIndex = 0;
                poolIndex++;
                Cycle();
            }
        }
        else
        {
            Invoke("EndGame", 3);
        }
    }

    void EndGame()
    {
        StopCycle();
        GameManager.gameManager.UpdateHighScore(scoreRecord);
    }

    public void StopCycle()
    {
        ResetTargets();
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
        scoreRecord.currentScore = score;
    }
    
    public void RestartMiniGame(string minigame)
    {
        GameManager.gameManager.RestartMiniGame(minigame);
    }
}