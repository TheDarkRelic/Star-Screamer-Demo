using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    bool _isGameOver;
    public bool _isSinglePlayer = true;
    [SerializeField] Image _pauseMenuCanvas;
    Animator _pauseAnimator;


    private void Start()
    {
        
        Scene currentScene = SceneManager.GetActiveScene();
        if (currentScene.name == "Single_Player_Mode")
        {
            _isSinglePlayer = true;
        }
        else
        {
            _isSinglePlayer = false;
        }

        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        if (_pauseAnimator == null)
        {
            Debug.LogError("Pause Menu Animator is Null");
        }

        _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            
            _pauseAnimator.SetBool("isPaused", true);
            _pauseMenuCanvas.gameObject.SetActive(true);
            Time.timeScale = 0;
        }

        if (_isGameOver == true && Input.GetKeyDown(KeyCode.R))
        {
           string scene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(scene);
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            LoadMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Application.Quit();
        }
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("Main_Menu");
    }

    public void GameOver()
    {
        _isGameOver = true;
    }
}
