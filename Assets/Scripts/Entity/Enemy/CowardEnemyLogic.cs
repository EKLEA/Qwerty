using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CowardEnemyLogic : EnemyLogicBase
{
    [SerializeField] float ledgeCheckX;
    [SerializeField] float ledgeCheckY;
    [SerializeField] float cowardSpeedMultiplier;
    [SerializeField] float chargeDuration;
    [SerializeField] float JumpForce;
    [SerializeField] LayerMask whatIsGround;
    protected new void Start()
    {
        base.Start();
        ChangeState(EnemyStates.Coward_Idle);
    }
    protected override void Update()
    {
        base.Update();
        if (!PlayerController.Instance.playerStateList.alive)
            ChangeState(EnemyStates.Coward_Idle);
    }
    public override void UpdateEnemyStates()
    {
        Vector3 _ledgeCheckStart = transform.rotation.y > 0 ? new Vector3(ledgeCheckX, 0, 0) : new Vector3(-ledgeCheckX, 0, 0);


        Vector2 _wallCheckDir = transform.forward;
        switch (GetCurrectEnemyState)
        {
            case EnemyStates.Coward_Idle:
                if (!Physics.Raycast(new Vector3(transform.position.x, 3, transform.position.z) + _ledgeCheckStart, Vector2.down, ledgeCheckY, whatIsGround)
                    || Physics.Raycast(new Vector3(transform.position.x, 3, transform.position.z), _wallCheckDir, ledgeCheckX, whatIsGround))

                {

                    transform.eulerAngles = transform.rotation.y > 0 ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);
                }


                Physics.Raycast(new Vector3(transform.position.x, 3, transform.position.z) + _ledgeCheckStart, _wallCheckDir, out RaycastHit _hit, ledgeCheckX * 10);


                if (_hit.collider != null && _hit.collider.gameObject.CompareTag("Player"))

                {
                    ChangeState(EnemyStates.Coward_Suprised);
                }

                if (transform.rotation.y > 0)
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                else
                    rb.velocity = new Vector2(-speed, rb.velocity.y);
                break;

            case EnemyStates.Coward_Suprised:
                transform.eulerAngles = transform.rotation.y > 0 ? new Vector3(0, -90, 0) : new Vector3(0, 90, 0);
                rb.velocity = new Vector2(0, JumpForce);
                ChangeState(EnemyStates.Coward_Run);
                break;
            case EnemyStates.Coward_Run:
                timer += Time.deltaTime;
                if (timer < chargeDuration)
                {
                    if (Physics.Raycast(new Vector3(transform.position.x, 3, transform.position.z), Vector3.down, ledgeCheckY, whatIsGround))
                    {
                        if (transform.rotation.y > 0)
                            rb.velocity = new Vector2(speed * cowardSpeedMultiplier, rb.velocity.y);
                        else
                            rb.velocity = new Vector2(-speed * cowardSpeedMultiplier, rb.velocity.y);
                    }
                    else
                    {
                        rb.velocity = new Vector2(0, rb.velocity.y);
                    }
                }
                else
                {
                    timer = 0;
                    ChangeState(EnemyStates.Coward_Idle);
                }

                break;
        }
    }
}
