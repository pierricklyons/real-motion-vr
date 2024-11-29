using UnityEngine;

public class HeadController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private ConfigurableJoint headJoint;

    void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        headJoint = physicsRig.HeadJoint;
    }

    void FixedUpdate()
    {
        headJoint.targetRotation = xrInputManager.CameraControllerRotation;
    }
}
