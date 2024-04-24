using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spike : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(RespawnPoint());

    }
    IEnumerator RespawnPoint()
    {
        PlayerController.Instance.playerStateList.cutscene = true;
        PlayerController.Instance.playerStateList.invincible = true;
        PlayerController.Instance.rb.velocity = Vector3.zero;
        Time.timeScale = 0;
        StartCoroutine(UIController.Instance.sceneFader.Fade(SceneFader.FadeDirection.In));
        PlayerController.Instance.playerHealthController.DamageMoment(1, Vector2.zero, 0f);
        yield return new WaitForSeconds(1);
        PlayerController.Instance.transform.position = GameManager.Instance.platformingRespawnPoint;
        StartCoroutine(UIController.Instance.sceneFader.Fade(SceneFader.FadeDirection.Out));
        yield return new WaitForSeconds(UIController.Instance.sceneFader.fadeTime);
        PlayerController.Instance.playerStateList.cutscene = false;

        PlayerController.Instance.playerStateList.invincible = false;
        Time.timeScale = 1;


    }
}
