using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public static GameController instance;

    public Text scoreText;

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

    void Start()
    {
        instance = this;
        score = 0;
    }

    public void StartNewGame()
    {
        SceneManager.LoadScene("main");
    }
}
