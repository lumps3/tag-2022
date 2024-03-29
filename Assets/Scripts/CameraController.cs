using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public float startMovementSeconds = 2f;

    private Transform player;
    private Vector3 offset = new Vector3(0f, 8f, -10f);
    private Vector3 startPosition;
    private float journeyLength;
    private float speed;
    private float startTime;

    void Start()
    {
        player = GameObject.FindWithTag("Player").transform;
        journeyLength = Vector3.Distance(transform.position, (player.position + offset));
        startPosition = transform.position;
        startTime = Time.time;
        speed = journeyLength / startMovementSeconds;
    }

    void Update()
    {
        // Perform initial movements to offset behind player.
        if (Time.time < startTime + startMovementSeconds)
        {
            // Distance moved equals elapsed time times speed..
            float distCovered = (Time.time - startTime) * speed;
            // Fraction of journey completed equals current distance divided by total distance.
            float fractionOfJourney = distCovered / journeyLength;
            // Set our position as a fraction of the distance between the markers.
            transform.position = Vector3.Lerp(startPosition, player.position + offset, fractionOfJourney);
        } 
        else
        {
            // Normal movement follows the player.
            transform.position = player.position + offset;
        }
        // Vector3 targetPosition = player.transform.position + offset;
        // transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, smoothTime);
 
    }
}