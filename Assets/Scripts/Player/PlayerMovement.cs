using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public PlayerControls controls; // Public biar bisa diakses dari luar kalau perlu

    float direction = 0;

    public float speed = 400;
    bool isFacingRight = true;
    public float jumpForce = 5;
    bool isGrounded;
    int numberOfJumps = 0;
    public int maxJumps = 3;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public Rigidbody2D playerRB;
    public Animator animator;
    private bool isWalkingSoundPlaying = false;

    public bool isFrozen = false;

    private void Awake()
    {
        isFrozen = false;

        controls = new PlayerControls();

        controls.Land.Move.performed += ctx =>
        {
            if (!isFrozen)
                direction = ctx.ReadValue<float>();
        };

        controls.Land.Move.canceled += ctx =>
        {
            direction = 0f;
        };

        controls.Land.Jump.performed += OnJumpPerformed;

        controls.Enable();
    }

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext ctx)
    {
        if (isFrozen || playerRB == null) return;
        Jump();
    }

    private void OnDisable()
    {
        if (controls != null)
        {
            controls.Land.Jump.performed -= OnJumpPerformed;
            controls.Disable();
        }
    }

    private void FixedUpdate()
    {
        if (playerRB == null) return;

        if (isFrozen)
        {
            playerRB.velocity = Vector2.zero;
            animator.SetFloat("speed", 0);

            if (isWalkingSoundPlaying)
            {
                AudioManager.instance.Stop("Steps");
                isWalkingSoundPlaying = false;
            }

            return;
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, 0.1f, groundLayer);
        animator.SetBool("isGrounded", isGrounded);

        playerRB.velocity = new Vector2(direction * speed * Time.fixedDeltaTime, playerRB.velocity.y);
        animator.SetFloat("speed", Mathf.Abs(direction));

        if (isFacingRight && direction < 0 || !isFacingRight && direction > 0)
            Flip();

        if (Mathf.Abs(direction) > 0.1f && isGrounded)
        {
            if (!isWalkingSoundPlaying)
            {
                AudioManager.instance.Play("Steps");
                isWalkingSoundPlaying = true;
            }
        }
        else
        {
            if (isWalkingSoundPlaying)
            {
                AudioManager.instance.Stop("Steps");
                isWalkingSoundPlaying = false;
            }
        }
    }

    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector2(transform.localScale.x * -1, transform.localScale.y);
    }

    void Jump()
    {
        if (playerRB == null) return;

        if (isGrounded)
        {
            numberOfJumps = 0;
        }

        if (numberOfJumps < maxJumps)
        {
            playerRB.velocity = new Vector2(playerRB.velocity.x, jumpForce);

            if (numberOfJumps == 0)
            {
                AudioManager.instance.Play("FirstJump");
            }
            else
            {
                AudioManager.instance.Play("SecondJump");
            }

            numberOfJumps++;
        }
    }

    public void Unfreeze()
    {
        isFrozen = false;
        direction = 0f;

        if (controls != null)
        {
            controls.Disable();
            controls.Land.Jump.performed -= OnJumpPerformed;

            controls.Enable();
            controls.Land.Jump.performed += OnJumpPerformed;
        }
    }
}
