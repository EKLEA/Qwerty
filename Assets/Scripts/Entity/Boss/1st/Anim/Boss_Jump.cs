using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Jump : StateMachineBehaviour
{
    Rigidbody rb;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        DiveAttack();
    }
    void DiveAttack()
    {
        if (Boss1.Instance.diveAttack)
        {
            Boss1.Instance.Flip();
            Vector2 _newPos = Vector2.MoveTowards(rb.position, Boss1.Instance.moveToPos, Boss1.Instance.runSpeed * 3 * Time.deltaTime);
            rb.MovePosition(_newPos);
            float _distance = Vector3.Distance(rb.position, _newPos);
            if (_distance < 0.1f)
            {
                Boss1.Instance.Dive();
            }

        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
