using UnityEngine;

public class RotationController : MonoBehaviour
{
    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private float rotationSpeed;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        rotationSpeed = PhysicsRig.RotationSpeed;
    }

    void FixedUpdate()
    {
        transform.Rotate(0, XRInputManager.RightTranslateAnchorValue.x * rotationSpeed, 0, Space.Self);
    }

}