using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private bool groundedPlayer;
    private bool doubleJump = true;
    public float playerSpeed = 5.0f;
    public float jumpHeight = 2.0f;
    public float gravityValue = -50f;
    [SerializeField] private IMoveHandler moveHandler;
    private void Awake()
    {
        controller = gameObject.GetComponent<CharacterController>();
        moveHandler = gameObject.GetComponent<IMoveHandler>();
        controller.minMoveDistance = 0;
        moveHandler.SetValues(playerSpeed,gravityValue,jumpHeight,controller);
    }
    void Update()
    {

        Vector2 move = new Vector2(Input.GetAxis("Horizontal"), 0);
        moveHandler.Move(move);
        groundedPlayer = controller.isGrounded;
        if (Input.GetKeyDown(KeyCode.Space) && groundedPlayer)
            moveHandler.JumpMoment();

        if (Input.GetKeyDown(KeyCode.Space) && doubleJump && !groundedPlayer)
        {
            moveHandler.JumpMoment();
            doubleJump = false;
        }
    }
}
