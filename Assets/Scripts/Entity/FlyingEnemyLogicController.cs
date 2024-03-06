using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyingEnemyLogicController : EnemyLogicBase
{
    [SerializeField] float chaseDistance;
    [SerializeField] float stunDruration;
    protected void Start()
    {
        ChangeState(EnemyStates.FlyingEn_Idle);
    }
    public override  void UpdateEnemyStates()
    {
        float dist = Vector2.Distance(transform.position, playerController.transform.position);

        switch(currectEnemyState)
        {
            case EnemyStates.FlyingEn_Idle:

                if (dist < chaseDistance)
                    ChangeState(EnemyStates.FlyingEn_Chase);
                break;
            case EnemyStates.FlyingEn_Chase:
               rb.MovePosition( Vector2.MoveTowards(transform.position, playerController.transform.position,Time.deltaTime*speed));
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
    void FlipFlyingEn()
    {
        if(playerController.transform.position.x<transform.position.x)
            transform.eulerAngles = new Vector3(0, 90, 0);
        else 
            transform.eulerAngles = new Vector3(0, -90, 0);
    }
}
