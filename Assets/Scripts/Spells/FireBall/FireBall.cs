using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private FireballInfo fireballInfo;
    private void Start()
    {
        Destroy(gameObject, fireballInfo.lifeTime);
    }
    private void FixedUpdate()
    {
        transform.position += fireballInfo.speed * transform.right;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            other.GetComponent<EnemyHealthController>().DamageMoment(fireballInfo.damage, (other.transform.position - transform.position).normalized, -fireballInfo.hitForce);
        }
    }
}
