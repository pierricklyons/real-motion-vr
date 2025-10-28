using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class MovementController : MonoBehaviour
{
    [Header("PID Settings")]
    public float Kp = 500; // Proportional term: reacts to current error
    public float Ki = 1; // Integral term: reacts to accumulated past errors
    public float Kd = 10; // Derivative term: reacts to rate of change of error

    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private GameObject sphere;
    private float movementTorqueMultiplier;

    private Rigidbody sphereRigidbody;
    private float sphereRadius;

    // PID error tracking
    private Vector3 lastError;
    private Vector3 integralError;

    // Target tracking
    private Vector3 targetPosition;
    private Vector3 lastCameraPosition;
    private Vector3 lastSpherePosition;

    private void Awake()
    {
        // Assign references
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
        if (sphere == null && physicsRig != null) sphere = physicsRig.Sphere;
        movementTorqueMultiplier = physicsRig.MovementSpeed;

        // Cache Rigidbody and sphere radius for torque calculations
        sphereRigidbody = sphere.GetComponent<Rigidbody>();
        sphereRadius = sphere.GetComponent<SphereCollider>().radius * sphere.transform.localScale.x;

        // Initialize target and previous positions for PID calculations
        targetPosition = sphereRigidbody.position;
        lastCameraPosition = xrInputManager.CameraControllerPosition;
        lastSpherePosition = sphereRigidbody.position;
    }

    private void FixedUpdate()
    {
        UpdateTargetPosition();
        ApplyPIDControl(targetPosition);
    }

    // Updates the desired target position for the sphere based on Player input (left-hand controller thumbstick), Camera/headset movement and external movement of the sphere (physics interactions)

    private void UpdateTargetPosition()
    {
        // Get player movement input, rotated relative to the rig's yaw
        Vector3 movementInput = GetMovementInput();
        movementInput = transform.rotation * movementInput;

        // Track how the camera has moved since the last frame
        Vector3 cameraControllerDelta = xrInputManager.CameraControllerPosition - lastCameraPosition;
        cameraControllerDelta = transform.rotation * cameraControllerDelta;

        // Detect any external displacement of the sphere (e.g., collisions)
        Vector3 externalMovementDelta = sphereRigidbody.position - lastSpherePosition;

        // Compute new target: sphere position + input + camera movement - external displacement
        targetPosition = sphereRigidbody.position + movementInput + cameraControllerDelta - externalMovementDelta;

        // Cache positions for next frame
        lastCameraPosition = xrInputManager.CameraControllerPosition;
        lastSpherePosition = sphereRigidbody.position;
    }

    // Reads the player's left-hand controller input and converts it into a world-space movement vector, movement is oriented to the headset yaw so forward is always in the direction the player is looking
    private Vector3 GetMovementInput()
    {
        // Create a rotation that only considers the headset yaw (horizontal rotation)
        Quaternion headYaw = Quaternion.Euler(0f, xrInputManager.CameraControllerRotation.eulerAngles.y, 0f);

        // Transform input from thumbstick into world-space vector
        Vector3 moveDirection = headYaw * new Vector3(xrInputManager.LeftTranslateAnchorValue.x, 0f, xrInputManager.LeftTranslateAnchorValue.y);

        // Scale by fixed delta time and movement multiplier for smooth movement
        return moveDirection * Time.fixedDeltaTime * movementTorqueMultiplier;
    }

    // Applies a PID controller to move the sphere towards the target position and converts the PID output vector into torque applied on the sphere to roll it
    private void ApplyPIDControl(Vector3 targetPosition)
    {
        // Calculate positional error
        Vector3 error = targetPosition - sphereRigidbody.position;
        error.y = 0f; // Ignore vertical displacement; sphere only rolls on XZ plane

        // Accumulate error over time for integral term
        integralError += error * Time.fixedDeltaTime;

        // Calculate rate of change for derivative term
        Vector3 derivativeError = (error - lastError) / Time.fixedDeltaTime;

        // Compute PID "force" vector
        Vector3 pidForce = (Kp * error) + (Ki * integralError) + (Kd * derivativeError);

        // Convert PID force into torque for a rolling sphere
        float rotationAmount = pidForce.magnitude / sphereRadius;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, pidForce.normalized).normalized;

        // Apply torque to sphere
        sphereRigidbody.AddTorque(rotationAxis * rotationAmount, ForceMode.Force);

        // Cache error for next frame's derivative calculation
        lastError = error;
    }
}
