using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager instance = null;
    [SerializeField] int lives = 1;
    [SerializeField] int score = 0;
    [SerializeField] Text livesText;
    [SerializeField] Text scoreText;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != null)
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }
    // Start is called before the first frame update
    void Start()
    {
        livesText.text = lives.ToString();
        scoreText.text = score.ToString();
    }

    public void AddScore(int point)
    {
        score += point;
        scoreText.text = score.ToString();
    }
   

    public void PlayerDeathProcess()
    {
        if(lives>1)
        {
            TakeLife();
        }
        else
        {
            ResetGame();
        }
    }

    void TakeLife()
    {
        lives--;
        var currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = lives.ToString();
    }

    void ResetGame()
    {
        SceneManager.LoadScene(0);
        Destroy(gameObject);
    }
}
