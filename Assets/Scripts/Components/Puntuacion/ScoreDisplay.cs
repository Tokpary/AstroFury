using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScoreDisplay : MonoBehaviour
{
    public Text scoreText;
    private ScoreManager scoreManager;
    private const string scoreFilePath = "score.dat";

    void Start()
    {
        scoreManager = ScoreManager.LoadScore(Path.Combine(Application.persistentDataPath, scoreFilePath));
        Debug.Log(Path.Combine(Application.persistentDataPath, scoreFilePath));
        UpdateScoreText();
    }

    void Update()
    {
        if (scoreManager.IsDirty)
        {
            UpdateScoreText();
            scoreManager.IsDirty = false;
        }
    }

    void UpdateScoreText()
    {
        scoreText.text = "Puntuación: " + scoreManager.Score;
    }

    void OnApplicationQuit()
    {
        scoreManager.SaveScore(Path.Combine(Application.persistentDataPath, scoreFilePath));
    }

    public void AddScore(int points)
    {
        scoreManager.AddScore(points);
    }
}
