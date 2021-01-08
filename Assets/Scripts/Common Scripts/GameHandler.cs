using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameHandler : MonoBehaviour
{
    public enum InputState { MenuInput, PlayerInput}

    public InputState inputstate;

    bool _isGameOver;
    [SerializeField] Image _pauseMenuCanvas= null;
    [SerializeField] UIHandler uiHandler = null;
    Animator _pauseAnimator = null;
    private Resolution res;

    private void Start()
    {
        inputstate = InputState.PlayerInput;
        SceneManager.GetActiveScene();
        UnscaledTimePauseAnimator();
        SetResolution();
    }

    private void Update()
    {
        switch (inputstate)
        {
            case InputState.MenuInput:

                if (Input.GetButtonDown("Pause"))
                    ResumeGame();

                if (Input.GetButtonDown("Back")) LoadMainMenu();

                if (_isGameOver == true && Input.GetButtonDown("Restart")) RestartLevel();

                if (_isGameOver == true && Input.GetButtonDown("Menu")) LoadMainMenu();
                break;

            case InputState.PlayerInput:
                if (Input.GetButtonDown("Pause"))
                    PauseGame();
                break;
        }
        
    }

    private void UnscaledTimePauseAnimator()
    {
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        if (_pauseAnimator != null)
        {
            _pauseAnimator.updateMode = AnimatorUpdateMode.UnscaledTime;
        }
    }

    public void RestartLevel()
    {
        var scene = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(scene);
    }

    private void SetResolution()
    {
        res = Screen.currentResolution;
        if (res.refreshRate == 60)
            QualitySettings.vSyncCount = 1;
        if (res.refreshRate == 120)
            QualitySettings.vSyncCount = 2;
        print(QualitySettings.vSyncCount);
    }

    private void PauseGame()
    {
        _pauseAnimator.SetBool("isPaused", true);
        _pauseMenuCanvas.gameObject.SetActive(true);
        inputstate = InputState.MenuInput;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        inputstate = InputState.PlayerInput;
        _pauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    public void LoadMainMenu()
    {
        inputstate = InputState.MenuInput;
        SceneManager.LoadScene("Main_Menu");
    }

    public void GameOver()
    {
        inputstate = InputState.MenuInput;
        _isGameOver = true;
    }

    private void LoadEndingScreen()
    {
        inputstate = InputState.MenuInput;
        SceneManager.LoadScene(2);
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
