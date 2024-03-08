using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChargerEnemyLogicController : EnemyLogicBase
{
    [SerializeField] float ledgeCheckX;
    [SerializeField] float ledgeCheckY;
    [SerializeField] float chargeSpeedMultiplier;
    [SerializeField] float chargeDuration;
    [SerializeField] float JumpForce;
    [SerializeField] LayerMask whatIsGround;
    private void Start()
    {
        ChangeState(EnemyStates.Charger_Idle);
    }
    private void OnCollisionStay(Collision collision)
    {
        rb.constraints = RigidbodyConstraints.FreezePositionY | rb.constraints;
    }
    private void OnCollisionExit(Collision collision)
    {
        rb.constraints &= ~RigidbodyConstraints.FreezePositionY;
    }
    public override void UpdateEnemyStates()
    {
        Vector3 _ledgeCheckStart = transform.rotation.y > 0 ? new Vector3(ledgeCheckX, 0) : new Vector3(-ledgeCheckX, 0);
        Vector2 _wallCheckDir = transform.rotation.y > 0 ? transform.right : -transform.right;
        switch (GetCurrectEnemyState)
        {
            case EnemyStates.Charger_Idle:
                

                if (!Physics.Raycast(transform.position + _ledgeCheckStart, Vector2.down, ledgeCheckY, whatIsGround)
                    || Physics.Raycast(transform.position, _wallCheckDir, ledgeCheckX, whatIsGround))

                    transform.eulerAngles = new Vector3(0, transform.rotation.y * -1, 0);

                RaycastHit hit;
                if (Physics.Raycast(transform.position + _ledgeCheckStart, _wallCheckDir, out hit, ledgeCheckX * 10))
                {
                    if (hit.collider != null && hit.collider.gameObject.CompareTag("Player"))
                    {
                        ChangeState(EnemyStates.Charger_Suprised);
                    }
                }
                if (transform.rotation.y > 0)
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                else
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                break;
            case EnemyStates.Charger_Suprised:
                rb.velocity = new Vector2(0, JumpForce);

                ChangeState(EnemyStates.Charger_Charge);
                break;
            case EnemyStates.Charger_Charge:
                timer += Time.deltaTime;
                if (timer < chargeDuration)
                {
                    if (Physics.Raycast(transform.position, Vector3.down, ledgeCheckY, whatIsGround))
                    {
                        if (transform.rotation.y > 0)
                            rb.velocity = new Vector2(speed*chargeSpeedMultiplier, rb.velocity.y);
                        else
                            rb.velocity = new Vector2(-speed * chargeSpeedMultiplier, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity=new Vector2(0,rb.velocity.y); 
                    }
                }
                else
                {
                    timer = 0;
                    ChangeState(EnemyStates.Charger_Idle);
                }

                break;
        }
    }

}
