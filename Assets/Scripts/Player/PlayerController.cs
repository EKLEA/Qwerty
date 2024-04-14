using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public Animator anim=> GetComponent<Animator>();
    public PlayerMoveHandler moveHandler=> GetComponent<PlayerMoveHandler>();
    PlayerAttackLogic attackLogic=> GetComponent<PlayerAttackLogic>();
    public PlayerHealthController playerHealthController => GetComponent<PlayerHealthController>();
    public PlayerStateList playerStateList =>GetComponent<PlayerStateList>();
    public PlayerLevelList playerLevelList =>GetComponent<PlayerLevelList>();
    public BoxCollider pCollider=> gameObject.GetComponent<BoxCollider>();
    public Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    public static PlayerController Instance;
    [HideInInspector] public float castOrHealTimer;
     Vector2 Axis;
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }
    

    float xAxis, yAxis;
    private void FixedUpdate()
    {
        if (playerStateList.cutscene) return;
        if (playerStateList.dashing) return;
        attackLogic.Recoil();
    }

    void Update()
    {
        if(playerStateList.cutscene) return;
        if (playerStateList.alive)
        {
            xAxis = Input.GetAxisRaw("Horizontal");
            yAxis = Input.GetAxisRaw("Vertical");
            Axis = new Vector2(xAxis, yAxis);
            playerStateList.Axis = Axis;
        }
        
        moveHandler.UpdateJumpVar();
        playerHealthController.RestoreTimeScale();

        if (playerStateList.dashing) return;

        if (playerStateList.alive && !playerStateList.interactedWithCheckPoint && !playerStateList.invOpened)
        {
            
            if (Input.GetButton("Cast/Heal"))
                castOrHealTimer += Time.deltaTime;
            else
                castOrHealTimer = 0;

            if (!moveHandler.isWallJumping)
            {
                moveHandler.Flip();
                moveHandler.Move();
                moveHandler.JumpMoment();
            }
            moveHandler.WallSlide();
            moveHandler.WallJump();

            moveHandler.StartDash();


            attackLogic.Attack();
            attackLogic.CastSpell();
        }
        if (playerStateList.interactedWithCheckPoint && ((Input.GetButtonDown("Cast/Heal") ||
                                                         Input.GetButtonDown("Horizontal") ||
                                                         Input.GetButtonDown("Vertical") ||
                                                         Input.GetButtonDown("Dash") ||
                                                         Input.GetButtonDown("Attack") )&& UIController.Instance.playerUIInterface.activeInHierarchy == false)) 
            StartCoroutine(ExitFromCheckPoint());
            
       
        playerHealthController.Heal();


    }
    public void Respawned()
    {
        if(!playerStateList.alive)
        {
            playerStateList.alive = true;
            playerHealthController.health = playerHealthController.maxHealth;
            anim.Play("Stading_Idle");
            playerHealthController.isHeartHas = false;
        }
    }
    public  IEnumerator EnterInCheckPoint()
    {
        // анимация входа
        yield return new WaitForSeconds(0.15f);
        PlayerInventory.Instance.blockInv = false;
        rb.velocity = Vector3.zero;
        PlayerInventory.Instance.BlockPlayerInv();
        playerStateList.interactedWithCheckPoint = true;

    }
    public   IEnumerator ExitFromCheckPoint()
    {
        // анимация выхода
        yield return new WaitForSeconds(0.15f);
        PlayerInventory.Instance.blockInv = true;
        PlayerInventory.Instance.BlockPlayerInv();
        playerStateList.interactedWithCheckPoint = false;
    }
}
