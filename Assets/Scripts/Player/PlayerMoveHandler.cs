using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Animations;
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

    [Header("Wall Jump Settings")]
    [SerializeField] float wallSlidingSpeed = 2f;
    [SerializeField] Transform wallCheck;
    [SerializeField] LayerMask wallLayer;
    [SerializeField] float wallJumpingDutarion;
    [SerializeField] Vector2 wallJumpingPower;
    float wallJumpingDirection;
    bool isWallSliding;
    public bool isWallJumping;

    [Space(5)]

    [Header("Ground Chek Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;
    bool landingSoundPlayed;




    private Animator anim => PlayerController.Instance.anim;
    private PlayerStateList pState => PlayerController.Instance.playerStateList;
    public new void Move()
    {
        if ((PlayerController.Instance.playerHealthController.energy > 0 && PlayerController.Instance.playerHealthController.isHeartHas == false) || (PlayerController.Instance.playerHealthController.isHeartHas == true))
        {
            rb.velocity = new Vector2(PlayerController.Instance.playerStateList.Axis.x * speed * PlayerController.Instance.playerLevelList.movekf, rb.velocity.y);

            anim.SetBool("Runing", rb.velocity.x != 0 && Grounded());
            if (Grounded() && rb.velocity.x>0.5f) 
            {
                PlayerController.Instance.audioSource.PlayOneShot(PlayerController.Instance.walk);
            }
            
            if (pState.recoilY)
                airJumpCount = 0;
        }

    }
    public void StartDash()
    {
        if (Input.GetButtonDown("Dash") && canDash && !dashed && PlayerController.Instance.playerLevelList.canDash)
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
        anim.SetTrigger("Dashing");
        PlayerController.Instance.audioSource.PlayOneShot(PlayerController.Instance.dashAndAttackSound);
        rb.useGravity = false;
        int _dir = pState.lookRight ? 1 : -1;
        rb.velocity = new Vector3(_dir * dashSpeed*PlayerController.Instance.playerLevelList.movekf, 0,0);
        if(PlayerController.Instance.playerHealthController.isHeartHas==false)
            PlayerController.Instance.playerHealthController.energy -= 0.1f;
        yield return new WaitForSeconds(dashTime);
        rb.useGravity= true;
        pState.dashing = false;
        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    bool jumpOnce;
    public new void JumpMoment()
    {
        if (jumpBufferCount>0 && coyoteTimeCounter>0 && !pState.jumping)
        {
            
             
            PlayerController.Instance.audioSource.PlayOneShot(PlayerController.Instance.jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            pState.jumping = true;

            if (PlayerController.Instance.playerHealthController.isHeartHas == false)
                PlayerController.Instance.playerHealthController.energy -= 0.1f/ jumpBufferFrames;
        }
        else if (!Grounded() && airJumpCount < maxAirJump && Input.GetButtonDown("Jump") && PlayerController.Instance.playerLevelList.canDoubleWallJump)
        {

            PlayerController.Instance.audioSource.PlayOneShot(PlayerController.Instance.jumpSound);
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
            if(PlayerController.Instance.playerHealthController.isHeartHas == false)
                PlayerController.Instance.playerHealthController.energy -= 0.1f;
            pState.jumping = true;
            airJumpCount++;
            
        }
        else if (Input.GetButtonDown("Jump") && rb.velocity.y > 3)
        {
            
            pState.jumping = false;
            rb.velocity = new Vector2(rb.velocity.x, 0);
        }
        anim.SetBool("Jumping", pState.jumping);
    }
    
    public new void UpdateJumpVar()
    {
        if (Grounded())
        {
            if (!landingSoundPlayed)
            {
                PlayerController.Instance.audioSource.PlayOneShot(PlayerController.Instance.landingSound);
                landingSoundPlayed = true;
            }
            anim.SetBool("Falling", false);
            anim.SetBool("FallingDown", true);
            PlayerController.Instance.pCollider.size = new Vector3(1, 7, 1) ;

            pState.jumping = false;
            coyoteTimeCounter = coyoteTime;
            airJumpCount = 0;
        }
        else
        {
            anim.SetBool("Falling", true);
            PlayerController.Instance.pCollider.size = new Vector3(1, 3.5f, 1);

            coyoteTimeCounter -= Time.deltaTime;

            landingSoundPlayed = false;
        }
        if (Input.GetButtonDown("Jump"))
            jumpBufferCount = jumpBufferFrames;
        else
            jumpBufferCount--;
    }
    private bool Walled()
    {
        var t=Physics.OverlapSphere(wallCheck.position, 0.2f, wallLayer);
        if (t.Length > 0)
            return true;
        else return false;
    }
    public void WallSlide()
    {
        
        if(Walled() && !Grounded() && pState.Axis.x!= 0 && PlayerController.Instance.playerLevelList.canDoubleWallJump)
        {
            isWallSliding = true;
            rb.velocity = new Vector2(rb.velocity.x,Mathf.Clamp( rb.velocity.y, -wallSlidingSpeed,float.MaxValue));
        }
        else
        {
            isWallSliding=false;
        }
    }
    public void WallJump()
    {
        if (isWallSliding)
        {
            isWallJumping = false;
            wallJumpingDirection = !pState.lookRight ? 1 : -1;
            CancelInvoke(nameof(StopWallJumping));
        }
        if(Input.GetButtonDown("Jump") && isWallSliding && PlayerController.Instance.playerLevelList.canDoubleWallJump)
        {
            isWallJumping=true;
            rb.velocity = new Vector2(wallJumpingDirection * wallJumpingPower.x, wallJumpingPower.y);
            dashed = false;
            airJumpCount = 0;
            if ((pState.lookRight && transform.eulerAngles.y==0) || (!pState.lookRight && transform.eulerAngles.y != 0))
            {
                pState.lookRight=!pState.lookRight;
                int _yRotation = pState.lookRight ? 0 : 180;
                transform.eulerAngles=new Vector2(transform.eulerAngles.x,_yRotation);
            }
            Invoke(nameof(StopWallJumping), wallJumpingDutarion);

            if (PlayerController.Instance.playerHealthController.isHeartHas == false)
                PlayerController.Instance.playerHealthController.energy -= 0.1f;
        }
    }
    void StopWallJumping()
    {
        isWallJumping = false;
    }
    public void Flip()
    {
        if (PlayerController.Instance.playerStateList.Axis.x< 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
            PlayerController.Instance.playerStateList.lookRight = false;
        }
        if (PlayerController.Instance.playerStateList.Axis.x > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            PlayerController.Instance.playerStateList.lookRight = true;
        }
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
    public IEnumerator WalkIntoNewScene(Vector2 _exitDir, float _delay)
    {
        if (_exitDir.y > 0)
            rb.velocity = jumpHeight * -_exitDir;
        if (_exitDir.x  != 0)
        {
            PlayerController.Instance.playerStateList.Axis.x = _exitDir.x>0? 1 : -1;
            Move();
        }    
        Flip();
        yield return new WaitForSeconds(_delay);
        PlayerController.Instance.playerStateList.cutscene = false;
        

    }

}
