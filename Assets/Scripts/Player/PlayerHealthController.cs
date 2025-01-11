using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    private PlayerMovementController _playerMovementController;
    [SerializeField]
    public HealthUI healthUI;

    private GameObject Hearts;
    private SpriteRenderer _spriteRenderer;
    public Rigidbody2D rb;

    public int maxHealth = 5;
    public int currentHealth;
    public Color defaultColor;
    public bool isBeingKnockedBack;
    public float horizontalKnockback = -5f;
    public float verticalKnockback = 2f;

    public static event Action OnPlayerDeath;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        _playerMovementController = GetComponent<PlayerMovementController>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        defaultColor = _spriteRenderer.color;
    }

    private void Update()
    {
        if (Hearts == null)
        {
            Hearts = GameObject.FindWithTag("Lives");
            if (Hearts != null)
            {
                healthUI = Hearts.GetComponent<HealthUI>();
                HealFull();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        if (gameObject != null)
        {
            currentHealth -= damage;
            healthUI.UpdateHearts(currentHealth);
            SoundManager.instance.PlayPlayerHurtSound();

            StartCoroutine(FlashRed());

            if (currentHealth <= 0)
            {
                OnPlayerDeath.Invoke();
            }

            StartCoroutine(Knockback());
        }
    }

    private IEnumerator FlashRed()
    {
        _spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        _spriteRenderer.color = defaultColor;
    }

    private IEnumerator Knockback()
    {
        isBeingKnockedBack = true;
        rb.velocity = new Vector2(horizontalKnockback * _playerMovementController.horizontalMovement, verticalKnockback);
        yield return new WaitForSeconds(0.2f);
        isBeingKnockedBack = false;
    }

    public void HealFull()
    {
        currentHealth = maxHealth;
        healthUI.SetMaxHearts(maxHealth);
    }
}
