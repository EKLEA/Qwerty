using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bounce1 : StateMachineBehaviour
{
    Rigidbody rb;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        if (Boss1.Instance.bounceAttack)
        {
            Vector3 _newPos = Vector3.MoveTowards(rb.position, Boss1.Instance.moveToPos,
                Boss1.Instance.runSpeed * Random.Range(2, 4) * Time.fixedDeltaTime);

            rb.MovePosition(_newPos);
            float _distance =  Vector3.Distance(rb.position, _newPos);
            if(_distance < 0.1f)
            {
                Boss1.Instance.CalculateTargetAngle();
                animator.SetTrigger("Bounce2");
            }
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Bounce1");
    }
}
