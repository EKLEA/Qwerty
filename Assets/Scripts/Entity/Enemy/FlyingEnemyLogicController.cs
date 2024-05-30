using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingEnemyLogicController : EnemyLogicBase
{
    [SerializeField] float chaseDistance;
    [SerializeField] float stunDruration;
    private Vector3 targetPosition;
    public AudioClip flyingSound;
    protected new void Start()
    {
        base.Start();
        ChangeState(EnemyStates.FlyingEn_Idle);
        rb.useGravity = false;
        GetComponent<EnemyHealthController>().OnHealthChangedCallBack += CheckDeath;
    }
    public override  void UpdateEnemyStates()
    {
        float dist = Vector2.Distance(transform.position, PlayerController.Instance.transform.position);

        switch(GetCurrectEnemyState)
        {
            case EnemyStates.FlyingEn_Idle:

                if (dist < chaseDistance)
                    ChangeState(EnemyStates.FlyingEn_Chase);
                break;
            case EnemyStates.FlyingEn_Chase:
                targetPosition = new Vector3(PlayerController.Instance.transform.position.x, PlayerController.Instance.transform.position.y+3, PlayerController.Instance.transform.position.z);
                rb.MovePosition(Vector3.MoveTowards(transform.position, targetPosition, Time.deltaTime * speed));
                transform.eulerAngles = new Vector3(-90, 90, 0);
                break;
            case EnemyStates.FlyingEn_Stunned:

                timer += Time.deltaTime;
                if(timer > stunDruration)
                {
                    ChangeState(EnemyStates.FlyingEn_Idle);
                    timer = 0;
                }
                break;
            case EnemyStates.FlyingEn_Death:


                break;
        }
    }
    protected override void Update()
    {
        if (!GetComponent<AudioSource>().isPlaying)
        {
            GetComponent<AudioSource>().PlayOneShot(flyingSound);
        }
        base.Update();
        if (!PlayerController.Instance.playerStateList.alive)
            ChangeState(EnemyStates.FlyingEn_Idle);
    }
    void CheckDeath()
    {
        if (enemyHealth.health > 0)
            ChangeState(EnemyStates.FlyingEn_Stunned);
        else
            ChangeState(EnemyStates.FlyingEn_Death);
    }
    protected override void ChangeCurrentAnimation()
    {
        //анимация
    }
}
