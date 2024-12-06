using UnityEngine;

public class HeadController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private ConfigurableJoint headJoint;

    private void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        headJoint = physicsRig.HeadJoint;
    }

    private void FixedUpdate()
    {
        headJoint.targetRotation = xrInputManager.CameraControllerRotation;
    }
}
