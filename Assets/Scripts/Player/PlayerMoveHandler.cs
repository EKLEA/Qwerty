using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveHandler : MonoBehaviour, IMoveHandler
{
    public CharacterController controller {  get; private set; }
    public float speed { get; private set; }
    public float jumpHeight { get; private set; }
    public float gravityValue { get; private set; }
    private Vector2 playerVelocity;

    public void Move(Vector2 vec)
    {
        controller.Move(vec * Time.deltaTime * speed);
        playerVelocity.y += gravityValue * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);
    }
    public void JumpMoment()
    {
        playerVelocity.y = jumpHeight*10;
    }
    public void SetValues(float _speed, float _gravity, float _jumpHeight, CharacterController _controller)
    {
        controller = _controller;
        speed = _speed;
        gravityValue = _gravity;
        jumpHeight = _jumpHeight;
    }
}
