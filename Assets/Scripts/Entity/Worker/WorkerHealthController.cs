using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorkerHealthController : EnemyHealthController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    protected override void Awake()
    {
        base.Awake();
    }
    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        //rb.AddForce(Physics.gravity * 16f * Time.fixedDeltaTime, ForceMode.Acceleration);
        if (!isRecoiling)
        {
            transform.position = Vector3.MoveTowards
                (transform.position, new Vector3(playerController.transform.position.x, playerController.transform.position.y,playerController.transform.position.z), speed * Time.deltaTime);
        }
    }
    public override void DamageMoment(int _damageDone, Vector2 _hitDirection, float _hitForce)
    {
        base.DamageMoment(_damageDone,_hitDirection,_hitForce);
    }
}