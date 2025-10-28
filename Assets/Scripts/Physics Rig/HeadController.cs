using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class HeadController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private ConfigurableJoint headJoint;

    private void Awake()
    {
        // Ensure essential references are assigned
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
        if (headJoint == null && physicsRig != null) headJoint = physicsRig.HeadJoint;
        if (physicsRig == null || xrInputManager == null || headJoint == null) Debug.LogWarning($"{nameof(HeadController)}: Missing required references.");
    }

    private void FixedUpdate()
    {
        // Skip updates if setup is incomplete
        if (xrInputManager == null || headJoint == null) return;

        // Apply the camera’s rotation to the head joint
        headJoint.targetRotation = xrInputManager.CameraControllerRotation;
    }
}
