using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance { get; private set; }

    public GameObject player;
    public GameObject gameOverScreen;
    public TMP_Text gameOverText;
    public UnityEngine.SceneManagement.Scene currentScene;

    public int pointsAmount;
    public TMP_Text pointsText;

    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
            return;
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this);
        }
    }
    void Start()
    {
        pointsAmount = 0;
        Orb.OnOrbCollect += increasePoints;
        PlayerHealthController.OnPlayerDeath += GameOverScreen;
        gameOverScreen.SetActive(false);
    }

    void GameOverScreen()
    {
        gameOverScreen.SetActive(true);
        gameOverText.text = "Total Score: " + pointsAmount;
    }

    public void ResetGame()
    {
        gameOverScreen.SetActive(false);
        pointsAmount = 0;
        pointsText.text = pointsAmount.ToString();
    }

    void increasePoints(int amount)
    {
        pointsAmount += amount;
        pointsText.text = pointsAmount.ToString();
    }

    public void ChangeScene(string sceneName)
    {
        SceneManager.LoadSceneAsync(sceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
