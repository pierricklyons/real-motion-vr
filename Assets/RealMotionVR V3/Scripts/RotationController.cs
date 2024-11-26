using UnityEngine;

public class RotationController : MonoBehaviour
{
    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private float rotationSpeed;

    void Awake()
    {
        PhysicsRig = GetComponentInChildren<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        rotationSpeed = PhysicsRig.RotationSpeed;
    }

    void FixedUpdate()
    {
        transform.parent.Rotate(0, XRInputManager.RightTranslateAnchorValue.x * rotationSpeed * Time.fixedDeltaTime, 0, Space.Self);
    }
}