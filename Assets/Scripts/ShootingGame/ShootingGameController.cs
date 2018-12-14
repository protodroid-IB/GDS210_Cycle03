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
    public float speedBasePoints;
    public float accuracyMultiplyer;
    int shotsFired = 0;
    int targetsHit = 0;
    int fastShotsHit = 0;
    float accuracyBonus;
    float speedBonus;

    [HideInInspector] public int score;
    public Text scoreText;
    public Text speedText;
    public Text accuraccyText;
    [SerializeField] ScoreRecords scoreRecord;

    public Color speedColor;
    public Color accuracyColor;
    public Text bonusLabelText;
    public Text bonusText;
    public Text labelText;
    public Text totalScoreText;
    int endSequence;
    float maxDelta; //for the end sequence
    float displayScore;
    float startTimer;

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
        if (startTimer > 0f)
        {
            totalScoreText.text = startTimer.ToString("0.00");
            startTimer -= Time.deltaTime;
        }
    }

    public void DisplayStartTimer()
    {
        StopCycle();
        ResetScore();
        bonusLabelText.gameObject.SetActive(false);
        bonusText.gameObject.SetActive(false);
        labelText.text = "GAME START";
        startTimer = 3f;
        labelText.gameObject.SetActive(true);
        totalScoreText.gameObject.SetActive(true);
        Invoke("StartCycle", 3f);
    }

    private void ResetScore()
    {
        accuracyBonus = 0;
        speedBonus = 0;
        score = 0;
        accuraccyText.text = "%" + accuracyBonus.ToString("00.00");
        speedText.text = fastShotsHit.ToString("00") + "/" + targetsHit.ToString("00");
        scoreText.text = score.ToString("00000");
    }

    public void StartCycle()
    {
        labelText.gameObject.SetActive(false);
        totalScoreText.gameObject.SetActive(false);
        sequencePool.pool = sequencePool.pool.OrderBy(x => Random.value).ToList();
        poolIndex = 0;
        sequenceIndex = 0;
        targetIndex = 0;
        shotsFired = 0;
        targetsHit = 0;
        fastShotsHit = 0;
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
            endSequence = 0;
            Invoke("EndSequence",1f);
            Debug.Log("game ended");
        }
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
        fastShotsHit++;
        Addscore(points);
    }

    public void Addscore(int points)
    {
        score += points;
        scoreText.text = score.ToString("00000");
        targetsHit++;
        speedText.text = fastShotsHit.ToString("00") + "/" + targetsHit.ToString("00");
        scoreRecord.currentScore = score;
    }
    
    public void RestartMiniGame(string minigame)
    {
        GameManager.instance.RestartMiniGame(minigame);
    }

    public void AddShot()
    {
        shotsFired++;
        accuracyBonus = targetsHit > 0 ? (((float)targetsHit / (float)shotsFired) * 100) : 0;
        accuraccyText.text = "%" + accuracyBonus.ToString("00.00");
    }

    void EndSequence() 
    {
        if (endSequence == 0) // display score
        {
            StopCycle();

            totalScoreText.text = score.ToString("00000");
            totalScoreText.gameObject.SetActive(true);

            endSequence = 1;
            Invoke("EndSequence", 0.5f);
        }
        else if (endSequence == 1) // display speed bonus
        {
            score += (int)speedBonus;
            maxDelta = speedBonus * 0.05f;

            bonusText.color = speedColor;
            bonusLabelText.color = speedColor;

            bonusText.text = "+" + speedBonus.ToString("00000");
            bonusLabelText.text = "SPEED BONUS";

            bonusText.gameObject.SetActive(true);
            bonusLabelText.gameObject.SetActive(true);

            endSequence = 2;
            Invoke("EndSequence", 1f);
        }
        else if (endSequence == 2) // add speed bonus
        {
            if (speedBonus > 0f && displayScore != score)
            {
                speedBonus = Mathf.MoveTowards(speedBonus, 0, maxDelta);
                displayScore = Mathf.MoveTowards(displayScore, score, maxDelta);

                bonusText.text = "+" + speedBonus.ToString("00000");
                totalScoreText.text = displayScore.ToString("00000");

                Invoke("EndSequence", 0.05f);
            }
            else
            {
                endSequence = 3;
                Invoke("EndSequence", 0.5f);
            }
        }
        else if (endSequence == 3) // display accuracy bonus
        {
            accuracyBonus = accuracyBonus / 100 * score;
            score += (int)(accuracyBonus);
            maxDelta = accuracyBonus * 0.05f;

            bonusText.color = accuracyColor;
            bonusLabelText.color = accuracyColor;

            bonusText.text = "+" + accuracyBonus.ToString("00000");
            bonusLabelText.text = "ACCURACY BONUS";

            endSequence = 4;
            Invoke("EndSequence", 1f);
        }
        else if (endSequence == 4) // add accuracy bonus
        {
            if (accuracyBonus > 0f && displayScore != score)
            {
                accuracyBonus = Mathf.MoveTowards(accuracyBonus, 0, maxDelta);
                displayScore = Mathf.MoveTowards(displayScore, score, maxDelta);

                bonusText.text = "+" + accuracyBonus.ToString("00000");
                totalScoreText.text = displayScore.ToString("00000");

                Invoke("EndSequence", 0.05f);
            }
            else
            {
                endSequence = 5;
                Invoke("EndSequence", 0.5f);
            }
        }
        else if (endSequence == 5) // display total score / highscore
        {
            bonusText.gameObject.SetActive(false);
            bonusLabelText.gameObject.SetActive(false);

            if (score > scoreRecord.highestScore)
            {
                labelText.text = "HIGH SCORE";
                labelText.gameObject.SetActive(true);

                scoreRecord.currentScore = score;
                GameManager.instance.UpdateHighScore(scoreRecord);
            }
        }
    }

    public void DoNothing()
    {

    }
}