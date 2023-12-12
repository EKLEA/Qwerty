using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Rigidbody))]
public class PlayerMoveHandler : MonoBehaviour, IMoveHandler
{
    public float speed { get; private set; }
    public float jumpHeight { get; private set; }
    [SerializeField] public Transform groundCheckPoint;
    [SerializeField] public float groundCheckY = 0.2f;
    [SerializeField] public float groundCheckX = 0.5f;
    [SerializeField] public LayerMask whatIsGround;


    public Rigidbody rb => GetComponent<Rigidbody>();

    public void Move(float xAxis)
    {
        rb.velocity= new Vector2(xAxis*  speed, rb.velocity.y);
    }
    public void JumpMoment(bool down)
    {
        if (down)
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        else
            rb.velocity = new Vector2(rb.velocity.x,0);
    }
    public void SetValues(float _speed, float _jumpHeight)
    {
        speed = _speed;
        jumpHeight = _jumpHeight;
    }
    public bool Grounded()
    {
        if (Physics2D.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY, whatIsGround) ||
            Physics2D.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround) ||
            Physics2D.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX, 0, 0), Vector2.down, groundCheckY, whatIsGround))
            return true;
        else
            return false;
    }
}
