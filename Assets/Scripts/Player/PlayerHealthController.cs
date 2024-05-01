using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealthController : DamagableObjWithLogic
{
    private PlayerController pController => GetComponent<PlayerController>();
    
    [SerializeField] private GameObject DamageEffect;
    bool restoreTime;
    float restoreTimeSpeed;
    float healTimer;
    float restoreEnergyTimer;

    [SerializeField] private float timeToHeal;
    [SerializeField] private float restoreTimeEnergy;
    [SerializeField] float energyDrainSpeed;
    [SerializeField] public float energyGain;
     public bool heartHas;
    public bool isHeartHas
    {
        get {  return heartHas; }
        set 
        { 
            heartHas = value;
        }
    }

    public float resHealth;
    public float resEnergy;

    public Action<object> OnHealthVarChange;
    public void InitPlayerHealth()
    {
        UpdateHealthVar();
        hp = resHealth;
        en = resEnergy;
    }
    private void UpdateHealthVar()
    {
        resHealth = maxHealth + PlayerController.Instance.playerLevelList.addHealth;
        resEnergy = maxEnergy + PlayerController.Instance.playerLevelList.addEnergy;
       
    }
    public void IncreaseMaxHealth(int var)
    {
        maxHealth += var;
        UpdateHealthVar() ;
        OnHealthVarChange.Invoke(null);
    }
    public void IncreaseMaxEnergy(int var)
    {
        maxEnergy+=var;
        UpdateHealthVar();
        OnHealthVarChange.Invoke(null);
    }
    public new float health
    {
        get
        {
            return hp;
        }
        set
        {
            if (defense > 0)
                defense -= value;
            else
                hp = value;
            OnHealthChangedCallBack?.Invoke();
            if (health <= 0)
            {
                StartCoroutine(Death());
            }
            else
            {
                StartCoroutine(StopTakingDamage());
            }

        }
    }
    public void MoveWithoutHeart()
    {
        if (!isHeartHas)
        {
            if (pController.rb.velocity.x == 0 && pController.playerStateList.grounded)
            {
                restoreEnergyTimer += Time.deltaTime;
                if (restoreEnergyTimer >= restoreTimeEnergy)
                {
                    energy += Time.deltaTime * energyDrainSpeed / 2;
                }
            }
            else if (pController.rb.velocity.x == 0 && !pController.playerStateList.grounded)
                return;
            else
            {
                energy -= Time.deltaTime * energyDrainSpeed / 2;
                if (energy <= 0) PlayerController.Instance.rb.velocity = new Vector3(0, PlayerController.Instance.rb.velocity.y, 0);
            }
        }
    }
    public override void DamageMoment(float _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        if (pController.playerStateList.alive)
            health -= _damageDone;

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
        
        if (_delay>0)
        {
            StopCoroutine(StartTimeAgain(_delay));
            StartCoroutine(StartTimeAgain(_delay));
        }
        else
        {
            restoreTime = true;
        }
        Time.timeScale = _newTimeScale;
    }
    IEnumerator Death()
    {
        OnDeadCallBack?.Invoke();
        pController.playerStateList.alive=false;
        Time.timeScale = 1f;
        GameObject _damageEffect = Instantiate(DamageEffect, new Vector2(transform.position.x, transform.position.y + 1.5f), Quaternion.identity);
        Destroy(_damageEffect, 1.5f);
        pController.anim.SetTrigger("Death");

        yield return new WaitForSeconds(0.9f);

        isHeartHas = false;
        StartCoroutine(UIController.Instance.ActivateDeathScreen());
        yield return new WaitForSeconds(0.9f);
        
        Instantiate(GameManager.Instance.heartEnemy,transform.position, Quaternion.identity);
        UIController.Instance.uiHud.GetComponent<UIHud>().InitHud();

    }


    IEnumerator StartTimeAgain(float _delay)
    {
        restoreTime = true;
        yield return new WaitForSeconds(_delay);
    }

   public void Heal()
   {
        if (Input.GetButton("Cast/Heal") && pController.castOrHealTimer > 0.05f&& health<resHealth && energy>0 && !pController.playerStateList.jumping && !pController.playerStateList.dashing&& pController.rb.velocity== Vector3.zero)
        {
            pController.playerStateList.healing = true;
            healTimer += Time.deltaTime;
            if (healTimer>=timeToHeal)
            {
                health++;
                healTimer = 0;
            }
            
            energy -= Time.deltaTime * energyDrainSpeed;
        }
        else
        {
            pController.playerStateList.healing=false;
            healTimer = 0;
        }
    }
}
