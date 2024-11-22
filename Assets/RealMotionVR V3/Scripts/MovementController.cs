using UnityEngine;

public class MovementController : MonoBehaviour
{
    public float Kp;
    public float Ki;
    public float Kd;

    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private GameObject Sphere;
    private float movementTorqueMultiplier;

    private Rigidbody sphereRigidbody;
    private float sphereRadius;

    private Vector3 lastError;
    private Vector3 integralError;

    private Vector3 targetPosition;
    private Vector3 lastCameraPosition;
    private Vector3 lastSpherePosition;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        Sphere = PhysicsRig.Sphere;
        movementTorqueMultiplier = PhysicsRig.MovementSpeed;

        sphereRigidbody = Sphere.GetComponent<Rigidbody>();
        sphereRadius = Sphere.GetComponent<SphereCollider>().radius * Sphere.transform.localScale.x;

        targetPosition = Sphere.transform.position;

        lastCameraPosition = XRInputManager.CameraControllerPosition;
        lastSpherePosition = sphereRigidbody.position;
    }

    void FixedUpdate()
    {
        UpdateTargetPosition();
        ApplyPIDControl(targetPosition);
    }

    private void UpdateTargetPosition()
    {
        Vector3 movementInput = GetMovementInput();
        Vector3 cameraControllerDelta = XRInputManager.CameraControllerPosition - lastCameraPosition;
        Vector3 externalMovementDelta = sphereRigidbody.position - lastSpherePosition;

        targetPosition = sphereRigidbody.position + movementInput + cameraControllerDelta - externalMovementDelta;

        lastCameraPosition = XRInputManager.CameraControllerPosition;
        lastSpherePosition = sphereRigidbody.position;
    }

    private Vector3 GetMovementInput()
    {
        Quaternion headYaw = Quaternion.Euler(0, XRInputManager.CameraControllerRotation.eulerAngles.y, 0);
        Vector3 moveDirection = headYaw * new Vector3(XRInputManager.LeftTranslateAnchorValue.x, 0, XRInputManager.LeftTranslateAnchorValue.y);

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
