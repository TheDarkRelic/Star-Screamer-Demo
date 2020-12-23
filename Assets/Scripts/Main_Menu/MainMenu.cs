using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadSinglePlayerGame();
        }
    }

    public void LoadSinglePlayerGame()
    {
        SceneManager.LoadScene(1);
    }

}


