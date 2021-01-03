using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    bool _isGameOver;
    [SerializeField] Image _pauseMenuCanvas= null;
    [SerializeField] UIHandler uiHandler = null;
    Animator _pauseAnimator = null;
    bool isPaused = false;

    private void Start()
    {
        SceneManager.GetActiveScene();

        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        if (_pauseAnimator != null)
        {
            _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

    }
    private void Update()
    {
        if (Input.GetButtonDown("Cancel") && isPaused) LoadMainMenu();
        if (Input.GetButtonDown("Pause"))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
                
            }
        }

        if (_isGameOver == true && Input.GetButtonDown("Cancel")) RestartLevel();

        if (_isGameOver == true && Input.GetButtonDown("Submit")) LoadMainMenu();

        if (Input.GetButtonDown("Cancel")) Application.Quit();
    }

    public void RestartLevel()
    {
        uiHandler.CheckForHighScore();
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    private void PauseGame()
    {
        isPaused = true;
        _pauseAnimator.SetBool("isPaused", true);
        _pauseMenuCanvas.gameObject.SetActive(true);
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        isPaused = false;
        _pauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        uiHandler.CheckForHighScore();
        SceneManager.LoadScene("Main_Menu");
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    private void LoadingEndScreen()
    {
        uiHandler.CheckForHighScore();
        SceneManager.LoadScene(2);
    }

    public void LoadEndingScreen()
    {
        Invoke("LoadingEndScreen", 1);
    }

    private void OnEnable()
    {
        BossAnimEvent.onBossDestroy += LoadEndingScreen;
    }

    private void OnDisable()
    {
        BossAnimEvent.onBossDestroy -= LoadEndingScreen;
    }
}
