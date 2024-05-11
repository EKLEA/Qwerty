using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class Boss1Events : MonoBehaviour
{
    void SlashDamagePlayer()
    {
        if (PlayerController.Instance.transform.position.x > transform.position.x ||
            PlayerController.Instance.transform.position.x < transform.position.x)
        {
            Hit(Boss1.Instance.sideAttackTransform, Boss1.Instance.sideAttackArea);
        }
        else if (PlayerController.Instance.transform.position.y > transform.position.y)
        {
            Hit(Boss1.Instance.upAttackTransform, Boss1.Instance.upAttackArea);
        }
        else if (PlayerController.Instance.transform.position.y < transform.position.y)
        {
            Hit(Boss1.Instance.downAttackTransform, Boss1.Instance.upAttackArea);
        }
    }
    private void Hit(Transform _attackTransform, Vector2 _attackArea)
    {
        Collider[] objectsToHit = Physics.OverlapBox(_attackTransform.position, _attackArea, Quaternion.identity);
        for (int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].GetComponent<PlayerController>() != null)
            {
                objectsToHit[i].GetComponent<PlayerController>().playerHealthController.DamageMoment(Boss1.Instance.damage,Vector2.zero,0f);
            }
        }
    }

    void Parrying()
    {
        Boss1.Instance.parrying = true;
    }
    void BendDownCheck()
    {
        if (Boss1.Instance.barrageAttack)
        {
            StartCoroutine(BarrageAttackTransition());
        }
        if (Boss1.Instance.outbreakAttack)
        {
            StartCoroutine(OutbreakAttackTransition());
        }
        if (Boss1.Instance.bounceAttack)
        {
            Boss1.Instance.animator.SetTrigger("Bounce1");
        }
    }
    void BarrageOrOutbreak()
    {
        if (Boss1.Instance.barrageAttack)
        {
            Boss1.Instance.StartCoroutine(Boss1.Instance.Barrage());
        }
        if (Boss1.Instance.outbreakAttack)
        {
            Boss1.Instance.StartCoroutine(Boss1.Instance.Outbreak());
        }
    }
    IEnumerator BarrageAttackTransition()
    {
        yield return new WaitForSeconds(1f);
        Boss1.Instance.animator.SetBool("Cast", true);
    }
    IEnumerator OutbreakAttackTransition()
    {
        yield return new WaitForSeconds(1f);
        Boss1.Instance.animator.SetBool("Cast", true);
    }
    void DestroyAfterDeath()
    {
        SpawnBoss.Instance.IsNotTrigger();
        Boss1.Instance.enemyHealth. DestroyAfterDeath();
    }


}
