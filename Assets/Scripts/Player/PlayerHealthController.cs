using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamagable
{
    public Action<GameObject> OnDead;
    private PlayerController pController => GetComponent<PlayerController>();
    [SerializeField] private int _maxHp;
    [SerializeField] private GameObject DamageEffect;

    public delegate void OnHealthChangedDelegate();
    [HideInInspector] public OnHealthChangedDelegate OnHealthChangedCallBack;

    bool restoreTime;
    float restoreTimeSpeed;
    float healTimer;
    [SerializeField] private float timeToHeal;

    public int hp;
    public int df;
    public int health
    {
        get
        {
            return hp;
        }
        set
        { 
            hp =value;
            OnHealthChangedCallBack?.Invoke();
            if (health <= 0)
                {
                    OnDead?.Invoke(gameObject);
                    Destroy(gameObject);
                }

        }
    }
    public int defense
    {
        get { return df; }
        set 
        { 
            df = value;
        }
    }

    private void OnEnable()
    {
        hp=maxHealth;
        
    }
    public int maxHealth =>_maxHp;

    public bool isHeartHas = true;



    public void DamageMoment(int _damageDone, Vector2 _hitDirection, float _hitForce)
    {   if (defense >= 1) 
            df -= _damageDone;
        else
            health -= _damageDone;
    }
    public void TakeDamage(int _damage)
    {
        if (defense >= 1)
            df -= _damage;
        else
            health -= _damage;
        StartCoroutine(StopTakingDamage());
    }
    IEnumerator StopTakingDamage()
    {
        pController.playerStateList.invincible = true;
        GameObject  _damageEffect = Instantiate(DamageEffect,new Vector2(transform.position.x,transform.position.y+1.5f), Quaternion.identity);
        Destroy(_damageEffect, 1.5f);
        yield return new WaitForSeconds(1f);
        pController.playerStateList.invincible=false;
    }
    public void RestoreTimeScale()
    {
        if (restoreTime)
        {
            if (Time.timeScale < 1)
                Time.timeScale += Time.deltaTime * restoreTimeSpeed;
            else
            {
                Time.timeScale = 1;
                restoreTime = false;
            }
        }
    }
    public void HitStopTime(float _newTimeScale, int _restoreSpeed, float _delay)
    {
        restoreTimeSpeed = _restoreSpeed;
        Time.timeScale = _newTimeScale;
        if (_delay>0)
        {
            StopCoroutine(StartTimeAgain(_delay));
            StartCoroutine(StartTimeAgain(_delay));
        }
        else
        {
            restoreTime = true;
        }
    }
    IEnumerator StartTimeAgain(float _delay)
    {
        restoreTime = true;
        yield return new WaitForSeconds(_delay);
    }

   public void Heal()
    {
        if (Input.GetButton("Healing") && health<maxHealth && !pController.playerStateList.jumping && !pController.playerStateList.dashing)
        {
            pController.playerStateList.healing = true;
            healTimer += Time.deltaTime;
            if (healTimer>=timeToHeal)
            {
                health++;
                healTimer = 0;
            }
        }
        else
        {
            pController.playerStateList.healing=false;
            healTimer = 0;
        }
    }
}
