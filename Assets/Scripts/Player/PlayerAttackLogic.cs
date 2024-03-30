using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackLogic : AttakingObjLogic
{
   
    private PlayerController playerController => GetComponent<PlayerController>();
    private PlayerStateList pState => playerController.playerStateList;
    public GameObject Hand;
    [SerializeField] LayerMask attacableLayer;
    public  override Item item
    {
        get 
        {
            if (PlayerInventory.Instance != null)
            {
                if (PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item != null)
                    return PlayerInventory.Instance.weaponAndPerks.GetAllSlots()[0].item;
                else
                    return null;
            }
            else
                return null;

        }
    }


    private float damage
    {
        get
        {
            if (item == null)
                return 1;
            else
                return (item.info as WeaponItemInfo).damage;
        }
    }
    private float range
    {
        get
        {
            if (item == null)
                return 1;
            else
                return (item.info as WeaponItemInfo).range;
        }
    }
    private float coolDown
    {
        get
        {
            if (item == null)
                return 0.01f;
            else
                return (item.info as WeaponItemInfo).cooldown;
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

    [SerializeField] float energySpellCost =0.3f; // потом вынести в инфу о спеллах
    [SerializeField] float timeBetweenCast=0.5f;
   

    [SerializeField] int spellDamage= 1; // потом вынести в инфу о спеллах
    [SerializeField] float downSpellForce = 0.5f;
    [SerializeField] float timeOfUpAttack = 0.4f;

    // настройки ниже потом передалть когда будет настройка персонажа типо рук ноги и ид
    [SerializeField] GameObject sideSpellFireball;
    [SerializeField] GameObject upSpellFireball;
    [SerializeField] GameObject downSpellFireball;

    Rigidbody rb => playerController.rb;


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }
    public override void Attack()
    {
        
        
        if (Input.GetButtonDown("Attack")&& timeSinceAttack >= coolDown)
        {
           //каждому добавить эффекты
            playerController.anim.SetTrigger("Attacking");

            if (playerController.playerStateList.Axis.y == 0 )
            {

                int recoilLeftOrRight = pState.lookRight ? 1 : -1;

                Hit(sideAttackTransform, sideAttackArea, ref pState.recoilX, Vector2.right*recoilLeftOrRight, recoilXSpeed);
            }
           
            
            else if (playerController.playerStateList.Axis.y > 0)
            {
                Hit(upAttackTransform, upAttackArea, ref pState.recoilY, Vector2.up, recoilYSpeed);
            }
            
            
            else if ( playerController.playerStateList.Axis.y < 0 && !playerController.playerStateList.grounded)
            {
                Hit(downAttackTransform, downAttackArea, ref pState.recoilY, Vector2.down, recoilYSpeed);
            }
            timeSinceAttack = 0;
        }
        timeSinceAttack += Time.deltaTime;
    }
    private void Hit(Transform _attackTransform, Vector3 _attackArea,ref bool _recoilBool, Vector2 _recoilDir, float _recoilStrenght)
    {
        Collider[] objectsToHit = Physics.OverlapBox(_attackTransform.position, _attackArea, Quaternion.identity, attacableLayer);

        if (objectsToHit.Length > 0 )
        {
            _recoilBool = true;

        }
        for (int i = 0; i < objectsToHit.Length; i++)
        {
            if (objectsToHit[i].gameObject.GetComponent<DamagableObj>() != null)
            { 
                objectsToHit[i].gameObject.GetComponent<DamagableObj>().DamageMoment(damage, _recoilDir, _recoilStrenght);
                if (objectsToHit[i].CompareTag("Enemy"))
                {
                    if (playerController.playerHealthController.energy + playerController.playerHealthController.energyGain > playerController.playerHealthController.maxEnergy)
                    {
                        playerController.playerHealthController.energy = playerController.playerHealthController.maxEnergy;
                    }
                    else
                        playerController.playerHealthController.energy += playerController.playerHealthController.energyGain;

                }
                playerController.playerHealthController.OnEnergyChangedCallBack?.Invoke();
            }
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

    public void CastSpell()
    {
        if(Input.GetButtonUp("Cast/Heal")&& playerController.castOrHealTimer<=0.05f && timeSinceAttack >=timeBetweenCast&& playerController.playerHealthController.energy >= energySpellCost)
        {
            playerController.playerStateList.casting = true;
            timeSinceAttack= 0;
            StartCoroutine(CastCoroutine());
            playerController.playerHealthController. OnEnergyChangedCallBack?.Invoke();
        }
        else
        {
            timeSinceAttack += Time.deltaTime;
        }
       if(playerController.playerStateList.grounded)
            downSpellFireball.SetActive(false);
        if (downSpellFireball.activeInHierarchy)
           rb.velocity += downSpellForce * Vector3.down;
    }
    IEnumerator CastCoroutine()
    {

        //анимция
        yield return new WaitForSeconds(0.15f);//потом поменть
        if (playerController.playerStateList.Axis.y == 0 || (playerController.playerStateList.Axis.y < 0 && playerController.playerStateList.grounded))
        {
            GameObject _fireBall = Instantiate(sideSpellFireball, sideAttackTransform.position, Quaternion.identity);
            if (playerController.playerStateList.lookRight)
                _fireBall.transform.eulerAngles = Vector3.zero;
            else
                _fireBall.transform.eulerAngles = new Vector2(_fireBall.transform.eulerAngles.x, 180);
            playerController.playerStateList.recoilX = true;
        }
        else if (playerController.playerStateList.Axis.y > 0)
        {
           GameObject _upFireBall =Instantiate(upSpellFireball, transform);
            _upFireBall.transform.localPosition = new Vector2(0f, 3.5f);

            rb.velocity= Vector3.zero;
            Destroy(_upFireBall, timeOfUpAttack);
        }
        else if(playerController.playerStateList.Axis.y < 0 && !playerController.playerStateList.grounded)
        {
            downSpellFireball.SetActive(true);
        }


        playerController.playerHealthController.energy -= energySpellCost;
        yield return new WaitForSeconds(0.35f);
        //анимция
        playerController.playerStateList.casting = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<EnemyHealthController>() != null && playerController.playerStateList.casting)
        {
            other.GetComponent<EnemyHealthController>().DamageMoment(spellDamage, (other.transform.position - transform.position).normalized, -recoilYSpeed);
        }
    }
}
