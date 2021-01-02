using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R)) LoadSinglePlayerGame();

        if (Input.GetKeyDown(KeyCode.Return)) LoadMainMenu();

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    private void LoadMainMenu() => SceneManager.LoadScene(0);

    public void LoadSinglePlayerGame() => SceneManager.LoadScene(1);
}
