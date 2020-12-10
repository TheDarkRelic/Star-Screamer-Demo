using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    public static int score = 0;
    public static int highScore = 0; 

    void awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
    }

    public static void AddScore(int points)
    {
        score += points;
    }

}
