using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class RotationController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PhysicsRig physicsRig;
    [SerializeField] private XRInputManager xrInputManager;
    [SerializeField] private Rigidbody fenderRigidbody;

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 180f;

    private void Awake()
    {
        // Ensure references are assigned
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
        if (fenderRigidbody == null && physicsRig != null && physicsRig.Fender != null) fenderRigidbody = physicsRig.Fender.GetComponent<Rigidbody>();
        if (physicsRig != null) rotationSpeed = physicsRig.RotationSpeed;
        if (physicsRig == null || xrInputManager == null || fenderRigidbody == null) Debug.LogWarning($"{nameof(RotationController)}: Missing required references.");
    }

    private void FixedUpdate()
    {
        // Skip updates if setup is incomplete
        if (xrInputManager == null || fenderRigidbody == null) return;

        // Get horizontal input from the right-hand controller
        float horizontalInput = xrInputManager.RightTranslateAnchorValue.x;

        if (Mathf.Approximately(horizontalInput, 0f)) return;

        // Calculate rotation based on input, speed, and fixed delta time
        float rotationAmount = horizontalInput * rotationSpeed * Time.fixedDeltaTime;

        // Create rotation quaternion around the Y axis
        Quaternion deltaRotation = Quaternion.Euler(0f, rotationAmount, 0f);

        // Apply rotation to the fender's Rigidbody
        fenderRigidbody.MoveRotation(fenderRigidbody.rotation * deltaRotation);
    }
}
