using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartEnemyLogic : CowardEnemyLogic
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

        Instantiate(itemHeart, transform.position, Quaternion.identity);
    }
}
