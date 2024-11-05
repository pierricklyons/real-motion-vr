using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class MovementPhysics : MonoBehaviour
{
    public InputManager InputManager;
    public GameObject Sphere;
    public Transform CameraController;
    public float moveForce = 20;
    public float movementSpeed = 5f; // Speed at which the ball moves to the target

    private Rigidbody sphereRigidbody;
    private Vector3 lastCameraPosition;
    private float ballRadius;

    void Start()
    {
        sphereRigidbody = Sphere.GetComponent<Rigidbody>();

        // Get the radius from the Sphere's collider
        SphereCollider sphereCollider = Sphere.GetComponent<SphereCollider>();
        if (sphereCollider != null)
        {
            ballRadius = sphereCollider.radius * Sphere.transform.localScale.x; // Adjust for potential scale
        }
        else
        {
            Debug.LogError("No SphereCollider found on Sphere object.");
        }

        lastCameraPosition = CameraController.position;
    }

    void FixedUpdate()
    {
        // Calculate movement delta based on Camera Controller position
        Vector3 cameraDelta = CameraController.position - lastCameraPosition;
        lastCameraPosition = CameraController.position;

        // Set target position for the sphere based on the camera's movement
        Vector3 targetPosition = sphereRigidbody.position + cameraDelta;

        // Move the sphere directly to the target position
        MoveMonoball(targetPosition);

        // Optionally add control-based movement
        MoveSphere(moveForce);
    }

    private void MoveMonoball(Vector3 targetPosition)
    {
        // Calculate the direction and distance to the target position
        Vector3 direction = targetPosition - sphereRigidbody.position;
        float distance = direction.magnitude;



        // Calculate the required rotation based on movement
        float rotationAmount = distance / ballRadius * Mathf.Rad2Deg;

        // Normalize direction to get the rotation axis
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, direction.normalized).normalized;

        // Move the sphere towards the target position
        Vector3 newPosition = Vector3.Lerp(sphereRigidbody.position, targetPosition, movementSpeed * Time.fixedDeltaTime);
        sphereRigidbody.MovePosition(newPosition);

        // Apply rotation to the sphere
        Quaternion rotation = Quaternion.AngleAxis(rotationAmount, rotationAxis);
        sphereRigidbody.MoveRotation(rotation * sphereRigidbody.rotation);

    }

    private void MoveSphere(float force)
    {
        // Input-based movement can still be applied
        sphereRigidbody.AddTorque(GetSphereDirection() * force, ForceMode.Force);
    }

    private Vector3 GetSphereDirection()
    {
        Quaternion headYaw = Quaternion.Euler(0, InputManager.cameraControllerRotation.eulerAngles.y, 0);
        Vector3 moveDirection = headYaw * new Vector3(InputManager.leftTrackpadValue.x, 0, InputManager.leftTrackpadValue.y);
        return new Vector3(moveDirection.z, 0, -moveDirection.x);
    }
}
