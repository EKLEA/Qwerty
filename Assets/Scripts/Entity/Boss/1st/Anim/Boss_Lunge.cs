using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lunge : StateMachineBehaviour
{
    Rigidbody rb;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb = animator.GetComponent<Rigidbody>();
    }
    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        rb.useGravity = false;
        int _dir = Boss1.Instance.facingRight ? 1 : -1;
        rb.velocity = new Vector3(_dir * (Boss1.Instance.runSpeed * 5), 0f);
        if(Vector3.Distance(PlayerController.Instance.transform.position, rb.position) <= Boss1.Instance.attackRange &&
            !Boss1.Instance.damagedPlayer)
        {
            PlayerController.Instance.playerHealthController.DamageMoment(Boss1.Instance.damage,Vector2.zero,0f);
            Boss1.Instance.damagedPlayer = true;
        }
    }  
    
    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
    }
}
