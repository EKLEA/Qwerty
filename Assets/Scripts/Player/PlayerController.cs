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
    private void OnEnable()
    {
        playerHealthController.OnEnergyChangedCallBack += UpdateStats;
        playerHealthController.OnHealthChangedCallBack += UpdateStats;
        
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

        if (playerStateList.alive)
        {
            moveHandler.Flip();
            if (Input.GetButton("Cast/Heal"))
                castOrHealTimer += Time.deltaTime;
            else
                castOrHealTimer = 0;

            moveHandler.Move();
            moveHandler.JumpMoment();
            moveHandler.StartDash();


            attackLogic.Attack();
            attackLogic.CastSpell();
        }
        
        playerHealthController.Heal();


    }
    
    void UpdateStats()
    {
        playerStateList.health = playerHealthController.health;
        playerStateList.energy=playerHealthController.energy;
        
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
}
