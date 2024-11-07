using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.XR.CoreUtils;

public class MovementPhysics : MonoBehaviour
{
    public InputManager InputManager;
    public GameObject Sphere;
    public Transform CameraController;
    // public float moveForce = 10f;
    // public float dampingFactor = 0.9f;

    // PID controller gains
    public Vector3 error;
    public float Kp = 2f;   // Proportional gain
    public float Ki = 0.1f; // Integral gain
    public float Kd = 0.5f; // Derivative gain

    private Rigidbody sphereRigidbody;
    private float sphereRadius;
    private Vector3 lastError;
    private Vector3 integralError;

    void Start()
    {
        // Set Sphere Rigidbody
        sphereRigidbody = Sphere.GetComponent<Rigidbody>();
        // Set Sphere radius
        sphereRadius = Sphere.GetComponent<SphereCollider>().radius * Sphere.transform.localScale.x;
    }

    void FixedUpdate()
    {
        // Set the CameraController's position as the target position, ignoring the Y component
        Vector3 targetPosition = GetTargetPosition();

        ApplyPIDControl(targetPosition);
    }

    private Vector3 GetTargetPosition()
    {
        return new Vector3(CameraController.position.x, sphereRigidbody.position.y, CameraController.position.z);
    }

    private void ApplyPIDControl(Vector3 targetPosition)
    {
        // Calculate error and its derivatives, ignoring the Y axis
        error = targetPosition - sphereRigidbody.position;
        error.y = 0; // Ignore vertical error

        integralError += error * Time.fixedDeltaTime;
        Vector3 derivativeError = (error - lastError) / Time.fixedDeltaTime;

        // PID force calculation
        Vector3 pidForce = (Kp * error) + (Ki * integralError) + (Kd * derivativeError);

        // Apply force to the sphere based on PID output
        sphereRigidbody.AddForce(pidForce, ForceMode.Force);

        // Apply rotational torque based on movement direction for realism
        float rotationAmount = pidForce.magnitude / sphereRadius;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, pidForce.normalized).normalized;
        sphereRigidbody.AddTorque(rotationAxis * rotationAmount, ForceMode.Force);

        // Update last error for next derivative calculation
        lastError = error;
    }
}
