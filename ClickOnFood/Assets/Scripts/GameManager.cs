using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> target;
    private float spawnTime = 1f;
    public TextMeshProUGUI scoreText;
    private int score;

    public GameObject pauseScreen;
    private bool paused;

    public TextMeshProUGUI gameOverText;
    public bool isGameActive;
    public Button restartButton;
    
    
    private int lives=3;
    public TMP_Text livesText;

    public GameObject titleScreen;
    public static GameManager instance;


    private void Awake()
    {
        instance = this;
    }

    public void StartGame(int difficulty)
    {
        spawnTime = spawnTime / difficulty;
        isGameActive = true;
        score = 0;
        IncrementScore(0);
        StartCoroutine("SpawnTarget");
        DecrementLives(3);
        titleScreen.gameObject.SetActive(false);
        
    }
    public void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            ChangedPause();
        }
    }

    void ChangedPause()
    {
        if(!paused)
        {
            pauseScreen.SetActive(true);
            paused = true;
            Time.timeScale = 0;

        }
        else
        {
            paused = false;
            pauseScreen.SetActive(false);
            Time.timeScale=1;
        }
    }

    IEnumerator SpawnTarget()
    {
        while(isGameActive == true)
        {
            yield return new WaitForSeconds(spawnTime);
            int index = Random.Range(0, target.Count);
            Instantiate(target[index]);

        }
    }
    
    public void IncrementScore(int scoreAdd)
    {
        score+=scoreAdd;
        scoreText.text = "Score: " + score;
    }

    public void DecrementLives(int decreaseLife)
    {
        lives += decreaseLife;
        livesText.text = "Lives:" + lives;
        if (lives <= 0)
        {
            GameOver();
        }
        
        
    }
    public void GameOver()
    {
        restartButton.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

   
}
