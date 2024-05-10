using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Dive : StateMachineBehaviour
{
    Rigidbody rb;
    bool callOnce;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        Boss1.Instance.divingCollider.SetActive(true);
        if (Boss1.Instance.Grounded())
        {
            Boss1.Instance.divingCollider.SetActive(false);
            if (!callOnce)
            {
                Boss1.Instance.DivingPillars();
                animator.SetBool("Dive", false);
                Boss1.Instance.ResetAllAttacks();
                callOnce = true;
            }

        }
    }
   
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        callOnce = false;
    }
}
