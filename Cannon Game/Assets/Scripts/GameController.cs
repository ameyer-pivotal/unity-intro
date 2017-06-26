using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Text scoreText;
    public GameObject gameOverPanel;

    private int _score;
    public int score
    {
        get { return _score; }
        set
        {
            _score = value;
            scoreText.text = "Score: " + score;
        }
    }

    public bool gameOver { get { return gameOverPanel.activeSelf; } }

    void Start()
    {
        Cursor.visible = false;
        instance = this;
        score = 0;
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Cursor.visible = true;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("main");
    }
}
