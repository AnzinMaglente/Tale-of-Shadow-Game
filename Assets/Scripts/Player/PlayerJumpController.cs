using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpController : MonoBehaviour
{
    private Rigidbody2D _rb;
    private CapsuleCollider2D _collider;

    [SerializeField]                                    // Allows the variable to be edited inside unity.
    private float _jumpHeight = 6;                      // This sets the jump height of the player.
    [SerializeField]
    private float _distanceDelta;                       // This variable is used to ensure that raycast detects the ground mask.
    [SerializeField]
    private bool _isOnGround;
    [SerializeField]
    private LayerMask _groundMask;                      // This specifies the layer mask of the ground.

    [SerializeField]
    private float _coyoteTime = 0.2f;
    private float _coyoteTimeCounter;

    [SerializeField]
    private float _jumpBufferTime = 0.2f;
    private float _jumpBufferCounter;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();              // This retrieves the rigidbody of the game object.
        _collider = GetComponent<CapsuleCollider2D>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (IsOnGround())
        {
            _coyoteTimeCounter = _coyoteTime;
        }
        else
        {
            _coyoteTimeCounter -= Time.deltaTime;
        }

        if (Input.GetButtonDown("Jump"))
        {
            _jumpBufferCounter = _jumpBufferTime;
        }
        else
        {
            _jumpBufferCounter -= Time.deltaTime;
        }

        // This checks if the current keycode is the spacebar key, and if the player is on the ground.
        if (_jumpBufferCounter > 0f && _coyoteTimeCounter > 0f)
        {
            // This adds a vertical force that propels the player upwards
            _rb.AddForce(Vector2.up * _jumpHeight, ForceMode2D.Impulse);

            _jumpBufferCounter = 0f;
        }
    }

    bool IsOnGround()
    {
        /* distance is used to specify how close the ground is to the game object by checking y axis' distance
        to another collider. */
        float distanceY = _collider.bounds.extents.y + _distanceDelta;

        // This is used for debugging purposes, to see where the distance float will end
        Debug.DrawRay(transform.position, Vector2.down * distanceY, Color.green, 1);

        // Raycast is used to detect the ground using a layer mask and the distance float.
        RaycastHit2D raycastHit = Physics2D.Raycast(transform.position, Vector2.down, distanceY, _groundMask);

        /* This returns a value to the function. Which will be a true or false statement based on whether or not
        raycastHit is detecting a collision. */
        return raycastHit.collider != null;
    }
}
