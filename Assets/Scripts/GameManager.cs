using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    InputManager _inputManager;

    private void Awake()
    {
        _inputManager = FindObjectOfType<InputManager>();
    }

    private void Start() 
    {
       //
    }
}
