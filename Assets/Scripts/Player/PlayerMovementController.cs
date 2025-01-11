using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovementController : MonoBehaviour
{
    [Header("Movement Settings")]

    [SerializeField]                                // Allows the variable to be edited inside unity.
    public float movementSpeed = 5;                 // This sets the movement speed of the player.
    public float horizontalMovement;
    public bool isFacingRight = true;
    
    // InputAction.Callback retrieves an input from the new unityinput system. //
    public void Move(InputAction.CallbackContext context)
    {
        /*
        The left and right keys/direction (on a analog stick for a controller)
        or the "A" and "D" / left and right keys.
            If nothing is being pressed, the float value will be 0
            if pressing left then the value would be -1
            if pressing right then the value would be 1
        */
        horizontalMovement = context.ReadValue<Vector2>().x;
    }

    // This function flips the character sprite whenever they changes directions.
    public void flip()
    {   
        // Checks if the player is moving left or right.
        if (isFacingRight && horizontalMovement < 0f || !isFacingRight && horizontalMovement > 0f)
        {
            isFacingRight = !isFacingRight;

            // This transforms the character's width backwards so that it appears like they have flipped.
            Vector3 localScale = transform.localScale;
            localScale.x *= -1f;
            transform.localScale = localScale;
        }
    }
}
