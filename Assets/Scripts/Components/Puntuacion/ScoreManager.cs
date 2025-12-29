using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.Serialization.Formatters.Binary;
using System;
using System.IO;

[Serializable]
public class ScoreManager
{
    private int score;
    private bool isDirty=false;

    public int Score
    {
        get => score;
        private set
        {
            if (score != value)
            {
                score = value;
                isDirty = true;
            }
        }
    }

    public bool IsDirty
    {
        get => isDirty;
        set => isDirty = value;
    }

    public void AddScore(int points)
    {
        Score += points;
    }

    public void SaveScore(string filePath)
    {
        BinaryFormatter bf = new BinaryFormatter();
        using (FileStream file = File.Create(filePath))
        {
            bf.Serialize(file, this);
        }
    }

    public static ScoreManager LoadScore(string filePath)
    {
        if (File.Exists(filePath))
        {
            BinaryFormatter bf = new BinaryFormatter();
            using (FileStream file = File.Open(filePath, FileMode.Open))
            {
                return (ScoreManager)bf.Deserialize(file);
            }
        }
        else
        {
            return new ScoreManager();
        }
    }
}
