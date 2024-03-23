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
    public bool cutscene = false;
    public bool alive = true;
    public bool interactedWithCheckPoint = false;
    public bool invOpened = false;

    public int health;
    public float energy;
    public bool isHeartHas = true;
}
