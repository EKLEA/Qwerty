using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
   
    



    
    public Animator anim=> GetComponent<Animator>();
    [SerializeField]  PlayerMoveHandler moveHandler=> GetComponent<PlayerMoveHandler>();
    [SerializeField]  PlayerUseMoment useMoment => GetComponent<PlayerUseMoment>();  
    [SerializeField]  PlayerAttackLogic attackLogic=> GetComponent<PlayerAttackLogic>();
    [SerializeField] public PlayerHealthController playerHealthController => GetComponent<PlayerHealthController>(); // тут мб исправить я хз
    [SerializeField]  public PlayerStateList playerStateList =>GetComponent<PlayerStateList>();
    public BoxCollider pCollider=> gameObject.GetComponent<BoxCollider>();
    public Rigidbody rb => gameObject.GetComponent<Rigidbody>();
    [HideInInspector] public float castOrHealTimer;
     Vector2 Axis;
    private void OnEnable()
    {
        playerHealthController.OnEnergyChangedCallBack += UpdateStats;
        playerHealthController.OnHealthChangedCallBack += UpdateStats;
        
    }

    float xAxis, yAxis;
    private void FixedUpdate()
    {
        if (playerStateList.dashing) return;
        attackLogic.Recoil();
    }

    void Update()
    {

        xAxis = Input.GetAxisRaw("Horizontal");
         yAxis = Input.GetAxisRaw("Vertical");
        Axis = new Vector2(xAxis, yAxis);
        playerStateList.Axis = Axis;
        moveHandler.UpdateJumpVar();
        playerHealthController.RestoreTimeScale();
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
        if (Input.GetButton("Cast/Heal"))
            castOrHealTimer += Time.deltaTime;
        else
            castOrHealTimer = 0;
        moveHandler.Move();
        moveHandler.JumpMoment();
        moveHandler.StartDash();

        
        attackLogic.Attack();
        attackLogic.CastSpell();
        
        playerHealthController.Heal();


    }
    
    void UpdateStats()
    {
        playerStateList.health = playerHealthController.health;
        playerStateList.energy=playerHealthController.energy;
        
    }
}
