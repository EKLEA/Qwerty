using UnityEngine;

public class FollowPlayer : MonoBehaviour
{
    [SerializeField] private Transform lookAt;
    [SerializeField] private Vector2 bound = new Vector2(0.15f, 0.05f);
    [SerializeField] private float speed = 5.0f;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(new Vector3(transform.position.x, transform.position.y, transform.position.z + 17), bound);
        Gizmos.color = Color.red;
        Gizmos.DrawLine(new Vector3(transform.position.x, transform.position.y, transform.position.z + 17), new Vector3(transform.position.x + bound.x, transform.position.y, transform.position.z + 17));
    }
    private void FixedUpdate()
    {
        float DX = 0, DY = 0;
        if ((lookAt.position.x < transform.position.x + bound.x / 2) && (lookAt.position.x > transform.position.x - bound.x / 2))
        {
            DX = 0;
            DY = 0;
        }
        if ((lookAt.position.x > transform.position.x + bound.x / 2) || (lookAt.position.x < transform.position.x - bound.x / 2))
            DX = lookAt.position.x - transform.position.x;
        if ((lookAt.position.y > transform.position.y + bound.y / 2) || (lookAt.position.y < transform.position.y - bound.y / 2))
            DY =3+ lookAt.position.y - transform.position.y;


        Vector3 targetPosition = new Vector3(DX, DY, 0);
        transform.position = Vector3.Lerp(transform.position, transform.position + targetPosition, speed * Time.deltaTime);
    }
}