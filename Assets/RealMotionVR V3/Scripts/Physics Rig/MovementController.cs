using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Kp;
    public float Ki;
    public float Kd;

    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private GameObject sphere;
    private float movementTorqueMultiplier;

    private Rigidbody sphereRigidbody;
    private float sphereRadius;

    private Vector3 lastError;
    private Vector3 integralError;

    private Vector3 targetPosition;
    private Vector3 lastCameraPosition;
    private Vector3 lastSpherePosition;

    private void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        sphere = physicsRig.Sphere;
        movementTorqueMultiplier = physicsRig.MovementSpeed;

        sphereRigidbody = sphere.GetComponent<Rigidbody>();
        sphereRadius = sphere.GetComponent<SphereCollider>().radius * sphere.transform.localScale.x;

        targetPosition = sphereRigidbody.position;

        lastCameraPosition = xrInputManager.CameraControllerPosition;
        lastSpherePosition = sphereRigidbody.position;
    }

    private void FixedUpdate()
    {
        UpdateTargetPosition();
        ApplyPIDControl(targetPosition);
    }

    private void UpdateTargetPosition()
    {
        Vector3 movementInput = GetMovementInput();
        movementInput = transform.rotation * movementInput;

        Vector3 cameraControllerDelta = xrInputManager.CameraControllerPosition - lastCameraPosition;
        cameraControllerDelta = transform.rotation * cameraControllerDelta;

        Vector3 externalMovementDelta = sphereRigidbody.position - lastSpherePosition;

        targetPosition = sphereRigidbody.position + movementInput + cameraControllerDelta - externalMovementDelta;

        lastCameraPosition = xrInputManager.CameraControllerPosition;
        lastSpherePosition = sphereRigidbody.position;
    }

    private Vector3 GetMovementInput()
    {
        Quaternion headYaw = Quaternion.Euler(0, xrInputManager.CameraControllerRotation.eulerAngles.y, 0);
        Vector3 moveDirection = headYaw * new Vector3(xrInputManager.LeftTranslateAnchorValue.x, 0, xrInputManager.LeftTranslateAnchorValue.y);

        return moveDirection * Time.fixedDeltaTime * movementTorqueMultiplier;
    }

    private void ApplyPIDControl(Vector3 targetPosition)
    {
        Vector3 error = targetPosition - sphereRigidbody.position;
        error.y = 0;  // Ignore Y axis

        integralError += error * Time.fixedDeltaTime;

        Vector3 derivativeError = (error - lastError) / Time.fixedDeltaTime;

        Vector3 pidForce = (Kp * error) + (Ki * integralError) + (Kd * derivativeError);

        float rotationAmount = pidForce.magnitude / sphereRadius;
        Vector3 rotationAxis = Vector3.Cross(Vector3.up, pidForce.normalized).normalized;

        sphereRigidbody.AddTorque(rotationAxis * rotationAmount, ForceMode.Force);

        lastError = error;
    }
}
