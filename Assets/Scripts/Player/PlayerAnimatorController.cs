using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerAnimatorController : MonoBehaviour
{
    private PlayerMovementController _playerMovementController;
    private PlayerJumpController _playerJumpController;
    private PlayerAttackController _playerAttackController;
    private PlayerWallJumpSlideController _playerWallJumpSlideController;

    private Rigidbody2D _rb;
    public Animator anim;

    private void Awake()
    {
        _playerMovementController = GetComponent<PlayerMovementController>();
        _playerJumpController = GetComponent<PlayerJumpController>();
        _playerAttackController = GetComponent<PlayerAttackController>();
        _playerWallJumpSlideController = GetComponent<PlayerWallJumpSlideController>();

        _rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        anim.SetFloat("xVelocity", Mathf.Abs(_playerMovementController.horizontalMovement));
        anim.SetFloat("yVelocity", _rb.velocity.y);

        if (_playerWallJumpSlideController.isWallSliding)
        {
            anim.SetBool("onWall", true);
            anim.SetBool("onJump", false);
        }
        else
        {
            anim.SetBool("onWall", false);
            anim.SetBool("onJump", true);
        }

        if (_playerJumpController.IsOnGround())
        {
            anim.SetBool("onJump", false);
        }
    }

    public void JumpAnim(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            anim.SetBool("onJump", true);
        }
    }
}
