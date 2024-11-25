using UnityEngine;

public class RotationController : MonoBehaviour
{
    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private float rotationSpeed;
    private float yaw;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        rotationSpeed = PhysicsRig.RotationSpeed;
    }

    void FixedUpdate()
    {
        yaw += XRInputManager.RightTranslateAnchorValue.x * Time.fixedDeltaTime;
        Quaternion targetRotation = Quaternion.Euler(0, yaw, 0);
        PhysicsRig.Fender.GetComponent<Rigidbody>().MoveRotation(targetRotation);
    }
}