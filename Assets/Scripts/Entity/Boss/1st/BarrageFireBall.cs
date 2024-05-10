using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarrageFireBall : MonoBehaviour
{
    [SerializeField] Vector2 startForceMinMax;
    [SerializeField] float turnSpeed = 0.5f;
    Rigidbody rb;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        Destroy(gameObject,4f);
        rb.AddForce(transform.right*Random.Range(startForceMinMax.x,startForceMinMax.y));

    }
    private void Update()
    {
        var _dir = rb.velocity;
        if (_dir != Vector3.zero)
        {
            Vector3 _frontVector = Vector3.right;
            Quaternion _targetRotation = Quaternion.FromToRotation(_frontVector, _dir - (Vector3)transform.position);
            if (_dir.x > 0 )
            {
                transform.rotation=Quaternion.Lerp(transform.rotation, _targetRotation, turnSpeed);
                transform.eulerAngles = new Vector3(transform.eulerAngles.x, 180, transform.eulerAngles.z);
            }
            else
            {
                transform.rotation = Quaternion.Lerp(transform.rotation, _targetRotation, turnSpeed);
            }
        
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerController>().playerHealthController.DamageMoment(Boss1.Instance.damage, Vector3.zero, 0f);
            Destroy(gameObject);
                }
    }
}
