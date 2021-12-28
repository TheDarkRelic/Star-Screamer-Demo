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
    private InputState playerInput;
    private InputState menuInput;

    private void Start()
    {
        playerInput = InputState.PlayerInput;
        menuInput = InputState.MenuInput;
        inputstate = InputState.PlayerInput;
        SceneManager.GetActiveScene();
        UnscaledTimePauseAnimator();
        SetResolution();
    }

    private void Update()
    {
        if (_isGameOver) inputstate = InputState.MenuInput;
        switch (inputstate)
        {
            case InputState.MenuInput:

                if (Input.GetButtonDown("Pause"))
                    ResumeGame();
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
    }

    private void PauseGame()
    {
        _pauseMenuCanvas.gameObject.SetActive(true);
        _pauseAnimator.SetBool("isPaused", true);
        inputstate = InputState.MenuInput;
        Time.timeScale = 0;
    }

    public void ResumeGame()
    {
        _pauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
        StartCoroutine(ResumeState(playerInput));
        
    }

    private IEnumerator ResumeState(InputState state)
    {
        yield return new WaitForSeconds(.5f);
        inputstate = state;
    }

    public void LoadMainMenu()
    {
        inputstate = InputState.MenuInput;
        SceneManager.LoadScene(0);
    }

    public void GameOver()
    {
        inputstate = InputState.MenuInput;
        _isGameOver = true;
    }

    private void LoadEndingScreen()
    {
        StartCoroutine(LoadingEndScreen());
    }

    private IEnumerator LoadingEndScreen()
    {
        yield return new WaitForSeconds(1.5f);
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
