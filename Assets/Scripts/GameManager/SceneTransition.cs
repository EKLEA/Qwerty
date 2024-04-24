using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SceneTransition : MonoBehaviour
{

    [SerializeField] string transitionTo;
    [SerializeField] Transform startPoint;
    [SerializeField] Vector2 exitDirection;
    [SerializeField] float exitTime;
    private void Start()
    {
        //тут делать по поводу сцене ттранзитиона
        if(transitionTo== GameManager.Instance.transitionedFromScene )
        {
            PlayerController.Instance.transform.position = startPoint.position;
            FollowPlayer.Instance.transform.position =  new Vector3(startPoint.position.x, startPoint.position.y, startPoint.position.z - 20.75f);
            StartCoroutine(PlayerController.Instance.moveHandler.WalkIntoNewScene(exitDirection,exitTime));
        }
        StartCoroutine(UIController.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Time.timeScale = 1;
            GameManager.Instance.transitionedFromScene = SceneManager.GetActiveScene().name;
            PlayerController.Instance.playerStateList.cutscene = true;
            StartCoroutine(UIController.Instance.sceneFader.FadeAndLoadScene(SceneFader.FadeDirection.In, transitionTo));
        }
    }
}
