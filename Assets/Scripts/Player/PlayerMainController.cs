using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMainController : MonoBehaviour
{
    [Header("Controllers")]

    private PlayerAttackController _playerAttackController;
    private PlayerHealthController _playerHealthController;
    private PlayerDashController _playerDashController;
    private PlayerJumpController _playerJumpController;
    private PlayerMovementController _playerMovementController;
    private PlayerWallJumpSlideController _playerWallJumpSlideController;
    public static PlayerMainController instance;

    [Header("Game Objects")]

    private Rigidbody2D _rb;

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

        _playerAttackController = GetComponent<PlayerAttackController>();
        _playerHealthController = GetComponent<PlayerHealthController>();
        _playerDashController = GetComponent<PlayerDashController>();
        _playerJumpController = GetComponent<PlayerJumpController>();
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerWallJumpSlideController = GetComponent<PlayerWallJumpSlideController>();

        _rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!_playerHealthController.isBeingKnockedBack)
        {
            // Disables other commands when dashing, by exiting the function
            if (_playerDashController.isDashing)
            {
                return;
            }

            // Checks if not wall jumping.
            if (!_playerWallJumpSlideController.isWallJumping)
            {
                // Flips the player character
                _playerMovementController.flip();
                _rb.velocity = new Vector2(_playerMovementController.horizontalMovement * _playerMovementController.movementSpeed, _rb.velocity.y);
            }

            // Changes the fall speed of the character
            _playerJumpController.gravity();

            processGround();
            processWall();
            processWallJump();
        }
    }

    public void processGround()
    {
        // Checks if the character is on the ground.
        if (_playerJumpController.IsOnGround())
        {
            _playerJumpController.coyoteTimeCounter = _playerJumpController.coyoteTime;
            _playerJumpController.currentJumps = _playerJumpController.jumpAmountTotal;
        }
        else
        {
            _playerJumpController.coyoteTimeCounter -= Time.deltaTime;
        }
    }

    public void processWall()
    {
        // Checks if wall sliding / ProcessWall function
        if (!_playerJumpController.IsOnGround() && _playerWallJumpSlideController.IsOnWall() && _playerMovementController.horizontalMovement != 0f)
        {
            _playerWallJumpSlideController.isWallSliding = true;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -_playerWallJumpSlideController.wallSlideSpeed));
        }
        else
        {
            _playerWallJumpSlideController.isWallSliding = false;
        }
    }

    private void processWallJump()
    {
        // Time for wall jumping
        if (_playerWallJumpSlideController.isWallSliding)
        {
            _playerWallJumpSlideController.isWallJumping = false;
            _playerWallJumpSlideController.wallJumpDirection = -transform.localScale.x;
            _playerWallJumpSlideController.wallJumpTimer = _playerWallJumpSlideController.wallJumpTime;

            CancelInvoke(nameof(_playerWallJumpSlideController.CancelWallJump));
        }
        else if (_playerWallJumpSlideController.wallJumpTimer > 0f)
        {
            _playerWallJumpSlideController.wallJumpTimer -= Time.deltaTime;
        }
    }

    public void JumpBuffer(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            _playerJumpController.jumpBufferCounter = _playerJumpController.jumpBufferTime;
        }
        else
        {
            _playerJumpController.jumpBufferCounter -= Time.deltaTime;
        }
    }
}
