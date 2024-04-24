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
    public CapsuleCollider pCollider=> gameObject.GetComponent<CapsuleCollider>();
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
    private void Start()
    {
        PlayerInventory.Instance.InitInv();
        SaveData.Instance.LoadPlayerInv();
        PlayerInventory.Instance.SetupPlayerVariables();
        SaveData.Instance.LoadPlayerLevelListData();
        SaveData.Instance.LoadPlayerData();
       
        playerHealthController.InitPlayerHealth();

       
        UIController.Instance.InitUIController();
        UIController.Instance.uiHud.GetComponent<UIHud>().InitHud();


    }

    float xAxis, yAxis;
    bool openMap;
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
            openMap = Input.GetButton("Map");
            Axis = new Vector2(xAxis, yAxis);
            playerStateList.Axis = Axis;
        }
        
       
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
                moveHandler.UpdateJumpVar();
            }
            moveHandler.WallSlide();
            moveHandler.WallJump();

            moveHandler.StartDash();


            attackLogic.Attack();
            attackLogic.CastSpell();
            ToggleMap();
            
        }
        if (playerStateList.interactedWithCheckPoint && ((Input.GetButtonDown("Cast/Heal") ||
                                                         Input.GetButtonDown("Horizontal") ||
                                                         Input.GetButtonDown("Vertical") ||
                                                         Input.GetButtonDown("Dash") ||
                                                         Input.GetButtonDown("Attack") )&& UIController.Instance.playerUIInterface.activeInHierarchy == false)) 
            StartCoroutine(ExitFromCheckPoint());
            
       
        playerHealthController.Heal();
        playerHealthController.MoveWithoutHeart();


    }
    void ToggleMap()
    {
        UIController.Instance.mapHandler.SetActive(openMap);
    }
    public void Respawned()
    {
        // запуск коротины ыхода их принтора
        if(!playerStateList.alive)
        {
            playerStateList.alive = true;
            playerHealthController.health = playerHealthController.resHealth;
            playerHealthController.energy = playerHealthController.resEnergy;
            anim.Play("Stading_Idle");
            playerHealthController.isHeartHas= false;
            UIController.Instance.uiHud.GetComponent<UIHud>().UpdateHeart();
            playerStateList.respawning = false;
        }
    }
    public  IEnumerator EnterInCheckPoint()
    {
        // анимация входа
        yield return new WaitForSeconds(0.15f);
        rb.velocity = Vector3.zero;
        playerStateList.interactedWithCheckPoint = true;

    }
    public   IEnumerator ExitFromCheckPoint()
    {
        // анимация выхода
        
        yield return new WaitForSeconds(0.15f);
        playerStateList.interactedWithCheckPoint = false;
    }
    public IEnumerator TakeShard(bool b)
    {

        yield return new WaitForSeconds(0.15f);
    }
}
