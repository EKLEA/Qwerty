using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

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
    private Animator anim => GetComponent<PlayerController>().anim;
    public void Move(float xAxis)
    {
        rb.velocity= new Vector2(xAxis*  speed, rb.velocity.y);

        anim.SetBool("Runing", rb.velocity.x != 0 && Grounded());

    }
    public void JumpMoment()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Grounded())
            rb.velocity = new Vector2(rb.velocity.x, jumpHeight);
        if (Input.GetKeyUp(KeyCode.Space) && rb.velocity.y > 0)
            rb.velocity = new Vector2(rb.velocity.x, 0);
        anim.SetBool("Jumping", !Grounded());


    }
    public void SetValues(float _speed, float _jumpHeight)
    {
        speed = _speed;
        jumpHeight = _jumpHeight;
    }
    public bool Grounded()
    {
        return (Physics.Raycast(groundCheckPoint.position, Vector2.down, groundCheckY , whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(groundCheckX, 0, 0), Vector2.down, groundCheckY , whatIsGround) ||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX/2, 0, 0), Vector2.down, groundCheckY , whatIsGround)||
            Physics.Raycast(groundCheckPoint.position + new Vector3(-groundCheckX/2, 0, 0), Vector2.down, groundCheckY, whatIsGround) );
           
    }
}
