using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShootingGameController : MonoBehaviour {

    public TargetSequence targetSequence;
    public Target[] targets;
    public List<int> idCache = new List<int>();
    int sequenceIndex = 0;
    int targetIndex = 0;
    [HideInInspector] public int score;
    public Text scoreText;

    public void Start()
    {
        ResetTargets();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            score = 0;
            sequenceIndex = 0;
            targetIndex = 0;
            idCache.Clear();
            Cycle();
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            ResetTargets();
        }
    }

    void Cycle()
    {
        if (sequenceIndex < targetSequence.sequence.Count)
        {
            Sequence sequence = targetSequence.sequence[sequenceIndex];
            if (sequence.randomize)
            {
                int id = Random.Range(0, sequence.targetID.Count);

                while (idCache.Contains(id))
                {
                    id = Random.Range(0, sequence.targetID.Count);
                }

                idCache.Add(id);
                targets[sequence.targetID[id]].FlipUp(sequence.targetTime);
                targetIndex = 0;
                sequenceIndex++;
                Invoke("Cycle", sequence.exitTime);
            }
            else if (targetIndex < sequence.targetID.Count)
            {
                idCache.Clear();
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
}
