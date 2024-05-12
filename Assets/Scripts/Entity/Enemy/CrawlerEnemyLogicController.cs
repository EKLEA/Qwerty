using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CrawlerEnemyLogicController : EnemyLogicBase
{
    [SerializeField] float flipWaitTime;
    [SerializeField] float ledgeCheckX;
    [SerializeField] float ledgeCheckY;
    [SerializeField] LayerMask whatIsGround;
    protected override void Update()
    {
        base.Update();
        if (!PlayerController.Instance. playerStateList.alive)
            ChangeState(EnemyStates.Crawler_Idle);
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
        switch (GetCurrectEnemyState)
        {
            case EnemyStates.Crawler_Idle:
                Vector3 _ledgeCheckStart = transform.rotation.y > 0 ? new Vector3(ledgeCheckX, 0) : new Vector3(-ledgeCheckX, 0);
                Vector2 _wallCheckDir = transform.rotation.y > 0 ? transform.right : -transform.right;

                if (!Physics.Raycast(transform.position + _ledgeCheckStart, Vector2.down, ledgeCheckY, whatIsGround)
                    || Physics.Raycast(transform.position, _wallCheckDir, ledgeCheckX, whatIsGround))
                    ChangeState(EnemyStates.Crawler_Flip);

                if (transform.rotation.y > 0)
                    rb.velocity = new Vector2(speed, rb.velocity.y);
                else
                    rb.velocity= new Vector2(-speed,rb.velocity.y);
                break;
            case EnemyStates.Crawler_Flip:
                timer += Time.deltaTime;
                if(timer > flipWaitTime)
                {
                    timer = 0;
                    transform.eulerAngles = new Vector3(0,transform.rotation.y*-1,0);
                    ChangeState(EnemyStates.Crawler_Idle);
                }
                break;
        }
    }
}
