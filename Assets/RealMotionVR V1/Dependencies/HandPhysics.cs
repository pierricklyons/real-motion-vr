using UnityEngine;
public class HandPhysics : MonoBehaviour
{
    [Header("Input Manager")]
    public InputManager InputManager;

    [Header("Hand Physics Joints")]
    public ConfigurableJoint RightHandJoint;
    public ConfigurableJoint LeftHandJoint;

    void FixedUpdate()
    {
        MoveAndRotateHands();
    }

    // Moves and rotates hands with a target
    private void MoveAndRotateHands()
    {
        RightHandJoint.targetPosition = InputManager.rightHandControllerPosition - InputManager.cameraControllerPosition;
        LeftHandJoint.targetPosition = InputManager.leftHandControllerPosition - InputManager.cameraControllerPosition;
        RightHandJoint.targetRotation = InputManager.rightHandControllerRotation;
        LeftHandJoint.targetRotation = InputManager.leftHandControllerRotation;
    }
}
