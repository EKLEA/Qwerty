using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLogic : AttakingObjLogic
{
    [SerializeField] PlayerController playerController;
    public GameObject Hand;
    [SerializeField] LayerMask attacableLayer;
    public override IItem item
    {
        get
        {
            try
            {
                return Hand.GetComponentInChildren<Item>().GetExItem();
            }
            catch
            {
                return null;
            }
        }
    }
    private float damage
    {
        get
        {
            if (item == null)
                return 1;
            else
                return item.info.usableItemInfo.damage;
        }
    }
    private float range
    {
        get
        {
            if (item == null)
                return 1;
            else
                return item.info.usableItemInfo.range;
        }
    }
    private float coolDown
    {
        get
        {
            if (item == null)
                return 1;
            else
                return item.info.usableItemInfo.coolDown;
        }
    }
    private Vector3 sideAttackArea
    {
        get
        {
            if (item == null)
            {
                sideAttackTransform.localPosition = new Vector3(1.5f, 3f, 0f);
                return new Vector3(1, 6, 3f);
            }
            else
            {
                sideAttackTransform.localPosition = new Vector3(1f+range/2, 3f,0f);
                return new Vector3(range, 6, 3f);
            }
        }
    }
    private Vector3 upAttackArea
    {
        get
        {
            if (item == null)
            {
                upAttackTransform.localPosition = new Vector3(0, 7f, 0);
                return new Vector3(1, 1, 1);
            }
            else
            {
                upAttackTransform.localPosition = new Vector3(0,  7 + range/2,0);
                return new Vector3(1, range, 1);
            }
        }
    }
    private Vector3 downAttackArea
    {
        get
        {
            if (item == null)
            {
                downAttackTransform.localPosition = new Vector3(0, -1.5f,0);
                return new Vector3(1, 1, 1);
            }
            else
            {
                downAttackTransform.localPosition = new Vector3(0,  -1-range/2,0);
                return new Vector3(1, range, 1);
            }
        }
    }

    
    private float timeSinceAttack;
    private bool attack;
    [SerializeField] Transform sideAttackTransform, upAttackTransform, downAttackTransform;
   

    private void Update()
    {
        attack = Input.GetMouseButtonDown(0);
        Attack();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }
    public override void Attack()
    {
        timeSinceAttack += Time.deltaTime;
        if (attack && timeSinceAttack >= coolDown)
        {
            timeSinceAttack = 0;
            //анимация тута включац

            if (playerController.Axis.y == 0 )
                Hit(sideAttackTransform, sideAttackArea);
            else if (playerController.Axis.y > 0)
                Hit(upAttackTransform, upAttackArea);
            else if ( playerController.Axis.y < 0 && !playerController.isJumping)
                Hit(downAttackTransform, downAttackArea);
        }
    }
    private void Hit(Transform _attackTransform, Vector3 _attackArea)
    {
        Collider[] objectsToHit = Physics.OverlapBox(_attackTransform.position, _attackArea, Quaternion.identity, attacableLayer);

       for (int i = 0; i < objectsToHit.Length; i++)
            if (objectsToHit[i].gameObject.GetComponent<IDamagable>() != null|| objectsToHit[i].gameObject.tag!=("Player"))
                objectsToHit[i].gameObject.GetComponent<IDamagable>().DamageMoment(damage,(-transform.position - objectsToHit[i].transform.position ).normalized,100);


    }
}
