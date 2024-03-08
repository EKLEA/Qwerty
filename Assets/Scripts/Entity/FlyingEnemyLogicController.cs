using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class FlyingEnemyLogicController : EnemyLogicBase
{
    [SerializeField] float chaseDistance;
    [SerializeField] float stunDruration;
    private Vector3 targetPosition;
    protected void Start()
    {
        ChangeState(EnemyStates.FlyingEn_Idle);
        rb.useGravity = false;
    }
    public override  void UpdateEnemyStates()
    {
        float dist = Vector2.Distance(transform.position, playerController.transform.position);

        switch(GetCurrectEnemyState)
        {
            case EnemyStates.FlyingEn_Idle:

                if (dist < chaseDistance)
                    ChangeState(EnemyStates.FlyingEn_Chase);
                break;
            case EnemyStates.FlyingEn_Chase:
                targetPosition = playerController.transform.position;
                MoveTowardsTarget();
                FlipFlyingEn();
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
    void MoveTowardsTarget()
    {
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * speed);
    }

    void FlipFlyingEn()
    {
        if(playerController.transform.position.x<transform.position.x)
            transform.eulerAngles = new Vector3(0, 90, 0);
        else 
            transform.eulerAngles = new Vector3(0, -90, 0);
    }
}
