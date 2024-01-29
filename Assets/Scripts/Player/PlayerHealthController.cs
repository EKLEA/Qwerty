using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : MonoBehaviour, IDamagable
{
    public Action<GameObject> OnDead;
    private PlayerController pController => GetComponent<PlayerController>();
    [SerializeField] private float _maxHp;
    [SerializeField] private GameObject DamageEffect;

    bool restoreTime;
    float restoreTimeSpeed;


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
    public float defenseK
    {
        get { return df; }
        set 
        { 
            if (value < 1f && value > 2f)
                df =1;
            else 
                df = value;
        }
    }

    private void OnEnable()
    {
        hp=maxHealth;
    }
    public float maxHealth =>_maxHp;
    public bool isHeartHas = true;



    public void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
            health -= _damageDone *(1/defenseK);
    }
    public void TakeDamage(float _damage)
    {
        health -= _damage * (1 / defenseK);
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
}
