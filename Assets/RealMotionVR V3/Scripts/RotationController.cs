using UnityEngine;

public class RotationController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private float rotationSpeed;

    private void Awake()
    {
        physicsRig = GetComponentInChildren<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        rotationSpeed = physicsRig.RotationSpeed;
    }

    private void FixedUpdate()
    {
        if (xrInputManager.RightTranslateAnchorValue.x != 0) transform.parent.Rotate(0, xrInputManager.RightTranslateAnchorValue.x * rotationSpeed * Time.fixedDeltaTime, 0, Space.Self);
    }
}