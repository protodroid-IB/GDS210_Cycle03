using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System;

public class ScoreManager : MonoBehaviour
    {
        // References to the Scriptable Objects
        public ScoreRecords shootingHighScores;
        public ScoreRecords throwingHighScores;
        public ScoreRecords barHighScores;


    private void Start()
    {
        // Ensure current scores start at 0.
        barHighScores.currentScore = 0;
        throwingHighScores.currentScore = 0;
        shootingHighScores.currentScore = 0;
    }

    // Loads the save data from disk.
    public void LoadScores()
    {
        if (File.Exists(Application.persistentDataPath + "/SavedData.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/SavedData.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();

            shootingHighScores.highestScore = data.shootingHighScores;
            throwingHighScores.highestScore = data.throwingHighScores;
            barHighScores.highestScore = data.barHighScores;
        }
    }

    // Save data to disk.
    public void SaveScores()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/SavedData.dat");

        SaveData data = new SaveData();

        data.shootingHighScores = shootingHighScores.highestScore;
        data.throwingHighScores = throwingHighScores.highestScore;
        data.barHighScores = barHighScores.highestScore;


        bf.Serialize(file, data);
        file.Close();
    }

    // Resets all save data and SO's to 0 and update posters.
    public void ResetHighScores()
    {
        shootingHighScores.highestScore = 0;
        throwingHighScores.highestScore = 0;
        barHighScores.highestScore = 0;

        SaveScores();
        LoadScores();

        GameManager.gameManager.UpdatePosters();
        print("reset scores");
    }
}

// Save data container.
[Serializable]
public class SaveData
{
    public int shootingHighScores;
    public int throwingHighScores;
    public int barHighScores;

}



