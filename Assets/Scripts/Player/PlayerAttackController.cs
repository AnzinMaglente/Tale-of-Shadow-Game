using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAttackController : MonoBehaviour
{
    [Header("Controllers")]

    public PlayerAnimatorController playerAnimatorController;

    [Header("Attack settings")]

    [SerializeField]
    public float attackTime;
    [SerializeField]
    private float _attackSpeed;
    [SerializeField]
    private float _attackCooldown;
    [SerializeField]
    private float _attackDamage;

    public bool OnEnemyHit = false;

    public void HorizontalSlash(InputAction.CallbackContext context)
    {
        if (attackTime <= 0f)
        {
            if (context.performed)
            {
                playerAnimatorController.anim.SetTrigger("onAttack");
                attackTime = _attackSpeed;
                SoundManager.instance.PlaySlashSound();
            }
        }
        else
        {
            attackTime -= Time.deltaTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Enemy")
        {
            Debug.Log("Enemy hit");
            SoundManager.instance.PlaySlimeDeathSound();
            Destroy(other.gameObject);
        }
    }
}
