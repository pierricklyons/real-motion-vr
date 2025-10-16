using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class MovingPlatform : MonoBehaviour
{
    public enum MovementAxis { Vertical, Horizontal }

    [Header("Movement Settings")]
    public MovementAxis movementAxis = MovementAxis.Vertical; // Choose the axis of movement
    public float amplitude = 2f;   // Distance of movement
    public float speed = 1f;       // Speed of movement
    public float phaseOffset = 0f; // Starting phase in degrees

    private Rigidbody rb;
    private Vector3 startPosition;

    void Start()
    {
        // Cache the starting position and Rigidbody
        rb = GetComponent<Rigidbody>();
        rb.isKinematic = true; // Platform will move via script
        startPosition = transform.position;
    }

    void FixedUpdate()
    {
        Vector3 newPosition = startPosition;

        // Convert phase offset to radians
        float phaseInRadians = phaseOffset * Mathf.Deg2Rad;

        // Calculate the offset based on the selected axis and phase
        float offset = Mathf.Sin(Time.time * speed + phaseInRadians) * amplitude;

        if (movementAxis == MovementAxis.Vertical)
        {
            newPosition.y += offset; // Move along the Y-axis
        }
        else if (movementAxis == MovementAxis.Horizontal)
        {
            newPosition.x += offset; // Move along the X-axis (you can replace this with Z if needed)
        }

        // Move the Rigidbody to the new position
        rb.MovePosition(newPosition);
    }
}
