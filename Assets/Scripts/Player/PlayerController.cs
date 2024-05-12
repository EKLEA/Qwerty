using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{

    [HideInInspector] public Animator anim;
    [HideInInspector] public PlayerMoveHandler moveHandler;
    PlayerAttackLogic attackLogic;
    [HideInInspector] public PlayerHealthController playerHealthController;
    [HideInInspector] public PlayerStateList playerStateList;
    [HideInInspector] public PlayerLevelList playerLevelList;
    [HideInInspector] public BoxCollider pCollider;
    [HideInInspector] public Rigidbody rb;
    public static PlayerController Instance;
    [HideInInspector] public float castOrHealTimer;
     Vector2 Axis;

    [Header("Audio")]
    
    public AudioClip landingSound;
    public AudioClip jumpSound;
    public AudioClip dashAndAttackSound;
    public AudioClip spellCastSound;
    public AudioClip hurtSound;
    public AudioSource audioSource;
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

        anim = GetComponent<Animator>();
        moveHandler = GetComponent<PlayerMoveHandler>();
        attackLogic = GetComponent<PlayerAttackLogic>();
        playerHealthController = GetComponent<PlayerHealthController>();
        playerStateList = GetComponent<PlayerStateList>();
        playerLevelList = GetComponent<PlayerLevelList>();
        pCollider = gameObject.GetComponent<BoxCollider>();
        rb = gameObject.GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }
    private void Start()
    {


        PlayerInventory.Instance.InitInv();
       
        SaveData.Instance.LoadPlayerInv();
        SaveData.Instance.LoadPlayerLevelListData();
       

        SaveData.Instance.LoadPlayerData();
        PlayerInventory.Instance.SetupPlayerVariables();


        PlayerHealthController.Instance.InitPlayerHealth();
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
        if (GameManager.Instance.gameIsPaused) return;
        if (playerStateList.cutscene) return;
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
            UIController.Instance.uiHud.GetComponent<UIHud>().UpdateHeart();
            playerStateList.respawning = false;
        }
    }
    public  IEnumerator EnterInCheckPoint()
    {
        // анимация входа
        playerHealthController.health=playerHealthController.resHealth;
        playerHealthController.energy=playerHealthController.resEnergy;
        UIController.Instance.uiHud.GetComponent<UIHud>().InitHud();
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
