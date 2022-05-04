using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallController : MonoBehaviour
{
    void Start()
    {
       // SetTransparent(true);
    }

    public void SetTransparent(bool isTransparent)
    {
        var color = GetComponent<Renderer> ().material.color;
        if (isTransparent) {
            color.a = 0.5f;
        } else {
            color.a = 1f;
        }
    }
}
