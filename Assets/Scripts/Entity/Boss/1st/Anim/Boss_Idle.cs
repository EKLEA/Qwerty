using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.ProBuilder.MeshOperations;

public class Boss_Idle : StateMachineBehaviour
{
   Rigidbody rb;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb=animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.velocity = Vector3.zero;
        RunToPLayer(animator);

        if(Boss1.Instance.attackCountdown<=0)
        {
            Boss1.Instance.AttackHandler();
            Boss1.Instance.attackCountdown = Boss1.Instance.attackTimer;

        }
    }
    void RunToPLayer(Animator animator)
    {

        if (Vector3.Distance(PlayerController.Instance.transform.position, rb.position) >= Boss1.Instance.attackRange)
        {
            animator.SetBool("Run", true);
        }

        else
        {
            return;
        }
    }
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        
    }
}
