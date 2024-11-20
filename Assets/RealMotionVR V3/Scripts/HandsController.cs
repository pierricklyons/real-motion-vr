using UnityEngine;
public class HandsController : MonoBehaviour
{
    [Header("Input Manager")]
    public XRInputManager XRInputManager;

    [Header("Hand Physics Joints")]
    public ConfigurableJoint LeftHandJoint;
    public ConfigurableJoint RightHandJoint;

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
