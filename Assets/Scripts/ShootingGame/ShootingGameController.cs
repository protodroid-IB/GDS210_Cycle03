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
    List<int> randomIdCache = new List<int>();

    public float speedThreshold;
    public float speedMultiplyer;
    public float accuracyMultiplyer;
    [SerializeField] int shotsFired = 0;
    [SerializeField] int targetsHit = 0;
    float accuracyBonus;
    int speedBonus;

    [HideInInspector] public int score;
    public Text scoreText;
    public Text speedText;
    public Text accuraccyText;
    [SerializeField] ScoreRecords scoreRecord;

    Gun[] guns;


    public void Start()
    {
        guns = FindObjectsOfType<Gun>();

        foreach(Gun gun in guns)
        {
            gun.sgc = this;
        }

        StopCycle();
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
        StopCycle();
        accuracyBonus = 0;
        speedBonus = 0;
        sequencePool.pool = sequencePool.pool.OrderBy(x => Random.value).ToList();
        score = 0;
        poolIndex = 0;
        sequenceIndex = 0;
        targetIndex = 0;
        shotsFired = 0;
        targetsHit = 0;
        speedText.text = speedBonus.ToString("00000");
        Cycle();
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
                    int randomID = Random.Range(subSequence.targetID[0], subSequence.targetID.Count);
                    while (randomIdCache.Contains(randomID) && randomIdCache.Count < subSequence.targetID.Count)
                    {
                        randomID = Random.Range(subSequence.targetID[0], subSequence.targetID.Count);
                    }
                    randomIdCache.Add(randomID);
                    targets[randomID].FlipUp(subSequence.targetTime);
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
            Invoke("EndGame",0);
        }
    }

    void EndGame()
    {
        StopCycle();
        score += speedBonus;
        score += (int)(accuracyBonus / 100 * score);
        scoreRecord.currentScore = score;
        scoreText.text = score.ToString("00000");
        GameManager.gameManager.UpdateHighScore(scoreRecord);
    }

    public void StopCycle()
    {
        CancelInvoke();
        foreach (Target target in targets)
        {
            target.FlipDown();
        }
    }

    public void Addscore(int points, float speed)
    {
        speedBonus += Mathf.CeilToInt((speedThreshold - speed) * speedMultiplyer);
        speedText.text = speedBonus.ToString("00000");
        Addscore(points);
    }

    public void Addscore(int points)
    {
        score += points;
        scoreText.text = score.ToString("00000");
        targetsHit++;
        scoreRecord.currentScore = score;
    }
    
    public void RestartMiniGame(string minigame)
    {
        GameManager.gameManager.RestartMiniGame(minigame);
    }

    public void AddShot()
    {
        shotsFired++;
        accuracyBonus = targetsHit > 0 ? (((float)targetsHit / shotsFired) * 100) : 0;
        accuraccyText.text = "%" + accuracyBonus.ToString("00.00");
    }
}