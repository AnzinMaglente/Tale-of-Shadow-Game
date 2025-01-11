using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJumpController : MonoBehaviour
{
    [Header("Controllers")]
    private PlayerWallJumpSlideController _playerWallJumpSlideController;

    [Header("Game Objects")]
    private Rigidbody2D _rb;
    private CapsuleCollider2D col;

    [Header("Jump Settings")]
    [SerializeField]                                    // Allows the variable to be edited inside unity.
    private float _jumpForce = 8f;                     // This sets the jump height of the player.
    [SerializeField]
    public float distanceDelta;                         // This variable is used to ensure that raycast detects the ground mask.
    [SerializeField]
    public LayerMask groundMask;                        // This specifies the layer mask of the ground.

    [Header("Hold Jump Settings")]
    [SerializeField]
    private float _jumpStartTime;
    private float _jumpTime;
    public bool holdJumping = false;

    [Header("double Jump Settings")]
    [SerializeField]
    public float jumpAmountTotal = 2;
    public float currentJumps;

    [Header("Coyote Time Settings")]
    [SerializeField]
    public float coyoteTime = 0.2f;
    public float coyoteTimeCounter;

    [Header("Jump Buffer Settings")]
    [SerializeField]
    public float jumpBufferTime = 0.2f;
    public float jumpBufferCounter;

    [Header("Gravity Settings")]
    public float baseGravity = 2f;
    public float maxFallSpeed = 18f;
    public float fallSpeedModifier = 2f;

    private void Awake()
    {
        _playerWallJumpSlideController = GetComponent<PlayerWallJumpSlideController>();

        _rb = GetComponent<Rigidbody2D>();              // This retrieves the rigidbody of the game object.
        col = GetComponent<CapsuleCollider2D>();
    }

    public void Jump(InputAction.CallbackContext context)
    {
        // This checks if the current keycode is the spacebar key, and if the player is on the ground.
        if (jumpBufferCounter > 0f && coyoteTimeCounter > 0f)
        {
            holdJumping = true;
            _jumpTime = _jumpStartTime;

            // This adds a vertical force that propels the player upwards.
            _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

            jumpBufferCounter = 0f;
        }

        // This checks if the jump button is being held while being in air.
        if (context.performed && holdJumping == true && currentJumps > 0)
        {
            SoundManager.instance.PlayJumpSound();
            currentJumps--;
            if (_jumpTime > 0)
            {
                // This increase the jump force the longer the button is held.
                _rb.velocity = new Vector2(_rb.velocity.x, _jumpForce);

                // Reduces the jump time.
                _jumpTime -= Time.deltaTime;
            }
            // This sets isJumping to false when the button is released. 
            else
            {
                holdJumping = false;
            }
        }
        else if (context.canceled && currentJumps > 0)
        {
            currentJumps--;
            _rb.velocity = new Vector2(_rb.velocity.x, _rb.velocity.y * 0.5f);
        }
    }

    public bool IsOnGround()
    {
        /* distance is used to specify how close the ground is to the game object by checking y axis' distance
        to another collider. */
        float distanceY = col.bounds.extents.y + distanceDelta;

        // This is used for debugging purposes, to see where the distance float will end
        Debug.DrawRay(transform.position, Vector2.down * distanceY, Color.green, 1);

        // Raycast is used to detect the ground using a layer mask and the distance float.
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, distanceY, groundMask);

        /* This returns a value to the function. Which will be a true or false statement based on whether or not
        raycastHit is detecting a collision. */
        return raycastHit.collider != null;
    }

    public void gravity()
    {
        if (_rb.velocity.y < 0f)
        {
            _rb.gravityScale = baseGravity * fallSpeedModifier;
            _rb.velocity = new Vector2(_rb.velocity.x, Mathf.Max(_rb.velocity.y, -maxFallSpeed));
        }
        else
        {
            _rb.gravityScale = baseGravity;
        }
    }
}
