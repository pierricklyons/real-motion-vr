using UnityEngine;
public class HandsController : MonoBehaviour
{
    private PhysicsRig phyisicsRig;
    private XRInputManager xrInputManger;
    private ConfigurableJoint leftHandJoint;
    private ConfigurableJoint rightHandJoint;

    private void Awake()
    {
        phyisicsRig = GetComponent<PhysicsRig>();
        xrInputManger = phyisicsRig.XRInputManager;
        leftHandJoint = phyisicsRig.LeftHandJoint;
        rightHandJoint = phyisicsRig.RightHandJoint;
    }

    private void FixedUpdate()
    {
        if (!xrInputManger.AreControllersInitialized) return;
        MoveAndRotateHands();
    }

    private void MoveAndRotateHands()
    {
        leftHandJoint.targetPosition = xrInputManger.LeftHandControllerPosition - xrInputManger.CameraControllerPosition;
        rightHandJoint.targetPosition = xrInputManger.RightHandControllerPosition - xrInputManger.CameraControllerPosition;
        leftHandJoint.targetRotation = xrInputManger.LeftHandControllerRotation;
        rightHandJoint.targetRotation = xrInputManger.RightHandControllerRotation;
    }
}
