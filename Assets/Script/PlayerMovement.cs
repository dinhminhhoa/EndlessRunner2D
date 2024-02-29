using UnityEngine;
using System.Collections;
using System.IO;
using System.Collections.Generic;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private Rigidbody2D rb;
    [SerializeField] private Transform GFX;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private Transform feetPosition;
    [SerializeField] private float groundDistance = 0.25f;
    [SerializeField] private float jumpTime = 0.3f;

    [SerializeField] private float crouchHeight = 0.5f;

    private bool isGrounded = false;
    private bool isJumping = false;
    private float jumpTimer;

    private void Update()
    {
        isGrounded = Physics2D.OverlapCircle(feetPosition.position, groundDistance, groundLayer);

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            isJumping = true;
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }

        if (isJumping && Input.GetKey(KeyCode.Space))
        {
            if (jumpTimer < jumpTime)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpForce);

                jumpTimer += Time.deltaTime;
            }
            else 
            {
                isJumping = false;

            }
        }

        if (Input.GetKeyUp(KeyCode.Space))
        {
            isJumping = false;
            jumpTimer = 0;
        }

        if(isGrounded && Input.GetKeyDown(KeyCode.LeftShift))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, crouchHeight, GFX.localScale.z);

            if(isJumping)
            {
                GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
            }
        }

        if( Input.GetKeyUp(KeyCode.LeftShift))
        {
            GFX.localScale = new Vector3(GFX.localScale.x, 1f, GFX.localScale.z);
        }

    }
}
