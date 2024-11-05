using UnityEngine;
using System.Collections;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Rigidbody2D rb;
    public Animator animator;
    private bool isFacingLeft = false;
    private bool isOnJumpCooldown = false;
    public float player1jumpcooldown = .75f;
    
    

    void Update()
    {
        
        if(Input.GetKeyDown(KeyCode.W) && !isOnJumpCooldown)
        {
            rb.AddForce(Vector2.up * 700);
            animator.SetTrigger("IsJumping");
            
            StartCoroutine(JumpCooldown());
        }
        
    }
    
   
    
    //edit this to change jump cooldown
    IEnumerator JumpCooldown()
    {
        
        isOnJumpCooldown = true;
        yield return new WaitForSeconds(player1jumpcooldown);
        isOnJumpCooldown = false;
       // yield return new WaitForSeconds(1.7f);
        animator.ResetTrigger("IsJumping");
        
    }
    
    
    
    
    
    void FixedUpdate()
    {
            float moveInput = 0f;

    // Check for 'A' key press for left movement
    if (Input.GetKey(KeyCode.A))
    {
        moveInput = -1f;
    }
    // Check for 'D' key press for right movement
    else if (Input.GetKey(KeyCode.D))
    {
        moveInput = 1f;
    }
        rb.velocity = new Vector2(moveInput * moveSpeed, rb.velocity.y);
        //rb.AddForce(new Vector2(moveInput * moveSpeed, 0f));
        // Flip the sprite if moving left
        if (moveInput < 0)
        {
            if (!isFacingLeft)
            {
                Flip();
            }
        }
        // Flip the sprite if moving right
        else if (moveInput > 0)
        {
            if (isFacingLeft)
            {
                Flip();
            }
        }

        // Set the "IsMoving" parameter for animation
        animator.SetBool("IsMoving", Mathf.Abs(moveInput) > 0);
    }

    void Flip()
    {
        isFacingLeft = !isFacingLeft;
        transform.Rotate(0f, 180f, 0f);
    }
}