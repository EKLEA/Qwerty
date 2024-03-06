using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(EnemyHealthController))]
public class EnemyLogicBase : MonoBehaviour
{
    protected float timer;
    protected EnemyHealthController enemyHealth=> GetComponent<EnemyHealthController>();
    protected Rigidbody rb => GetComponent<Rigidbody>();
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
       
    }
    protected EnemyStates currectEnemyState;
    public virtual void UpdateEnemyStates()
    {

    }
    protected virtual void ChangeState(EnemyStates state)
    {
        currectEnemyState = state;
    }
}
