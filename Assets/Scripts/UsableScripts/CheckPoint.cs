using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckPoint : ExampleUsable
{
    public bool interacted;
    public override void UseMoment()
    {
        interacted = true;
        StartCoroutine(PlayerController.Instance.EnterInCheckPoint());

        SaveData.Instance.checkPointSceneName=SceneManager.GetActiveScene().name; 

        SaveData.Instance.checkPointPos=new Vector3(gameObject.transform.position.x,
            gameObject.transform.position.y,
            gameObject.transform.position.z);
        SaveData.Instance.SaveCheckPoint();
    }
    private new void OnTriggerExit(Collider other)
    {
        interacted = false;
        StartCoroutine(PlayerController.Instance.ExitFromCheckPoint());
        if (other.gameObject.tag == "Player")
            playerUseMoment.OnUsedEvent -= Cheker;
    }
}
