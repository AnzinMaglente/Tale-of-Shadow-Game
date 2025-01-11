using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerWallJumpSlideController : MonoBehaviour
{
    [Header("Controllers")]

    public PlayerMovementController playerMovementController;
    public PlayerJumpController playerJumpController;

    [Header("Game Objects")]

    public Rigidbody2D rb;
    public CapsuleCollider2D col;

    [Header("Wall Detection Settings")]

    [SerializeField]
    public float distanceDelta;
    [SerializeField]
    public LayerMask wallMask;

    [Header("Wall Slide Settings")]

    [SerializeField]
    public float wallSlideSpeed = 2;
    public bool isWallSliding;


    [Header("Wall Jump Settings")]

    public bool isWallJumping;
    public float wallJumpDirection;
    [SerializeField]
    public float wallJumpTime = 0.5f;
    [SerializeField]
    public float wallJumpTimer;
    [SerializeField]
    private Vector2 _wallJumpForce = new Vector2(5f,10f);

    private void Awake()
    {
        playerMovementController = GetComponent<PlayerMovementController>();
        playerJumpController = GetComponent<PlayerJumpController>();

        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<CapsuleCollider2D>();
    }

    public void WallJump(InputAction.CallbackContext context)
    {
        if (context.performed && wallJumpTimer > 0f && IsOnWall())
        {
            SoundManager.instance.PlayJumpSound();
            isWallJumping = true;
            // Jump away from wall.
            rb.velocity = new Vector2(wallJumpDirection * _wallJumpForce.x, _wallJumpForce.y);
            wallJumpTimer = 0;
            
            playerMovementController.isFacingRight = !playerMovementController.isFacingRight;
            
            // This transforms the character's width backwards so that it appears like they have flipped.
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;

            Invoke(nameof(CancelWallJump), wallJumpTime + 0.1f); // Wall jump will only last 0.5 frames and will allow for a new jump in the 0.6th frame.
        }
    }

    public void CancelWallJump()
    {
        isWallJumping = false;
    }

    public bool IsOnWall()
    {
        float distanceX = col.bounds.extents.x + distanceDelta;
        if (playerMovementController.isFacingRight)
        {
            Debug.DrawRay(transform.position, Vector2.right * distanceX, Color.green, 1);

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.right, distanceX, wallMask);
            return raycastHit.collider != null;
        }
        else
        {
            
            Debug.DrawRay(transform.position, Vector2.left * distanceX, Color.green, 1);

            RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.left, distanceX, wallMask);
            return raycastHit.collider != null;
        }
    }
}