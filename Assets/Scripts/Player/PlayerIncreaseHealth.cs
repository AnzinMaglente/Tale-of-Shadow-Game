using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIncreaseHealth : MonoBehaviour
{
    public PlayerHealthController playerHealthController;
    public HealthUI healthUI;

    private GameObject player;
    private GameObject Hearts;

    private void Update()
    {
        if (Hearts == null)
        {
            Hearts = GameObject.FindWithTag("Lives");
            if (Hearts != null)
            {
                healthUI = Hearts.GetComponent<HealthUI>();
            }
        }
        if (player == null)
        {
            player = GameObject.FindWithTag("Player");
            if (player != null)
            {
                playerHealthController = player.GetComponent<PlayerHealthController>();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (playerHealthController.currentHealth < playerHealthController.maxHealth) {
                playerHealthController.currentHealth += 1;
                healthUI.UpdateHearts(playerHealthController.currentHealth);
            }
            Destroy(gameObject);
        }
    }
}
