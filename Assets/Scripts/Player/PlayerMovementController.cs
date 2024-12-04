using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovementController : MonoBehaviour
{
    [SerializeField]                                // Allows the variable to be edited inside unity.
    private float _movementSpeed = 5;               // This sets the movement speed of the player.

    private Rigidbody2D _rb;
    private float leftRight;
    private bool isFacingRight = true;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();          // This retrieves the rigidbody of the game object.
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        /* 
        This retrieves the default horizontal input for keyboard and gamepad. 
        The left and right keys/direction (on a analog stick for a controller)
        or the "A" and "D keys.
            If nothing is being pressed, the float value will be 0
            if pressing left then the value would be -1
            if pressing right then the value would be 1
        */
        leftRight = Input.GetAxis("Horizontal");

        // This specifies how the player character interact with the unity system.
        _rb.velocity = new Vector2(leftRight * _movementSpeed, _rb.velocity.y);

        Flip();
    }

    private void Flip()
    {
        if (isFacingRight && leftRight < 0f || !isFacingRight && leftRight > 0f)
        {
            isFacingRight = !isFacingRight;
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
