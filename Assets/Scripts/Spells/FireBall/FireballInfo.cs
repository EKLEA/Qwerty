using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(fileName = "FireBallInfo", menuName = "Gameplay/Items/Create new FireBallInfo")]
public class FireballInfo : ScriptableObject
{
    [SerializeField] private int _damage;
    [SerializeField] private float _hitForce;
    [SerializeField] private int _speed;
    [SerializeField] private float _lifeTime;


    public int damage => _damage;

    public float hitForce => _hitForce;
    public int speed => _speed;
    public float lifeTime => _lifeTime;
}
