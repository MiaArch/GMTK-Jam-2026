using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    [Header("Movement")]
    public float moveSpeed = 4f;

    public float jumpForce = 10f;
    public float acceleration = 15f;
    public float deceleration = 20f;
    public float playerGravity = 9.6f;

    private float defaultMoveSpeed;
    private Rigidbody2D rb;
    // private Animator animator;
    private Vector2 moveInput;
    private Vector2 currentVelocity;
    private bool isJumping;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        moveInput = context.ReadValue<Vector2>();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        isJumping = context.ReadValueAsButton();
    }

    public void FixedUpdate()
    {
        float targetVelocityInputX = moveInput.x * moveSpeed;
        float targetVelocityInputY = -playerGravity;
        if (isJumping)
        {
            targetVelocityInputY += jumpForce;
        }
        
        Vector2 targetVelocity = new Vector2(targetVelocityInputX, targetVelocityInputY);
        float accelRate = moveInput.magnitude > 0
            ? acceleration
            : deceleration;
        
        currentVelocity = Vector2.Lerp(
            currentVelocity,
            targetVelocity,
            accelRate * Time.fixedDeltaTime
        );

        rb.linearVelocity = currentVelocity;
    }
}