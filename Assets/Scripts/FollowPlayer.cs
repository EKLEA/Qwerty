using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector2 bound = new Vector2(0.15f, 0.05f);

    private void LateUpdate()
    {
        Vector3 delta = Vector3.zero;

        float deltaX = lookAt.position.x - transform.position.x;
        if (deltaX > bound.x || deltaX < -bound.x)
        {
            if (transform.position.x < lookAt.position.x)
            {
                delta.x = deltaX - bound.x;
            }
            else
            {
                delta.x = deltaX + bound.x;
            }
        }

        float deltaY = lookAt.position.y - transform.position.y;
        if (deltaY > bound.y || deltaY < -bound.y)
        {
            if (transform.position.y < lookAt.position.y)
            {
                delta.y = deltaY - bound.y;
            }
            else
            {
                delta.y = deltaY + bound.y;
            }
        }

        transform.position += new Vector3(delta.x, deltaY, 0);
    }
}
