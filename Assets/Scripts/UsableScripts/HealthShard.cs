using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthShard : ExampleUsable
{
    public override void UseMoment()
    {
        //�������� ��������
        PlayerController.Instance.playerLevelList.tempAddHP += 1;

    }
}
