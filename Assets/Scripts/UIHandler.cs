using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class UiHandler : MonoBehaviour
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
    private Score _score;
    void Awake()
    {
        _score = GetComponent<Score>();
        SetGameOverText();
        _scoreText.text = "Score: " + 0;
        _highScoreText.text = "High Score:" + Score.highScore;

        _gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        if (_gameHandler == null)
        {
            Debug.LogError("GameHandler is Null");
        }
    }

    private void SetGameOverText()
    {
        gameOver = false;
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void CheckForHighScore(int playerScore)
    {
        if (playerScore < Score.highScore)
            return;;
        {
            Score.highScore = playerScore;
            SetHighScore();
        }
    }

    private void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", Score.highScore);
        _highScoreText.text = "High Score:" + Score.highScore;
    }

    public void UpdateLives(int currentLives)
    {
        _livesImage.sprite = _liveSprites[currentLives];

        if (currentLives < 1)
        {
            _gameHandler.GameOver();
            GameOverSequence();
        }
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
}
