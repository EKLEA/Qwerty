using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Bounce2 :StateMachineBehaviour
{
    Rigidbody rb;
    bool callOnce;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Vector3 _forceDirection=new Vector3(Mathf.Cos(Mathf.Deg2Rad*Boss1.Instance.rotationDirectionToTarget),
            Mathf.Sin(Mathf.Deg2Rad*Boss1.Instance.rotationDirectionToTarget));
        rb.AddForce(_forceDirection * 3, ForceMode.Impulse);

        Boss1.Instance.divingCollider.SetActive(true);

        if (Boss1.Instance.Grounded())
        {
            Boss1.Instance.divingCollider.SetActive(false);
            if (!callOnce)
            {
                Boss1.Instance.ResetAllAttacks();
                Boss1.Instance.CheckBounce();
                callOnce= true;
            }
            animator.SetTrigger("Grounded");
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        animator.ResetTrigger("Bounce2");
        animator.ResetTrigger("Grounded");
        callOnce = false;
    }

}
