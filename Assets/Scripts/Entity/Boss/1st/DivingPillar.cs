using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DivingPillar : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.gameObject.GetComponent<PlayerController>().playerHealthController.DamageMoment(Boss1.Instance.damage,Vector2.zero,0f);
        }
    }
}
