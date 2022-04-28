using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour {

    public float speed = 0;

    private Rigidbody rigidBody;
    private float movementX;
    private float movementY;

    // Start is called before the first frame update
    void Start() {
        rigidBody = GetComponent<Rigidbody>();
        Debug.Log("Hello: " + gameObject.name);
    }

    public void FixedUpdate() {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rigidBody.AddForce(movement * speed);
    }

    public void OnMove(InputValue movementValue) {
        Vector2 movementVector = movementValue.Get<Vector2>();
        Debug.Log("On Move: " + movementVector);
        movementX = movementVector.x;
        movementY = movementVector.y;
    }
}
