using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss1HealthController : EnemyHealthController

{
    protected new void OnCollisionEnter(Collision other)
    {
        
    }
    public override void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        if (!Boss1.Instance.parrying)
        {
            base.DamageMoment(_damageDone, _hitDirection, _hitForce);
            Boss1.Instance. StartCoroutine(Boss1.Instance.Parry());
        }
        else
        {
            StopCoroutine(Boss1.Instance.Parry());
            Boss1.Instance.ResetAllAttacks();
            Boss1.Instance.StartCoroutine(Boss1.Instance.Slash());
        }
    }
}
