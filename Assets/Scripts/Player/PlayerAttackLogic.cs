using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLogic : AttakingObjLogic
{
   
    private PlayerController playerController => GetComponent<PlayerController>();
    private PlayerStateList pState => playerController.playerStateList;
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
                return 0.01f;
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
                sideAttackTransform.localPosition = new Vector3(0f, 3f, 1.5f);
                return sideAttackTransformArea;
            }
            else
            {
                sideAttackTransform.localPosition = new Vector3(0f, 3f, 1f + range / 2);
                return new Vector3(range, sideAttackTransformArea.y, sideAttackTransformArea.z);
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
                return upAttackTransformArea;
            }
            else
            {
                upAttackTransform.localPosition = new Vector3(0,  7 + range/2,0);
                return new Vector3(upAttackTransformArea.x, range, upAttackTransformArea.z);
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
                return downAttackTransformArea;
            }
            else
            {
                downAttackTransform.localPosition = new Vector3(0,  -1-range/2,0);
                return new Vector3(downAttackTransformArea.x, range, downAttackTransformArea.z);
            }
        }
    }
    [SerializeField] private int recoilXSteps = 5;
    [SerializeField] private int recoilYSteps = 5;
    [SerializeField] private int recoilXSpeed = 100;
    [SerializeField] private int recoilYSpeed = 100;

    private int stepsXRecoiled, stepsYRecoiled;

    private float timeSinceAttack;
    [SerializeField] Transform sideAttackTransform, upAttackTransform, downAttackTransform;
    [SerializeField] Vector3 sideAttackTransformArea;
    [SerializeField] Vector3 upAttackTransformArea;
    [SerializeField] Vector3 downAttackTransformArea;

    public Rigidbody rb => playerController.rb;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }
    public override void Attack(bool attack)
    {
        
        
        if (attack && timeSinceAttack >= coolDown)
        {
           
            playerController.anim.SetTrigger("Attacking");

            if (playerController.playerStateList.Axis.y == 0 )
                Hit(sideAttackTransform, sideAttackArea,ref pState.recoilX,recoilXSpeed);
            else if (playerController.playerStateList.Axis.y > 0)
                Hit(upAttackTransform, upAttackArea,ref pState.recoilY,recoilYSpeed);
            else if ( playerController.playerStateList.Axis.y < 0 && !playerController.playerStateList.jumping)
                Hit(downAttackTransform, downAttackArea, ref pState.recoilY, recoilYSpeed);
            timeSinceAttack = 0;
        }
        timeSinceAttack += Time.deltaTime;
    }
    private void Hit(Transform _attackTransform, Vector3 _attackArea,ref bool _recoilDir, float _recoilStrenght)
    {
        Collider[] objectsToHit = Physics.OverlapBox(_attackTransform.position, _attackArea, Quaternion.identity, attacableLayer);

        if (objectsToHit.Length > 0 )
        {
            _recoilDir = true;

        }
        for (int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].gameObject.GetComponent<IDamagable>() != null)
                objectsToHit[i].gameObject.GetComponent<IDamagable>().DamageMoment(damage, (transform.position - objectsToHit[i].transform.position).normalized, _recoilStrenght);
        }


    }
    public void Recoil()
    {
        if (pState.recoilX)
            if (pState.lookRight)
                rb.velocity = new Vector2(-recoilXSpeed, 0);
            else
                rb.velocity = new Vector2(recoilXSpeed, 0);
        if (pState.recoilY)
        {
            rb.useGravity = false;
            if (pState.Axis.y < 0)
                rb.velocity = new Vector2(rb.velocity.x, recoilYSpeed);

            else
                rb.velocity = new Vector2(rb.velocity.x, -recoilYSpeed);
        }
        else
            rb.useGravity = true;


        if (pState.recoilX && stepsXRecoiled < recoilXSteps)
            stepsXRecoiled++;
        else
            StopRecoilX();
        if (pState.recoilY && stepsYRecoiled < recoilYSteps)
            stepsYRecoiled++;
        else
            StopRecoilY();
        if (pState.grounded)
            StopRecoilY();


    }

    public void StopRecoilX()
    {
        stepsXRecoiled = 0;
        pState.recoilX = false;
    }
    public void StopRecoilY()
    {
        stepsYRecoiled = 0;
        pState.recoilY = false;
    }
}
