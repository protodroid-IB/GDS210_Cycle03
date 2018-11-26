using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

    public class ScoreManager : MonoBehaviour
    {
        public ScoreRecords shootingGameScores;
        public ScoreRecords throwingGameScores;
        public ScoreRecords barGameScores;

        [SerializeField] TMP_Text shootingGameHighscore;
        [SerializeField] TMP_Text throwingGameHighscore;
        [SerializeField] TMP_Text barGameHighscore;

    private void Start()
    {
        UpdateScores();
    }

    void UpdateScores()
    {
        shootingGameHighscore.text = shootingGameScores.highestScore.ToString();
        throwingGameHighscore.text = throwingGameScores.highestScore.ToString();
        barGameHighscore.text = barGameScores.highestScore.ToString();

    }

    public void UpdateHighScore(ScoreRecords game)
    {
        game.highestScore = game.currentScore;
    }
}

