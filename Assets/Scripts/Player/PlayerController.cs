using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public bool isJumping = false;
    bool doubleJump = true;
    public float playerSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public Animator anim;
    [SerializeField] private IMoveHandler moveHandler;
    public BoxCollider c=> gameObject.GetComponent<BoxCollider>();


    public Vector2 Axis;


    float xAxis, yAxis;


    private void Awake()
    {
        anim = GetComponent<Animator>();
        moveHandler = GetComponent<IMoveHandler>();
        moveHandler.SetValues(playerSpeed, jumpHeight);
    }
    void Update()
    {

        var xAxis = Input.GetAxisRaw("Horizontal");
        var yAxis = Input.GetAxisRaw("Vertical");
        Axis = new Vector2(xAxis, yAxis);
        if (xAxis < 0)
            gameObject.transform.eulerAngles = new Vector3(0, -90, 0);
        if (xAxis > 0)
            gameObject.transform.eulerAngles = new Vector3(0, 90, 0);


        moveHandler.Move(xAxis);
        moveHandler.JumpMoment();
        if (moveHandler.Grounded()==false)
        {
            anim.SetBool("Falling", true);
            c.size = new Vector3(1.5f, 4, 1.5f);
        }
        if(moveHandler.Grounded() == true)
        {
            anim.SetBool("Falling", false);
            anim.SetBool("FallingDown", true);
            c.size = new Vector3(1.5f, 6, 1.5f);
        }


    }
    public void AttackToogle()
    {
        anim.SetBool("Attacking", false);
    }



}
