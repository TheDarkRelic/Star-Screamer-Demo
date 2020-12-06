using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public  class UIHandler : MonoBehaviour
{

    [SerializeField] Sprite[] _liveSprites;
    [SerializeField] Text _scoreText;
    public int highScore = 0; //check to see if this needs to be public
    [SerializeField] Text _highScoreText;
    [SerializeField] Image _livesImage;
    [SerializeField] Text _gameOverText;
    [SerializeField] Text _restartText;
    [SerializeField] Text _MainMenuText;
    [SerializeField] Image _pauseMenuCanvas;
    [SerializeField] GameHandler _gameHandler;
    public bool _gameOver;

    void Awake()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        SetGameOverText();
        _scoreText.text = "Score: " + 0;
        _highScoreText.text = "High Score:" + highScore;


        _gameHandler = GameObject.Find("GameHandler").GetComponent<GameHandler>();
        if (_gameHandler == null)
        {
            Debug.LogError("GameHandler is Null");
        }
    }

    private void SetGameOverText()
    {
        _gameOver = false;
        _gameOverText.gameObject.SetActive(false);
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore;
    }

    public void CheckForHighScore(int playerScore)
    {
        if (playerScore < highScore)
            return;;
        {
            highScore = playerScore;
            PlayerPrefs.SetInt("HighScore", highScore);
            _highScoreText.text = "High Score:" + highScore;
        }
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
        _gameOver = true;
        _restartText.gameObject.SetActive(true);
        _MainMenuText.gameObject.SetActive(true);
        StartCoroutine(FlashGameOverEnabled());
    }

    IEnumerator FlashGameOverEnabled()
    {
        while (_gameOver)
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
