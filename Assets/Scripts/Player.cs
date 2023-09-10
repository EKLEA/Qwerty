using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private CharacterController controller;
    private Vector2 playerVelocity;
    private bool groundedPlayer;
    private bool doubleJump = true;
    public float playerSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravityValue = -50f;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        controller.minMoveDistance = 0;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;

        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
            doubleJump = true;
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            Debug.Log(doubleJump);
        }

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
        controller.Move(move * Time.deltaTime * playerSpeed);
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)

        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -3.0f * gravityValue); ;

        }

        if (Input.GetKeyDown(KeyCode.Space) && doubleJump && !groundedPlayer)
        {
            playerVelocity.y = Mathf.Sqrt(jumpHeight * -3.0f * gravityValue);
            doubleJump = false;
        }

        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
}