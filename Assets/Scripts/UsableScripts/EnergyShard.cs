using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyShard : ExampleUsable
{
    public override void UseMoment()
    {
        //�������� ��������
        PlayerController.Instance.playerLevelList.tempAddEN += 1;

    }
}
