using UnityEngine;
using Unity.XR.CoreUtils;

public class Movement : MonoBehaviour
{
    public InputManager InputManager;

    public GameObject Sphere;  // The monoball
    public Transform CameraController; // Reference to the Camera Controller inside XR Origin

    public float moveForce;
    private float sphereRadius;

    private Vector3 previousCameraPosition;

    void Start()
    {
        InputManager.GetComponent<InputManager>();

        previousCameraPosition = CameraController.position;

        sphereRadius = Sphere.transform.lossyScale.y * .5f;
    }

    void FixedUpdate()
    {
        MoveSphere(moveForce);
    }

    private Vector3 GetSphereDirection()
    {
        Quaternion headYaw = Quaternion.Euler(0, InputManager.cameraControllerRotation.eulerAngles.y, 0);
        Vector3 moveDirection = headYaw * new Vector3(InputManager.leftTrackpadValue.x, 0, InputManager.leftTrackpadValue.y);
        return new Vector3(moveDirection.z, 0, -moveDirection.x);
    }

    private void MoveSphere(float force)
    {
        // Calculate camera delta movement
        Vector3 cameraDelta = CameraController.position - previousCameraPosition;

        // Calculate rotation from camera movement
        Vector3 rotationFromCamera = cameraDelta / sphereRadius * Mathf.Rad2Deg;

        // Apply rotation from camera movement to the monoball
        Sphere.GetComponent<Rigidbody>().AddTorque(rotationFromCamera, ForceMode.Force);

        // Apply additional directional movement based on user input
        Sphere.GetComponent<Rigidbody>().AddTorque(GetSphereDirection() * force, ForceMode.Force);

        // Update the previous camera position
        previousCameraPosition = CameraController.position;
    }
}
