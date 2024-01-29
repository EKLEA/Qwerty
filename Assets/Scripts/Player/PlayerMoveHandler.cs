using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveHandler : MonoBehaviour, IMoveHandler
{
    public float speed { get; private set; }
    public float jumpHeight { get; private set; }

    private int jumpBC = 0;
    private int jumpBF;
    private float coyoteTC = 0;
    private float coyoteT;
    private int airJC;
    private int maxAJ;

    bool canDash=true;
    bool dashed;
    private float dashSpeed;
    private float dashTime;
    private float dashCooldown;
    [SerializeField] public Transform groundCheckPoint;
    [SerializeField] public float groundCheckY = 0.2f;
    [SerializeField] public float groundCheckX = 0.5f;
    [SerializeField] public LayerMask whatIsGround;
   


    public Rigidbody rb => pController.rb;
    private PlayerController pController=> GetComponent<PlayerController>();
    private Animator anim => pController.anim;
    private PlayerStateList pState => pController.playerStateList;
    public void Move(float xAxis)
    {
        rb.velocity= new Vector2(xAxis*  speed, rb.velocity.y);

        anim.SetBool("Runing", rb.velocity.x != 0 && Grounded());
        if (pState.recoilY)
            airJC = 0;

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
    public void JumpMoment()
    {
        if (!pState.jumping)
        {
            if (jumpBC>0 && coyoteTC>0)
            {
                rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
                pState.jumping = true;
            }
            else if(!Grounded() && airJC<maxAJ && Input.GetButtonUp("Jump"))
            {
                pState.jumping = true;
                airJC++;
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
            pController. c.size = new Vector3(1.5f, 4, 1.5f);
        }
        if (Grounded() == true)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("FallingDown", true);
            pController.c.size = new Vector3(1.5f, 6, 1.5f);
        }


    }
    public void UpdateJumpVar()
    {
        if (Grounded())
        {
            pState.jumping = false;
            coyoteTC = coyoteT;
            airJC = 0;
        }
        else
        {
            coyoteTC-=Time.deltaTime;
        }
        if (Input.GetButtonDown("Jump"))
            jumpBC = jumpBF;
        else
            jumpBC--;
    }
    public void SetMoveValues(float _speed, float _jumpHeight)
    {
        speed = _speed;
        jumpHeight = _jumpHeight;
    }
    public void SetValues(
        int jumpBufferFrames,
        float coyoteTime,
        int MaxAirJump,
        float dashS,
        float dashT,
        float dashCD)
    {
        jumpBF = jumpBufferFrames;
        coyoteT = coyoteTime;
        maxAJ = MaxAirJump;
        dashSpeed= dashS;
        dashTime = dashT;
        dashCooldown= dashCD;

    }
    public bool Grounded()
    {
        bool b = (Physics.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX / 2, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX / 2, 0, 0), Vector2.down, groundCheckY, whatIsGround));
        pState.grounded = b;
        return b;
           
    }
   
}
