using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "InventoryUsableItemInfo", menuName = "Gameplay/Items/Create new UsableItemInfo")]
public class UsableItemInfo : ScriptableObject, IUsableItemInfo
{
    [SerializeField] private float _damage;
    [SerializeField] private float _cooldown;
    [SerializeField] private float _range;

    public float damage => _damage;

    public float coolDown => _cooldown;

    public float range => _range;
}
