using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PlayerAttackLogic : AttakingObjLogic
{
   
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
            if (PlayerController.Instance != null && item != null)
                return (item.info as WeaponItemInfo).damage+PlayerInventory.Instance.weaponAndPerks.GetAllItems()
                                                           .Where(rp => rp != null && rp.info is PerkInfo)
                                                           .Sum(rp => (rp.info as PerkInfo).partBaseDamage);
            else
                return 1;
        }
    }
    private float range
    {
        get
        {
            if (PlayerController.Instance != null && item != null)
                return (item.info as WeaponItemInfo).damage + PlayerInventory.Instance.weaponAndPerks.GetAllItems()
                                                           .Where(rp => rp != null && rp.info is PerkInfo)
                                                           .Sum(rp => (rp.info as PerkInfo).partbaseRange);
            else
                return 1;
        }
    }
    private float coolDown
    {
        get
        {
            if (PlayerController.Instance != null && item != null)
                return (item.info as WeaponItemInfo).damage + PlayerInventory.Instance.weaponAndPerks.GetAllItems()
                                                           .Where(rp => rp != null && rp.info is PerkInfo)
                                                           .Sum(rp => (rp.info as PerkInfo).partBaseCooldown);
            else
                return 1;
        }
    }
    private Vector3 sideAttackArea
    {
        get
        {
            sideAttackTransform.localPosition = new Vector3(0f, 3f, 1f + range / 2);
                return new Vector3(range, sideAttackTransformArea.y, sideAttackTransformArea.z);
        }
    }
    private Vector3 upAttackArea
    {
        get
        {
                upAttackTransform.localPosition = new Vector3(0,  7 + range/2,0);
                return new Vector3(upAttackTransformArea.x, range, upAttackTransformArea.z);
        }
    }
    private Vector3 downAttackArea
    {
        get
        {
           
                downAttackTransform.localPosition = new Vector3(0,  -1-range/2,0);
                return new Vector3(downAttackTransformArea.x, range, downAttackTransformArea.z);
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

    Rigidbody rb => PlayerController.Instance.rb;


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
            PlayerController.Instance.anim.SetTrigger("Attacking");

            if (PlayerController.Instance.playerStateList.Axis.y == 0 )
            {

                int recoilLeftOrRight = PlayerController.Instance.playerStateList.lookRight ? 1 : -1;

                Hit(sideAttackTransform, sideAttackArea, ref PlayerController.Instance.playerStateList.recoilX, Vector2.right*recoilLeftOrRight, recoilXSpeed);
            }
           
            
            else if (PlayerController.Instance.playerStateList.Axis.y > 0)
            {
                Hit(upAttackTransform, upAttackArea, ref PlayerController.Instance.playerStateList.recoilY, Vector2.up, recoilYSpeed);
            }
            
            
            else if ( PlayerController.Instance.playerStateList.Axis.y < 0 && !PlayerController.Instance.playerStateList.grounded)
            {
                Hit(downAttackTransform, downAttackArea, ref PlayerController.Instance.playerStateList.recoilY, Vector2.down, recoilYSpeed);
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
                    if (PlayerController.Instance.playerHealthController.energy + PlayerController.Instance.playerHealthController.energyGain > PlayerController.Instance.playerHealthController.maxEnergy)
                    {
                        PlayerController.Instance.playerHealthController.energy = PlayerController.Instance.playerHealthController.maxEnergy;
                    }
                    else
                        PlayerController.Instance.playerHealthController.energy += PlayerController.Instance.playerHealthController.energyGain;

                }
                PlayerController.Instance.playerHealthController.OnEnergyChangedCallBack?.Invoke();
            }
        }


    }
    public void Recoil()
    {
        if (PlayerController.Instance.playerStateList.recoilX)
            if (PlayerController.Instance.playerStateList.lookRight)
                rb.velocity = new Vector2(-recoilXSpeed, 0);
            else
                rb.velocity = new Vector2(recoilXSpeed, 0);
        if (PlayerController.Instance.playerStateList.recoilY)
        {
            rb.useGravity = false;
            if (PlayerController.Instance.playerStateList.Axis.y < 0)
                rb.velocity = new Vector2(rb.velocity.x, recoilYSpeed);

            else
                rb.velocity = new Vector2(rb.velocity.x, -recoilYSpeed);
        }
        else
            rb.useGravity = true;


        if (PlayerController.Instance.playerStateList.recoilX && stepsXRecoiled < recoilXSteps)
            stepsXRecoiled++;
        else
            StopRecoilX();
        if (PlayerController.Instance.playerStateList.recoilY && stepsYRecoiled < recoilYSteps)
            stepsYRecoiled++;
        else
            StopRecoilY();
        if (PlayerController.Instance.playerStateList.grounded)
            StopRecoilY();


    }

    public void StopRecoilX()
    {
        stepsXRecoiled = 0;
        PlayerController.Instance.playerStateList.recoilX = false;
    }
    public void StopRecoilY()
    {
        stepsYRecoiled = 0;
        PlayerController.Instance.playerStateList.recoilY = false;
    }

    public void CastSpell()
    {
        if(Input.GetButtonUp("Cast/Heal")&& PlayerController.Instance.castOrHealTimer<=0.05f && timeSinceAttack >=timeBetweenCast&& PlayerController.Instance.playerHealthController.energy >= energySpellCost)
        {
            PlayerController.Instance.playerStateList.casting = true;
            timeSinceAttack= 0;
            StartCoroutine(CastCoroutine());
            PlayerController.Instance.playerHealthController. OnEnergyChangedCallBack?.Invoke();
        }
        else
        {
            timeSinceAttack += Time.deltaTime;
        }
       if(PlayerController.Instance.playerStateList.grounded)
            downSpellFireball.SetActive(false);
        if (downSpellFireball.activeInHierarchy)
           rb.velocity += downSpellForce * Vector3.down;
    }
    IEnumerator CastCoroutine()
    {

        //анимция
        yield return new WaitForSeconds(0.15f);//потом поменть
        if (PlayerController.Instance.playerStateList.Axis.y == 0 || (PlayerController.Instance.playerStateList.Axis.y < 0 && PlayerController.Instance.playerStateList.grounded))
        {
            GameObject _fireBall = Instantiate(sideSpellFireball, sideAttackTransform.position, Quaternion.identity);
            if (PlayerController.Instance.playerStateList.lookRight)
                _fireBall.transform.eulerAngles = Vector3.zero;
            else
                _fireBall.transform.eulerAngles = new Vector2(_fireBall.transform.eulerAngles.x, 180);
            PlayerController.Instance.playerStateList.recoilX = true;
        }
        else if (PlayerController.Instance.playerStateList.Axis.y > 0)
        {
           GameObject _upFireBall =Instantiate(upSpellFireball, transform);
            _upFireBall.transform.localPosition = new Vector2(0f, 3.5f);

            rb.velocity= Vector3.zero;
            Destroy(_upFireBall, timeOfUpAttack);
        }
        else if(PlayerController.Instance.playerStateList.Axis.y < 0 && !PlayerController.Instance.playerStateList.grounded)
        {
            downSpellFireball.SetActive(true);
        }


        PlayerController.Instance.playerHealthController.energy -= energySpellCost;
        yield return new WaitForSeconds(0.35f);
        //анимция
        PlayerController.Instance.playerStateList.casting = false;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (other.GetComponent<EnemyHealthController>() != null && PlayerController.Instance.playerStateList.casting)
        {
            other.GetComponent<EnemyHealthController>().DamageMoment(spellDamage, (other.transform.position - transform.position).normalized, -recoilYSpeed);
        }
    }
}
