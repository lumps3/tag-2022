using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 0;

    private GameController gameController;
    private Rigidbody rigidBody;
    private float movementX;
    private float movementY;

    void Start()
    {
        gameController = GameObject.FindWithTag("GameController").GetComponent<GameController>();
        rigidBody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 movement = new Vector3(movementX, 0.0f, movementY);
        rigidBody.AddForce(movement * speed);
    }

    void OnMove(InputValue movementValue)
    {
        if (isActiveAndEnabled)
        {
            Vector2 movementVector = movementValue.Get<Vector2>();
            movementX = movementVector.x;
            movementY = movementVector.y;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            gameController.PlayerDead();
        }
        else if (collision.gameObject.tag == "Goal")
        {
            gameController.PlayerWon();
        }
    }
}
