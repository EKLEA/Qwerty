using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEngine;
public class Boss1 : EnemyLogicBase
{
    public static Boss1 Instance;
    public new Boss1HealthController enemyHealth =>GetComponent<Boss1HealthController>();
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

    [HideInInspector] public int hitCounter;
    [HideInInspector] public bool stunned, canStun;
    [HideInInspector] public bool alive;
    [HideInInspector] public float runSpeed;
    [HideInInspector] public bool facingRight;


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

    protected new void Start()
    {
        base.Start();
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
        if (stunned)
        {
            rb.velocity = Vector3.zero;
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
                        canStun = true;
                        attackTimer = 6;
                        runSpeed = speed;
                        break;
                    }
                case EnemyStates.Boss1_Stage2:
                    {
                        canStun = true;
                        attackTimer = 5;
                        break;
                    }
                case EnemyStates.Boss1_Stage3:
                    {
                        canStun = false;
                        attackTimer = 8;
                        break;
                    }
                case EnemyStates.Boss1_Stage4:
                    {
                        canStun = false;
                        attackTimer = 10;
                        runSpeed = speed / 2;
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
    [HideInInspector] public bool outbreakAttack;

    [HideInInspector] public bool bounceAttack;
    [HideInInspector] public float rotationDirectionToTarget;
    [HideInInspector] public int bounceCount;


    
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
        if(currectEnemyState== EnemyStates.Boss1_Stage2)
        {
            if (Vector3.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(TripleSlash());
            }
            else
            {
                float _attackChosen = UnityEngine.Random.Range(1, 3);
                switch (_attackChosen)
                {
                    case 1:
                        {
                            StartCoroutine(Lunge());
                            break;
                        }
                    case 2:
                        {
                            DiveAttackJump();
                            break;
                        }
                    case 3:
                        {

                            BarrageBendDown();
                            break;
                        }
                }
            }
        }
        if (currectEnemyState == EnemyStates.Boss1_Stage3)
        {
            float _attackChosen = UnityEngine.Random.Range(1, 4);
            switch (_attackChosen)
            {
                case 1:
                    {
                        OutbreakBendDown();
                        break;
                    }
                case 2:
                    {
                        DiveAttackJump();
                        break;
                    }
                case 3:
                    {

                        BarrageBendDown();
                        break;
                    }
                case 4:
                    {

                        BounceAttack();
                        break;
                    }
            }
        }
        if (currectEnemyState == EnemyStates.Boss1_Stage4)
        {
            if (Vector3.Distance(PlayerController.Instance.transform.position, rb.position) <= attackRange)
            {
                StartCoroutine(Slash());
            }
            else
            {
                BounceAttack();
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
        outbreakAttack= false;
        bounceAttack = false;
    }
    // stage 3
    void OutbreakBendDown()
    {
        attacking = true;
        rb.velocity=Vector3.zero;
        moveToPos = new Vector3(transform.position.x, rb.position.y, transform.position.x);
        outbreakAttack=true;
        animator.SetTrigger("BendDown");
       
    }
    public IEnumerator Outbreak()
    {
        yield return new WaitForSeconds(1f);
        animator.SetBool("Cast", true);
        rb.velocity = Vector3.zero;
        for (int i = 0; i < 30; i++)
        {
            Instantiate(barrageFireBall, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(110, 130)));//down
            Instantiate(barrageFireBall, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(50, 70)));//dioganal Right
            Instantiate(barrageFireBall, transform.position, Quaternion.Euler(0, 0, UnityEngine.Random.Range(260, 280))); // Diogamal left
            yield return new WaitForSeconds(0.2f);
        }
        yield return new WaitForSeconds(0.1f);
        rb.constraints = RigidbodyConstraints.FreezePositionZ;
        rb.constraints = RigidbodyConstraints.FreezeRotation;


        rb.velocity = new Vector3(rb.velocity.x, -10, 0f);
         yield return new WaitForSeconds(0.1f);
        animator.SetBool("Cast", false);
        ResetAllAttacks();



    }
    void BounceAttack()
    {
        attacking = true;
        bounceCount = UnityEngine.Random.Range(2, 5);
        BounceBendDown();
    }
    int _bounces = 0;
    public void CheckBounce()
    {
        if (_bounces < bounceCount-1)
        {
            _bounces++;
            BounceBendDown();
        }
        else
        {
            _bounces = 0;
            animator.Play("Boss_Run");
        }
    }
    public void BounceBendDown()
    {
        rb.velocity = Vector3.zero;
        moveToPos = new Vector3(PlayerController.Instance.transform.position.x, rb.position.y + 10, PlayerController.Instance.transform.position.z);
        bounceAttack = true;
        animator.SetTrigger("BendDown");
    
    
    }
    public void CalculateTargetAngle()
    {
        Vector3 _directionToTarget = (PlayerController.Instance.transform.position - transform.position).normalized;
        float _angleOfTarget = Mathf.Atan2(_directionToTarget.y, _directionToTarget.x) * Mathf.Rad2Deg;
        rotationDirectionToTarget = _angleOfTarget;





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

    public IEnumerator Stunned()
    {
        stunned = true;
        hitCounter = 0;
        animator.SetBool("Stunned", true);

        yield return new WaitForSeconds(6f);
        animator.SetBool("Stunned", false) ;
        stunned = false;
    }
}
