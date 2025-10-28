using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class HandsController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PhysicsRig physicsRig;
    [SerializeField] private XRInputManager xrInputManager;

    [Header("Hand Joints")]
    [SerializeField] private ConfigurableJoint leftHandJoint;
    [SerializeField] private ConfigurableJoint rightHandJoint;

    private void Awake()
    {
        // Ensure references are assigned
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
        if (leftHandJoint == null && physicsRig != null) leftHandJoint = physicsRig.LeftHandJoint;
        if (rightHandJoint == null && physicsRig != null) rightHandJoint = physicsRig.RightHandJoint;
        if (physicsRig == null || xrInputManager == null || leftHandJoint == null || rightHandJoint == null) Debug.LogWarning($"{nameof(HandsController)}: Missing required references.");
    }

    private void FixedUpdate()
    {
        // Skip updates if setup is incomplete
        if (xrInputManager == null || !xrInputManager.AreControllersInitialized) return;

        UpdateHandJoints();
    }


    // Moves and rotates the hand joints based on the XR controllers' current positions and rotations.
    private void UpdateHandJoints()
    {
        leftHandJoint.targetPosition = xrInputManager.LeftHandControllerPosition - xrInputManager.CameraControllerPosition;
        rightHandJoint.targetPosition = xrInputManager.RightHandControllerPosition - xrInputManager.CameraControllerPosition;
        leftHandJoint.targetRotation = xrInputManager.LeftHandControllerRotation;
        rightHandJoint.targetRotation = xrInputManager.RightHandControllerRotation;
    }
}
