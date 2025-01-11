using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Search : MonoBehaviour
{
    public GameObject gameManagerObj;
    public GameManager gameManager;

    void Update()
    {
        if (gameManagerObj == null)
        {
            gameManagerObj = GameObject.FindWithTag("GameManager");
            if (gameManagerObj != null)
            {
                gameManager = gameManagerObj.GetComponent<GameManager>();
            }
        }
    }

    public void ChangeSceneAction(string sceneName)
    {
        gameManager.ChangeScene(sceneName);
    }

    public void QuitGameAction()
    {
        gameManager.QuitGame();
    }

    public void ResetStats()
    {
        gameManager.ResetGame();
    }
}
