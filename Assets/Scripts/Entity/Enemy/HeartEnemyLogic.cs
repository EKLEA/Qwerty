using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEnemyLogic : EnemyLogicBase
{

    [SerializeField] GameObject itemHeart;
    public static HeartEnemyLogic Instance;
    private void Awake()
    {
        if(Instance!=null && Instance!=this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }

        SaveData.Instance.SaveHeartEnemy();
    }
    private void Start()
    {
        ChangeState(EnemyStates.Coward_Idle);

        gameObject.GetComponent<EnemyHealthController>().OnDeadCallBack += CheckDeath;
    }
    void CheckDeath()
    {
        gameObject.GetComponent<EnemyHealthController>().OnDeadCallBack -= CheckDeath;
        Instantiate(itemHeart, transform.position, Quaternion.identity);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GetComponent<EnemyHealthController>().DamageMoment(1000, Vector2.zero, 0f);
        }
    }
}
