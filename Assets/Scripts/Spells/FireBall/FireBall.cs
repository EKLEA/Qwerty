using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    [SerializeField] private int damage;
    [SerializeField] private float hitForce;
    [SerializeField] private int speed;
    [SerializeField] private float lifeTime;
    private void Start()
    {
        if (PlayerController.Instance.playerLevelList.levelTier == 2)
        {
            damage *= 2;
            // доп анимция
        }
        Destroy(gameObject, lifeTime);
    }
    private void FixedUpdate()
    {
        transform.position += speed * transform.right;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag ( "Enemy"))
        {
            other.GetComponent<EnemyHealthController>().DamageMoment(damage, (other.transform.position - transform.position).normalized, -hitForce);
            Debug.Log(other.GetComponent<EnemyHealthController>().health);
        }
    }
}
