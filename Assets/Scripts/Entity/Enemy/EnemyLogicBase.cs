using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyLogicBase : MonoBehaviour
{
     protected float timer;
    public EnemyHealthController enemyHealth =>GetComponent<EnemyHealthController>();
    [HideInInspector] public Rigidbody rb;
    public Animator animator;

    [SerializeField] protected float speed;
    protected void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator =GetComponent<Animator>();
    }
    protected  virtual void Update()
    {
        UpdateEnemyStates();
    }
    public enum EnemyStates
    {
        //крадущийся чел
        Crawler_Idle,
        Crawler_Flip,
        //Летающий чел
        FlyingEn_Idle,
        FlyingEn_Chase,
        FlyingEn_Stunned,
        FlyingEn_Death,
        //преследующий
        Charger_Idle,
        Charger_Suprised,
        Charger_Charge,
        //убегающий
        Coward_Idle,
        Coward_Suprised,
        Coward_Run,
        //boss1
        Boss1_Stage1,
        Boss1_Stage2,
        Boss1_Stage3,
        Boss1_Stage4,


    }
    public EnemyStates currectEnemyState;
    public EnemyStates GetCurrectEnemyState
    {
        get { return currectEnemyState; }
        set
        {
            if (currectEnemyState != value)
            {
                currectEnemyState = value;
                ChangeCurrentAnimation();
            }

        }
    }
    public virtual void UpdateEnemyStates(){ }
    protected virtual void ChangeCurrentAnimation() { }
    public virtual void ChangeState(EnemyStates state)
    {
        GetCurrectEnemyState = state;
    }
}
