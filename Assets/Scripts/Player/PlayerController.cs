using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

   
    public float playerSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    [SerializeField] private int jumpBufferFrames;
    [SerializeField] private float coyoteTime;
    [SerializeField] private int maxAirJump;
    [SerializeField] private float dashSpeed;
    [SerializeField] private float dashTime;
    [SerializeField] private float dashCooldown;
  


    public Animator anim=> GetComponent<Animator>();
    [SerializeField] private PlayerMoveHandler moveHandler=> GetComponent<PlayerMoveHandler>();
    [SerializeField] private PlayerAttackLogic attackLogic=> GetComponent<PlayerAttackLogic>();
    [SerializeField] public PlayerStateList playerStateList =>GetComponent<PlayerStateList>();
    public Rigidbody rb=> GetComponent <Rigidbody>();
    public BoxCollider c=> gameObject.GetComponent<BoxCollider>();

    private bool attack;
    public Vector2 Axis;


    float xAxis, yAxis;


    private void Awake()
    {
        moveHandler.SetMoveValues(playerSpeed, jumpHeight);
        moveHandler.SetValues(jumpBufferFrames,coyoteTime,maxAirJump,dashSpeed,dashTime,dashCooldown);
    }
    
    void Update()
    {

        var xAxis = Input.GetAxisRaw("Horizontal");
        var yAxis = Input.GetAxisRaw("Vertical");
        attack = Input.GetButtonDown("Attack");
        Axis = new Vector2(xAxis, yAxis);
        moveHandler.UpdateJumpVar();
       if (playerStateList.dashing) return;

        if (xAxis < 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
            playerStateList.lookRight = false;
        }
        if (xAxis > 0)
        {
            gameObject.transform.eulerAngles = new Vector3(0, 90, 0);
            playerStateList.lookRight = true;
        }

        moveHandler.Move(xAxis);
        moveHandler.JumpMoment();
        moveHandler.StartDash();
        
        attackLogic.Attack(attack);
        attackLogic.Recoil(yAxis);
        if (moveHandler.Grounded()==false)
        {
            anim.SetBool("Falling", true);
            c.size = new Vector3(1.5f, 4, 1.5f);
        }
        if(moveHandler.Grounded() == true)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("FallingDown", true);
            c.size = new Vector3(1.5f, 6, 1.5f);
        }


    }



}
