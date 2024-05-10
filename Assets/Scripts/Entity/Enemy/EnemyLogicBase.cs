using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Animator))]
public class EnemyLogicBase : MonoBehaviour
{
     protected float timer;
   protected EnemyHealthController enemyHealth=> GetComponent<EnemyHealthController>();
    protected Rigidbody rb => GetComponent<Rigidbody>();
    public Animator animator => GetComponent<Animator>();
    [HideInInspector] protected PlayerController playerController => PlayerController.Instance;

    [SerializeField] protected float speed;
    protected  virtual void Update()
    {
        UpdateEnemyStates();
    }
    protected enum EnemyStates
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
    protected EnemyStates currectEnemyState;
    protected EnemyStates GetCurrectEnemyState
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
    protected virtual void ChangeState(EnemyStates state)
    {
        GetCurrectEnemyState = state;
    }
}
