using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateList : MonoBehaviour
{
    public Vector2 Axis;
    public bool jumping = false;
    public bool dashing = false;
    public bool grounded = true;
    public bool recoilX,recoilY;
    public bool lookRight;
    public bool invincible= false;
    public bool healing = false;
    public bool casting = false;

    public int health;
    public float energy;
}
