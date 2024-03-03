using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class PlayerMoveHandler : MoveHandler
{


    
    
    private int airJumpCount=0;
    private int jumpBufferCount=0;
    [SerializeField] private int jumpBufferFrames;
    [SerializeField] private int maxAirJump;

    [Space(5)]

    [Header("Dash Settings")]
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
    bool canDash=true;
    bool dashed;

    [Space(5)]

    [Header("Coyote Settings")]
    [SerializeField] private float coyoteTime;
    private float coyoteTimeCounter = 0;

    [Space(5)]

    [Header("Ground Chek Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    



    private PlayerController pController=> GetComponent<PlayerController>();
    private Animator anim => pController.anim;
    private PlayerStateList pState => pController.playerStateList;
    public new void Move()
    {
        
        rb.velocity= new Vector2(pController.playerStateList.Axis.x*  speed, rb.velocity.y);

        anim.SetBool("Runing", rb.velocity.x != 0 && Grounded());
        if (pState.recoilY)
            airJumpCount = 0;

    }
    public void StartDash()
    {
        if (Input.GetButtonDown("Dash") && canDash && !dashed)
        {
            StartCoroutine(Dash());
            dashed = true;
        }

        if (Grounded())
        {
            dashed = false;
        }
    }

    IEnumerator Dash()
    {
        canDash = false;
        pState.dashing = true;
        // animatiom
        rb.useGravity = false;
        rb.velocity = new Vector3(transform.forward.x * dashSpeed, 0,0);
        yield return new WaitForSeconds(dashTime);
        rb.useGravity= true;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public new void JumpMoment()
    {
        if (!pState.jumping)
        {
            if (jumpBufferCount>0 && coyoteTimeCounter>0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                pState.jumping = true;
            }
            else if(!Grounded() && airJumpCount<maxAirJump && Input.GetButtonUp("Jump"))
            {
                pState.jumping = true;
                airJumpCount++;
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            }
            
        }
        if (Input.GetButtonUp("Jump") && rb.velocity.y > 0)
        {
            rb.velocity = new Vector2(rb.velocity.x, 0);
            pState.jumping = false;
        }

        anim.SetBool("Jumping", pState.jumping);
        if (Grounded() == false)
        {
            anim.SetBool("Falling", true);
            pController. pCollider.size = new Vector3(1.5f, 4, 1.5f);
        }
        if (Grounded() == true)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("FallingDown", true);
            pController.pCollider.size = new Vector3(1.5f, 6, 1.5f);
        }


    }
    public new void UpdateJumpVar()
    {
        if (Grounded())
        {
            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCount = 0;
        }
        else
        {
            coyoteTimeCounter -= Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
            jumpBufferCount = jumpBufferFrames;
        else
            jumpBufferCount--;
    }
   
    public new bool Grounded()
    {
        bool b = (Physics.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX / 2, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX / 2, 0, 0), Vector2.down, groundCheckY, whatIsGround));
        pState.grounded = b;
        return b;
           
    }
   
}
