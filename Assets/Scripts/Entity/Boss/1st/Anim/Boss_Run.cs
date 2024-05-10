using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Run : StateMachineBehaviour
{
    Rigidbody rb;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {

        TargetPlayerPosition(animator);
        if (Boss1.Instance.attackCountdown <= 0)
        {
            Boss1.Instance.AttackHandler();
            Boss1.Instance.attackCountdown = Boss1.Instance.attackTimer;

        }
    }
    private void TargetPlayerPosition(Animator animator)
    {
        if (Boss1.Instance.Grounded())
        {
            Boss1.Instance.Flip();
            Vector3 target = new Vector3(PlayerController.Instance.transform.position.x, rb.position.y, PlayerController.Instance.transform.position.z);
            Vector3 _newpos = Vector3.MoveTowards(rb.position, target, Boss1.Instance.runSpeed*Time.fixedDeltaTime);
            
            rb.MovePosition(_newpos);

        }
        else
        {
            rb.velocity = new Vector2(rb.velocity.x, -25);

        }
        if (Vector3.Distance(PlayerController.Instance.transform.position, rb.position) <= Boss1.Instance.attackRange)
        {
            animator.SetBool("Run", false);
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.SetBool("Run", false);
    }
}
