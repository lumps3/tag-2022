using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    public float speed = 2;
    public float maxVelocity = 10;

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
        rigidBody.velocity = Vector3.ClampMagnitude(rigidBody.velocity, maxVelocity);
    }

    void OnMove(InputValue movementValue)
    {
        Vector2 movementVector = movementValue.Get<Vector2>();
        movementX = movementVector.x;
        movementY = movementVector.y;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collided with " + collision.gameObject);
        if (collision.gameObject.tag == "Enemy")
        {
            gameController.PlayerDead();
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Goal")
        {
            gameController.PlayerWon();
        }
    }
}
