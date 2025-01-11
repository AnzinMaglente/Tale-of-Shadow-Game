using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerDashController : MonoBehaviour
{
    [Header("Game Objects")]

    private PlayerMovementController _playerMovementController;
    private PlayerJumpController _playerJumpController;
    private Rigidbody2D _rb;

    [Header("Dash Settings")]

    [SerializeField]
    private float _dashingPower = 20f;
    [SerializeField]
    private float _dashingTime = 0.1f;
    [SerializeField]
    private float _dashingCooldown = 0.1f;
    private bool _canDash = true;
    public bool isDashing;

    [Header("Miscellaneous Settings")]

    [SerializeField]
    private TrailRenderer _tr;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerJumpController = GetComponent<PlayerJumpController>();
        _playerMovementController = GetComponent<PlayerMovementController>();
    }

    public void Dash(InputAction.CallbackContext context)
    {
        if (context.performed && _canDash && _playerJumpController.IsOnGround())
        {
            StartCoroutine(groundDash());
        } else if (context.performed && _canDash){
            StartCoroutine(airDash());
        }
    }

    private IEnumerator groundDash()
    {
        // Variable setting.

        Physics2D.IgnoreLayerCollision(7, 8, true);
        Physics2D.IgnoreLayerCollision(7, 9, true);
        _canDash = false;
        isDashing = true;
        _tr.emitting = true;
        float dashDirection = _playerMovementController.isFacingRight ? 1f : -1f;

        _rb.velocity = new Vector2(dashDirection * _dashingPower * 0.5f, _rb.velocity.y);
        SoundManager.instance.PlayDashSound();

        yield return new WaitForSeconds(_dashingTime);

        _rb.velocity = new Vector2(0f, _rb.velocity.y);

        isDashing = false;
        _tr.emitting = false;
        Physics2D.IgnoreLayerCollision(7, 8, false);
        Physics2D.IgnoreLayerCollision(7, 9, false);

        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }

    private IEnumerator airDash()
    {
        // Variable setting.

        Physics2D.IgnoreLayerCollision(7, 9, true);
        _canDash = false;
        isDashing = true;
        _tr.emitting = true;
        float dashDirection = _playerMovementController.isFacingRight ? 1f : -1f;

        _rb.velocity = new Vector2(dashDirection * _dashingPower, _rb.velocity.y);
        SoundManager.instance.PlayDashSound();

        yield return new WaitForSeconds(_dashingTime);

        _rb.velocity = new Vector2(0f, _rb.velocity.y);

        isDashing = false;
        _tr.emitting = false;
        Physics2D.IgnoreLayerCollision(7, 9, false);

        yield return new WaitForSeconds(_dashingCooldown);
        _canDash = true;
    }
}
