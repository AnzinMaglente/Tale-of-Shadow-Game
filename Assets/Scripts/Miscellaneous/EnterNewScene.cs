using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EnterNewScene : MonoBehaviour
{
    public DoorScript doorScript;

    public float holdDuration = 1f;
    public Image fillCircle;

    public GameObject player;
    public GameObject DoorObj;
    private float holdTimer = 0;
    private bool isHolding = false;
    public string sceneName;
    public float TimeBeforeNextScene;

    void Update()
    {
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
        }

        if (DoorObj == null)
        {
            DoorObj = GameObject.FindWithTag("Door");
            if (DoorObj != null)
            {
                doorScript = DoorObj.GetComponent<DoorScript>();
            }
        }

        if (isHolding && doorScript.isAtDoor)
        {
            holdTimer += Time.deltaTime;
            fillCircle.fillAmount = holdTimer / holdDuration;
            if (holdTimer >= holdDuration)
            {
                StartCoroutine(EnterArea());
                SceneManager.LoadSceneAsync(sceneName);
            }
        }
    }

    public IEnumerator EnterArea()
    {
        ResetHold();
        player.SetActive(false);
        yield return new WaitForSeconds(TimeBeforeNextScene);
    }

    public void OnHold(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            isHolding = true;
        }
        else if (context.canceled)
        {
            ResetHold();
        }
    }

    private void ResetHold()
    {
        isHolding = false;
        holdTimer = 0;
        fillCircle.fillAmount = 0;
    }
}
