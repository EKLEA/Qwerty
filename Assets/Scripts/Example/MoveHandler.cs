using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public abstract class MoveHandler : MonoBehaviour
{
    [Header("Speed Settings")]
    [SerializeField] protected  float speed;
    [Header("Jump Settings")]
    [SerializeField] protected float jumpHeight;
    protected virtual void Move() { }
    protected virtual void JumpMoment() { }
    protected virtual void UpdateJumpVar() { }
    protected virtual bool Grounded() { return true; }
    protected Rigidbody rb => gameObject.GetComponent<Rigidbody>();
}
