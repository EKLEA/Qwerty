using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class ExampleUseMoment : MonoBehaviour
{
    
    public delegate void OnSimpleAttackEventDelegate();
    [HideInInspector] public OnSimpleAttackEventDelegate OnSimpleAttackEventCallBack;
    public delegate void OnSpecialAttackEventDelegate();
    [HideInInspector] public OnSpecialAttackEventDelegate OnSpecialAttackEventCallBack;
    public delegate void OnCastSpellEvenDelegate();
    [HideInInspector] public OnSpecialAttackEventDelegate OnCastSpellEventCallBack;

}
