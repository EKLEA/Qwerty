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
    [SerializeField] private IMoveHandler moveHandler;



    public Vector2 Axis;


    float xAxis, yAxis;


    private void Awake()
    {
        moveHandler = GetComponent<IMoveHandler>();
        moveHandler.SetValues(playerSpeed, jumpHeight);
    }
    void Update()
    {

        var xAxis = Input.GetAxisRaw("Horizontal");
        var yAxis = Input.GetAxisRaw("Vertical");
        Axis = new Vector2(xAxis, yAxis);
        if (xAxis < 0)
            gameObject.transform.eulerAngles = new Vector3(0, 180, 0);
        if (xAxis > 0)
            gameObject.transform.eulerAngles = new Vector3(0, 0, 0);


        moveHandler.Move(xAxis);



        Debug.Log(moveHandler.Grounded());

        if (Input.GetKeyDown(KeyCode.Space) && moveHandler.Grounded())
        {
            moveHandler.rb.velocity = new Vector2(moveHandler.rb.velocity.x, jumpHeight);
        }
        if (moveHandler.Grounded())
        {
            doubleJump = true;
        }
    }
    
   
}
