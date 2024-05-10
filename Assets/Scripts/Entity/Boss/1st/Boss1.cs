using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
public class Boss1 : EnemyLogicBase
{
    public static Boss1 Instance;

    [SerializeField] GameObject slashEffect;
    public Transform sideAttackTransform, upAttackTransform, downAttackTransform;
    public Vector3 sideAttackArea;

    public Vector3 upAttackArea;
    public Vector3 downAttackArea;

    public float attackRange;
    public float attackTimer;
    public float damage;

    [Header("Ground Chek Settings")]
    [SerializeField] private Transform groundCheckPoint;
    [SerializeField] private float groundCheckY = 0.2f;
    [SerializeField] private float groundCheckX = 0.5f;
    [SerializeField] private LayerMask whatIsGround;

    int hitCounter;
    bool stunned, canStun;
    bool alive;
    [HideInInspector] public float runSpeed;
    public bool facingRight;


    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    protected void Start()
    {
        ChangeState(EnemyStates.Boss1_Stage1);
        alive = true;

    }
    public bool Grounded()
    {
        bool b = (Physics.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX / 2, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX / 2, 0, 0), Vector2.down, groundCheckY, whatIsGround));

        return b;

    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(sideAttackTransform.position, sideAttackArea);
        Gizmos.DrawWireCube(upAttackTransform.position, upAttackArea);
        Gizmos.DrawWireCube(downAttackTransform.position, downAttackArea);
    }

    protected override void Update()
    {
        base.Update();
        if (!attacking)
        {
            attackCountdown -= Time.deltaTime;
        }
    }
    public void Flip()
    {
        if (PlayerController.Instance.transform.position.x < transform.position.x && transform.rotation.y > 0)
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, -90);
            facingRight = false;
        }
        else
        {
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 90);
            facingRight = true;
        }
    }
    public override void UpdateEnemyStates()
    {
        if (PlayerController.Instance != null)
        {
            switch (GetCurrectEnemyState)
            {
                case EnemyStates.Boss1_Stage1:
                    {
                        break;
                    }
                case EnemyStates.Boss1_Stage2:
                    {
                        break;
                    }
                case EnemyStates.Boss1_Stage3:
                    {
                        break;
                    }
                case EnemyStates.Boss1_Stage4:
                    {
                        break;
                    }
            }
        }
    }

    [HideInInspector] public bool attacking;
    [HideInInspector] public float attackCountdown;
    [HideInInspector] public bool damagedPlayer = false;

    [HideInInspector] public bool parrying;

    [HideInInspector] public Vector3 moveToPos;
    [HideInInspector] public bool diveAttack;
    public GameObject divingCollider;
    public GameObject pillar;

    [HideInInspector] public bool barrageAttack;
    public GameObject barrageFireBall;



    //control
    public void AttackHandler()
    {
        if (currectEnemyState == EnemyStates.Boss1_Stage1)
        {
            if (Vector3.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(TripleSlash());
            }
            else
            {
                StartCoroutine(Lunge());
            }
        }
    }

    public void ResetAllAttacks()
    {
        attacking = false;
        StopCoroutine(TripleSlash());
        StopCoroutine(Lunge());
        StopCoroutine(Parry());
        StopCoroutine(Slash());

        diveAttack = false;
        barrageAttack= false;
    }
    //stage 2
    void DiveAttackJump()
    {
        attacking=true;
        moveToPos = new Vector3(PlayerController.Instance.transform.position.x, rb.position .y+ 10, PlayerController.Instance.transform.position.z);
        diveAttack=true;
        animator.SetBool("Jump", true);
    
    }
    
    public void Dive()
    {
        animator.SetBool("Dive", true);
        animator.SetBool("Jump", false);
    }
    public void DivingPillars()
    {
        Vector3 _impactPoint = groundCheckPoint.position;
        float _spawnDistanse = 5f;
        for(int i = 0; i < 10; i++)
        {
            Vector3 _pillarSpawnPointRight = _impactPoint + new Vector3(_spawnDistanse, 0, transform.position.z);
            Vector3 _pillarSpawnPointLeft= _impactPoint - new Vector3(_spawnDistanse, 0, transform.position.z);
            Instantiate(pillar, _pillarSpawnPointRight, Quaternion.Euler(0,0,-90));
            Instantiate(pillar, _pillarSpawnPointLeft, Quaternion.Euler(0,0,-90));
            _spawnDistanse += 5;
        }
        ResetAllAttacks();
    }
    void BarrageBendDown()
    {
        attacking=true;
        rb.velocity= Vector3.zero;
        barrageAttack=true;
        animator.SetTrigger("BendDown");
    }
    public IEnumerator Barrage()
    {
        rb.velocity= Vector3.zero;
        float _currentAngle = 30f;
        for(int i = 0;i<10;i++)
        {
            GameObject _projectTile = Instantiate(barrageFireBall, transform.position, Quaternion.Euler(0,0,_currentAngle));
            if (facingRight)
            {
                _projectTile.transform.eulerAngles = new Vector3(_projectTile.transform.eulerAngles.x, 0, _currentAngle+45);
            }
            else
            {
                _projectTile.transform.eulerAngles = new Vector3(_projectTile.transform.eulerAngles.x, 180, _currentAngle);
            }
            _currentAngle += 5f;
            yield return new WaitForSeconds(0.4f); 
        }
        yield return new WaitForSeconds(0.1f);
        animator.SetBool("Cast", false);
        ResetAllAttacks();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlayerController>()!=null && diveAttack)
        {
            other.GetComponent<PlayerController>().playerHealthController.DamageMoment(damage * 2, Vector2.zero, 0f);
            PlayerController.Instance.playerStateList.recoilX = true;
        }
    }
    //stage 1
    IEnumerator TripleSlash()
    {
        attacking = true;
        rb.velocity = Vector3.zero;

        animator.SetTrigger("Slash");
        SlashAngle();
        yield return new WaitForSeconds(0.3f);
        animator.ResetTrigger("Slash");

        animator.SetTrigger("Slash");
        SlashAngle();
        yield return new WaitForSeconds(0.5f);
        animator.ResetTrigger("Slash");

        animator.SetTrigger("Slash");
        SlashAngle();
        yield return new WaitForSeconds(0.2f);
        animator.ResetTrigger("Slash");

        ResetAllAttacks();
    }
    void SlashAngle()
    {
        if(PlayerController.Instance.transform.position.x> transform.position.x||
            PlayerController.Instance.transform.position.x<transform.position.x)
        {
            Instantiate(slashEffect, sideAttackTransform);
        }
        if (PlayerController.Instance.transform.position.y > transform.position.y)
        {
            SlahEffectAtAngle(slashEffect, 90,upAttackTransform);
        }
        if (PlayerController.Instance.transform.position.y < transform.position.y)
        {
            SlahEffectAtAngle(slashEffect, -90,downAttackTransform);
        }
    }
    void SlahEffectAtAngle(GameObject _slashEffect,int _effectAngle, Transform _attackTransform)
    {
        _slashEffect = Instantiate(_slashEffect, _attackTransform);
        _slashEffect.transform.eulerAngles = new Vector3(0, 0, _effectAngle);
        _slashEffect.transform.localScale =new  Vector2(transform.localScale.x,transform.localScale.y);
    }
    IEnumerator Lunge()
    {
        Flip();
        attacking = true;
        animator.SetBool("Lunge", true);
        yield return new WaitForSeconds(1f);
        animator.SetBool("Lunge", false);
        damagedPlayer = false;
        rb.useGravity = true;
        ResetAllAttacks();
    }
    public IEnumerator Parry()
    {
        attacking= true;
        rb.velocity = Vector3.zero;
        animator.SetBool("Lunge", true);
        yield return new WaitForSeconds(0.8f);
        animator.SetBool("Lunge", false);
        parrying = false;
        ResetAllAttacks();
    }
    public IEnumerator Slash()
    {
        attacking = true;
        rb.velocity = Vector3.zero;

        animator.SetTrigger("Slash");
        SlashAngle();
        yield return new WaitForSeconds(0.2f);
        animator.ResetTrigger("Slash");
        ResetAllAttacks() ;
    }
}
