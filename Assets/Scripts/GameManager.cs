
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public static bool isGameStarted;
    public static bool isGameEnded;
    public static int LevelNumber = 0;
    public int HighScore;
    public Text ScoreText;
    public Text HighScoreText;

    public GameObject LevelCompletedPanel, LevelFailedPanel,ScorePanel,HighScorePanel,InstructionPanel,StartGamePanel;
    public List<GameObject> Levels = new List<GameObject>();
    [SerializeField] int LevelIndex = 0;
    int PointsCounted;
    public int LastCounted;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            CreateLevel();
        }
    }
    void Start()
    {
        HighScore = PlayerPrefs.GetInt("HighScore");
    }
    public void CreateLevel()
    {
        LevelIndex = PlayerPrefs.GetInt("LevelNo", 0);
        if (LevelIndex > Levels.Count - 1)
        {
            LevelIndex = 0;
            PlayerPrefs.GetInt("LevelNo", 0);
        }
        Instantiate(Levels[LevelIndex]);
        LevelNumber = LevelIndex + 1;
    }
    public void StartGame()
    {
        isGameStarted = true;
        isGameEnded = false;
        ScorePanel.SetActive(true);
        HighScorePanel.SetActive(true);
        QuitInstructions();
    }
    public void EndGame()
    {
        isGameEnded = true;
    }
    public void OnLevelCompleted()
    {
        LevelCompletedPanel.SetActive(true);
    }
    public void OnLevelFailed()
    {
        LevelFailedPanel.SetActive(true);
    }
    public void RestartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameStarted = false;
    }
    public void NextLevel()
    {
        LevelIndex++;
        PlayerPrefs.SetInt("LevelNo", LevelIndex);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        isGameStarted = false;
    }
    public void Instructions()
    {
        InstructionPanel.SetActive(true);
        StartGamePanel.SetActive(false);
    }
    public void QuitInstructions()
    {
        InstructionPanel.SetActive(false);
        StartGamePanel.SetActive(true);
    }
    public void PointCounter(int thisCount)
    {

        PointsCounted = LastCounted + thisCount;
        LastCounted = PointsCounted;
        Debug.Log(LastCounted);
        ScoreText.text = LastCounted.ToString();
        HighScoreText.text = HighScore.ToString();
        if (LastCounted > HighScore)
        {
            PlayerPrefs.SetInt("HighScore", LastCounted);
        }
    }
}
