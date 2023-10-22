using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    
    private CharacterController controller;
    private Vector2 playerVelocity;
    private bool groundedPlayer;
    private bool doubleJump = true;
    public float playerSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravityValue = -50f;
    public bool isUsable;
    private IsUsable usableObj;
    

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.minMoveDistance = 0;
    }

    void FixedUpdate()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            doubleJump = true;
        }
        
        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
        controller.Move(move * Time.deltaTime * playerSpeed);


        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);

        if (Input.GetKeyDown(KeyCode.Space) && doubleJump && !groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            doubleJump = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
        if (Input.GetKeyDown(KeyCode.E))
            usableObj.UseMoment();
       
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Usable")
            usableObj = other.GetComponent<IsUsable>();

    }
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Usable")
            usableObj = null;

    }


}