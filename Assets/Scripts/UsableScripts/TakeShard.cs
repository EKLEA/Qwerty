using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeShard : ExampleUsable
{
    public int _tempEN;
    public int _tempHP;
    public override void UseMoment()
    {
        
        if (_tempEN > 0)
        {
            PlayerController.Instance.playerLevelList.tempAddEN += _tempEN;
           StartCoroutine( PlayerController.Instance.TakeShard(false));// ��
        }
        else if (_tempHP > 0)
        {
            PlayerController.Instance.playerLevelList.tempAddHP += _tempHP;
            StartCoroutine( PlayerController.Instance.TakeShard(true));//�������

        }
        else
            Debug.Log("������� ��������� �������");
        playerUseMoment.OnUsedEvent -= Cheker;
        Destroy(gameObject);
        //�������� ��������
    }
}
