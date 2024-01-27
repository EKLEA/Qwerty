using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamagable
{
    public Action<GameObject> OnDead;
    private PlayerController pController => GetComponent<PlayerController>();
    [SerializeField] private float _maxHp;
    [SerializeField] private float _maxDefense;
     public float hp;
     public float df;
    public float health
    {
        get
        {
            return hp;
        }
        set
        {
            hp= value;
            if (health <= 0)
            {
                OnDead?.Invoke(gameObject);
                Destroy(gameObject);
            }   
        }
    }
    public float defense
    {
        get { return df; }
        set { df = value; }
    }

    private void OnEnable()
    {
        hp=maxHealth; df=maxDefense;
    }
    public float maxHealth =>_maxHp;
    public float maxDefense => _maxDefense;
    public bool isHeartHas = true;



    public void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        
        if (defense > 0)
            defense -= _damageDone;
        else
            health -= _damageDone;
    }
    public void TakeDamage(float _damage)
    {
        if (defense <= 0)
            health -= _damage;
        else
            defense -= _damage;
        StartCoroutine(StopTakingDamage());
    }
    void ClampHealth()
    {
        health=Math.Clamp(health, 0.0f, maxHealth);
    }
    IEnumerator StopTakingDamage()
    {
        pController.playerStateList.invincible = true;
        ClampHealth();
        yield return new WaitForSeconds(1f);
        pController.playerStateList.invincible=false;
    }
}
