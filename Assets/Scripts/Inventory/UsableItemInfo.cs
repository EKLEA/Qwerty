using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryUsableItemInfo", menuName = "Gameplay/Items/Create new UsableItemInfo")]
public class UsableItemInfo : ScriptableObject, IUsableItemInfo
{
    [SerializeField] private int _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _range;

    public int damage => _damage;

    public float coolDown => _cooldown;

    public float range => _range;
}
