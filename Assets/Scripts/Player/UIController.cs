using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] Camera _camera;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            _camera.depth = -2;
        if (Input.GetKeyUp(KeyCode.I))
            _camera.depth = 0;
    }
}
