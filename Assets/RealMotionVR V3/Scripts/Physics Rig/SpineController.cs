using UnityEngine;

public class SpineController : MonoBehaviour
{
    public float VerticalOffset;
    public float TargetPosition;
    public float MinTarget;
    public float MaxTarget;

    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private GameObject head;
    private GameObject chest;
    private GameObject fender;
    private ConfigurableJoint spineJoint;

    private void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;

        head = physicsRig.Head;
        chest = physicsRig.Chest;
        fender = physicsRig.Fender;

        spineJoint = physicsRig.SpineJoint;

        VerticalOffset = fender.transform.localPosition.y + (head.transform.localPosition.y - chest.transform.localPosition.y);
        MinTarget = physicsRig.MinCrouchHeight - VerticalOffset;
        MaxTarget = physicsRig.MaxTiptoeHeight - VerticalOffset;
    }

    private void FixedUpdate()
    {
        MinTarget = physicsRig.MinCrouchHeight - VerticalOffset;
        MaxTarget = physicsRig.MaxTiptoeHeight - VerticalOffset;
        if (!physicsRig.isCrouching && !physicsRig.isTiptoeing && !physicsRig.isJumping) SetSpineTargetPosition(xrInputManager.CameraControllerPosition.y);
    }

    public void SetSpineTargetPosition(float height)
    {
        TargetPosition = height;
        float target = Mathf.Clamp(height - VerticalOffset, MinTarget, MaxTarget);
        spineJoint.targetPosition = new Vector3(0, target, 0);
    }
}
