using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    // Update is called once per frame
    void Update() 
    {
        if (Keyboard.current.anyKey.isPressed)
        {
            SceneManager.LoadScene("Level1");
        }
    }
}
