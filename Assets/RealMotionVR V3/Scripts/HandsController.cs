using UnityEngine;
public class HandsController : MonoBehaviour
{
    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private ConfigurableJoint LeftHandJoint;
    private ConfigurableJoint RightHandJoint;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        LeftHandJoint = PhysicsRig.LeftHandJoint;
        RightHandJoint = PhysicsRig.RightHandJoint;
    }

    void FixedUpdate()
    {
        if (!XRInputManager.AreControllersInitialized) return;
        MoveAndRotateHands();
    }

    private void MoveAndRotateHands()
    {
        LeftHandJoint.targetPosition = XRInputManager.LeftHandControllerPosition - XRInputManager.CameraControllerPosition;
        RightHandJoint.targetPosition = XRInputManager.RightHandControllerPosition - XRInputManager.CameraControllerPosition;
        LeftHandJoint.targetRotation = XRInputManager.LeftHandControllerRotation;
        RightHandJoint.targetRotation = XRInputManager.RightHandControllerRotation;
    }
}
