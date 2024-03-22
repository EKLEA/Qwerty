using System.Collections;
using System.Collections.Generic;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class SceneFader : MonoBehaviour
{
    [SerializeField] float fadeTime;
    Image fadeOutUIImage;
    public enum FadeDirection
    {
        In,
        Out
    }
    void Awake()
    {
        fadeOutUIImage=GetComponent<Image>();
    }
    public IEnumerator Fade(FadeDirection _fadeDirection)
    {
        float _alpha = _fadeDirection == FadeDirection.Out ? 1 : 0;
        float _fadeEndValue = _fadeDirection == FadeDirection.Out ? 0 : 1;
        if (_fadeDirection == FadeDirection.Out)
        {
            while (_alpha >= _fadeEndValue)
            {
                SetColorImage(ref _alpha, _fadeDirection);
                yield return null;
            }
            fadeOutUIImage.enabled = false;
        }
        else
        {
            fadeOutUIImage.enabled = true;
            while (_alpha <= _fadeEndValue)
            {
                SetColorImage(ref _alpha, _fadeDirection);
                yield return null;
            }
            
        }

    }
    public IEnumerator FadeAndLoadScene(FadeDirection _fadeDirection,string _sceneToLoad)
    {
        fadeOutUIImage.enabled=true;
        yield return Fade(_fadeDirection);
        SceneManager.LoadScene(_sceneToLoad);
    }
    void SetColorImage(ref float _alpha,FadeDirection _fadeDirection)
    {
        fadeOutUIImage.color = new Color(fadeOutUIImage.color.r,
                                       fadeOutUIImage.color.g,
                                       fadeOutUIImage.color.b,
                                       _alpha);
        _alpha += Time.deltaTime * (1 / fadeTime)* (_fadeDirection==FadeDirection.Out? -1: 1);
    }
}
