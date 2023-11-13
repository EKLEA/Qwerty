using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    [SerializeField] public GameObject _interface;
    private void Awake()
    {
        _interface.SetActive(false);
    }
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
            _interface.SetActive(true);
        if (Input.GetKeyUp(KeyCode.I))
            _interface.SetActive(false);
    }
}
