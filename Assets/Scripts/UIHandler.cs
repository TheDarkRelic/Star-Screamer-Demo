using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class UIHandler : MonoBehaviour, IScoreable
{

    [SerializeField] Sprite[] _liveSprites;
    [SerializeField] Text _scoreText;
    
    [SerializeField] Text _highScoreText;
    [SerializeField] Image _livesImage;
    [SerializeField] Text _gameOverText;
    [SerializeField] Text _restartText;
    [SerializeField] Text _mainMenuText;
    [SerializeField] Image _pauseMenuCanvas;
    [SerializeField] GameHandler _gameHandler;
    public bool gameOver;
    public int score = 0;
    public int highScore = 0;

    void Awake()
    {
        SetGameOverText();
        InitializeScore();
    }

    private void InitializeScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        _scoreText.text = "Score: " + 0;
        _highScoreText.text = "High Score:" + highScore;
    }

    private void SetGameOverText()
    {
        gameOver = false;
        _gameOverText.gameObject.SetActive(false);
    }

    public void Score(int scoreAmount)
    {
        score += scoreAmount;
        UpdateScore(score);
    }
    public void UpdateScore(int scoreTotal)
    {
        _scoreText.text = "Score: " + scoreTotal;
    }

    public IEnumerator ProcessHighScore()
    {
        if (score > highScore)
        {
            highScore = score;
            SetHighScore();
            yield return new WaitForSeconds(2);
        }

    }

    void CheckForHighScore()
    {
        StartCoroutine(ProcessHighScore());
    }

    private void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        _highScoreText.text = "High Score:" + highScore;
    }

    public void UpdateLives(int currentLives)
    {

        if (currentLives < 1)
        {
            currentLives = 0;
            _gameHandler.GameOver();
            GameOverSequence();
        }
        _livesImage.sprite = _liveSprites[currentLives];
    }

    private void GameOverSequence()
    {
        gameOver = true;
        _restartText.gameObject.SetActive(true);
        _mainMenuText.gameObject.SetActive(true);
        StartCoroutine(FlashGameOverEnabled());
    }

    IEnumerator FlashGameOverEnabled()
    {
        while (gameOver)
        {
            _gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(1);
            _gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(1); 
        }
    }

    public void ResumeGame()
    {
        _pauseMenuCanvas.gameObject.SetActive(false);
        Time.timeScale = 1;
    }

    void OnEnable()
    {
        HitDamage.OnHitAction += UpdateLives;
        EventsList.OnScoreAction += Score;
        EventsList.OnPlayerDeath += CheckForHighScore;
    }

    void OnDisable()
    {
        HitDamage.OnHitAction -= UpdateLives;
        EventsList.OnScoreAction -= Score;
        EventsList.OnPlayerDeath -= CheckForHighScore;
    }

}
