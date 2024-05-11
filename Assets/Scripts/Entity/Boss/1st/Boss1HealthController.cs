using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1HealthController : EnemyHealthController

{
    protected new void OnCollisionEnter(Collision other)
    {
        
    }
    public override void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        if (!Boss1.Instance.stunned)
        {
            if (!Boss1.Instance.parrying)
            {
                if (Boss1.Instance.canStun)
                {
                    Boss1.Instance.hitCounter++;
                    if (Boss1.Instance.hitCounter >= 13)
                    {
                        Boss1.Instance.ResetAllAttacks();
                        Boss1.Instance.StartCoroutine(Boss1.Instance.Stunned());
                    }
                }
                base.DamageMoment(_damageDone, _hitDirection, _hitForce);

                if (Boss1.Instance.currectEnemyState != Boss1.EnemyStates.Boss1_Stage4)
                {
                    Boss1.Instance.ResetAllAttacks();
                    Boss1.Instance.StartCoroutine(Boss1.Instance.Parry());
                }
            }
            else
            {
                StopCoroutine(Boss1.Instance.Parry());
                Boss1.Instance.ResetAllAttacks();
                Boss1.Instance.StartCoroutine(Boss1.Instance.Slash());
            }
        }
        else
        {
            Boss1.Instance.StopCoroutine(Boss1.Instance.Stunned());
            Boss1.Instance.animator.SetBool("Stunned", false);
            Boss1.Instance.stunned = false;
        }
        if (health > 20)
            Boss1.Instance.ChangeState(Boss1.EnemyStates.Boss1_Stage1);
        if(health<=15 && health>10)
            Boss1.Instance.ChangeState(Boss1.EnemyStates.Boss1_Stage2);
        if (health <= 10 && health > 5)
            Boss1.Instance.ChangeState(Boss1.EnemyStates.Boss1_Stage3);
        if (health <=5)
            Boss1.Instance.ChangeState(Boss1.EnemyStates.Boss1_Stage4);
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>()!=null &&(Boss1.Instance.diveAttack|| Boss1.Instance.bounceAttack))
        {
            other.GetComponent<PlayerController>().playerHealthController.DamageMoment(Boss1.Instance.damage * 2,Vector2.zero,0f);
            PlayerController.Instance.playerStateList.recoilX = true;
        }
    }
    void Death(float _destroyTime)
    {
        Boss1.Instance.ResetAllAttacks();
        Boss1.Instance.alive = false;
        rb.velocity = new Vector3(rb.velocity.x, -25, 0);
        Boss1.Instance.animator.SetTrigger("Die");
    }
    public void DestroyAfterDeath()
    {
        Destroy(gameObject);
    }
   
}
