using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndingScreen : MonoBehaviour
{
    private void Update()
    {               
        if (Input.GetButtonDown("Menu")) LoadMainMenu();
                     
        if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    public void LoadMainMenu() => SceneManager.LoadScene(0);

    public void LoadSinglePlayerGame() => SceneManager.LoadScene(1);
}
