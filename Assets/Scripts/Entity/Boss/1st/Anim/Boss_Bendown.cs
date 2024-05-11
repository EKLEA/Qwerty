using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bendown : StateMachineBehaviour
{
    Rigidbody rb;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        OutbreakAttack();
    }
    void OutbreakAttack()
    {
        if (Boss1.Instance.outbreakAttack)
        {
            Vector3 _newPos = Vector3.MoveTowards(rb.position, Boss1.Instance.moveToPos, 
                Boss1.Instance.runSpeed * 1.5f * Time.fixedDeltaTime);
            rb.MovePosition(_newPos);
            float _distance = Vector3.Distance(rb.position, _newPos);
            if (_distance < 0.1f)
            {
                Boss1.Instance.rb.constraints = RigidbodyConstraints.FreezePosition;
            }

        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("BendDown");
    }
}
