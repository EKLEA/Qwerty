using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyHealthController))]
public class EnemyLogicBase : MonoBehaviour
{
     protected float timer;
   protected EnemyHealthController enemyHealth=> GetComponent<EnemyHealthController>();
    protected Rigidbody rb => GetComponent<Rigidbody>();
    [SerializeField] protected Animator animator => GetComponent<Animator>();
    [HideInInspector] protected PlayerController playerController => GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
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
        Charger_Charge
       
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
