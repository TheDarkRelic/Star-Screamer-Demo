using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public  class UIHandler : MonoBehaviour, IScoreable
{

    [SerializeField] private Sprite[] liveSprites;
    [SerializeField] private Text scoreText;
    [SerializeField] private Text highScoreText;
    [SerializeField] private Image livesImage;
    [SerializeField] private Text gameOverText;
    [SerializeField] private Text restartText;
    [SerializeField] private Text mainMenuText;
    [SerializeField] private Text readyText;
    [SerializeField] private GameHandler gameHandler;
    public bool gameOver;
    public int score = 0;
    public int highScore = 0;

    private void Awake()
    {
        SetGameOverText();
        InitializeScore();
    }

    private void InitializeScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore", 0);
        scoreText.text = "Score: " + 0;
        highScoreText.text = "High Score:" + highScore;
    }

    private void SetGameOverText()
    {
        gameOver = false;
        gameOverText.gameObject.SetActive(false);
    }

    public void Score(int scoreAmount)
    {
        score += scoreAmount;
        UpdateScore(score);
    }

    private void UpdateScore(int scoreTotal)
    {
        scoreText.text = "Score: " + scoreTotal;
    }

    private IEnumerator ProcessHighScore()
    {
        if (score <= highScore) yield break;
        highScore = score;
        SetHighScore();
        yield return new WaitForSeconds(2);

    }

    private void CheckForHighScore()
    {
        StartCoroutine(ProcessHighScore());
    }

    private void SetHighScore()
    {
        PlayerPrefs.SetInt("HighScore", highScore);
        highScoreText.text = "High Score:" + highScore;
    }

    private void UpdateLives(int currentLives)
    {

        if (currentLives < 1)
        {
            currentLives = 0;
            gameHandler.GameOver();
            GameOverSequence();
        }
        livesImage.sprite = liveSprites[currentLives];
    }

    private void GameOverSequence()
    {
        gameOver = true;
        restartText.gameObject.SetActive(true);
        mainMenuText.gameObject.SetActive(true);
        StartCoroutine(FlashGameOverEnabled());
    }

    private IEnumerator FlashGameOverEnabled()
    {
        while (gameOver)
        {
            gameOverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.1f);
            gameOverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.05f); 
        }
    }

    private IEnumerator FlashReadyEnabled()
    {
        var flashTime = 0;
        while (flashTime < 5)
        {
            readyText.gameObject.SetActive(true);
            yield return new WaitForSeconds(.5f);
            readyText.gameObject.SetActive(false);
            yield return new WaitForSeconds(.5f);
            flashTime++;
        }
    }

    private void OnEnable()
    {
        StartCoroutine(FlashReadyEnabled());
        HitDamage.onHitAction += UpdateLives;
        EventsList.OnScoreAction += Score;
        EventsList.OnPlayerDeath += CheckForHighScore;
        EventsList.OnHealthPickup += UpdateLives;
    }

    private void OnDisable()
    {
        HitDamage.onHitAction -= UpdateLives;
        EventsList.OnScoreAction -= Score;
        EventsList.OnPlayerDeath -= CheckForHighScore;
        EventsList.OnHealthPickup -= UpdateLives;
    }

}

