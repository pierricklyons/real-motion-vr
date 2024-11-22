using UnityEngine;

public class SpineController : MonoBehaviour
{
    public ConfigurableJoint SpineJoint;

    public float verticalOffset;
    public float minTarget;
    public float maxTarget;

    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private GameObject Head;
    private GameObject Chest;
    private GameObject Fender;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;

        Head = PhysicsRig.Head;
        Chest = PhysicsRig.Chest;
        Fender = PhysicsRig.Fender;

        verticalOffset = Fender.transform.position.y + (Head.transform.position.y - Chest.transform.position.y);
        minTarget = PhysicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = PhysicsRig.MaxCrouchHeight - verticalOffset;
    }

    void FixedUpdate()
    {
        if (!PhysicsRig.isCrouching && !PhysicsRig.isTiptoeing && !PhysicsRig.isJumping)
            SetSpineTargetPosition(XRInputManager.CameraControllerPosition.y);
    }

    public void SetSpineTargetPosition(float height)
    {
        float target = Mathf.Clamp(height - verticalOffset, minTarget, maxTarget);
        SpineJoint.targetPosition = new Vector3(0, target, 0);
    }
}
