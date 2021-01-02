using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    bool _isGameOver;
    [SerializeField] Image _pauseMenuCanvas;
    Animator _pauseAnimator;
    bool isPaused = false;

    private void Start()
    {
        Scene currentScene = SceneManager.GetActiveScene();

        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        if (_pauseAnimator != null)
        {
            _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }

    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
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

        if (_isGameOver == true && Input.GetKeyDown(KeyCode.R)) RestartLevel();

        if (_isGameOver == true && Input.GetKeyDown(KeyCode.Return)) LoadMainMenu();

        if (Input.GetKeyDown(KeyCode.Escape)) Application.Quit();
    }

    private static void RestartLevel()
    {
        string scene = SceneManager.GetActiveScene().name;
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
        SceneManager.LoadScene("Main_Menu");
    }

    public void GameOver()
    {
        _isGameOver = true;
    }

    private void LoadingEndScreen()
    {
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
