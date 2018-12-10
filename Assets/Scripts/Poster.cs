using UnityEngine;
using TMPro;

public class Poster : MonoBehaviour {
    [SerializeField] ScoreRecords scoreRecord;
    [SerializeField] TMP_Text highScoreText;

    private void Start()
    {
        highScoreText.text = "$" + scoreRecord.highestScore.ToString();
    }

    public void UpdateScore()
    {
        highScoreText.text = "$" + scoreRecord.highestScore.ToString();

    }

}
