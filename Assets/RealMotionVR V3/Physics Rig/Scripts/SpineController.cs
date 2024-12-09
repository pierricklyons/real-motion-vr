using UnityEngine;

public class SpineController : MonoBehaviour
{
    public float verticalOffset;
    public float TargetPosition;
    public float minTarget;
    public float maxTarget;

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

        verticalOffset = fender.transform.localPosition.y + (head.transform.localPosition.y - chest.transform.localPosition.y);
        minTarget = physicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = physicsRig.MaxTiptoeHeight - verticalOffset;
    }

    private void FixedUpdate()
    {
        minTarget = physicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = physicsRig.MaxTiptoeHeight - verticalOffset;
        if (!physicsRig.isCrouching && !physicsRig.isTiptoeing && !physicsRig.isJumping) SetSpineTargetPosition(xrInputManager.CameraControllerPosition.y);
    }

    public void SetSpineTargetPosition(float height)
    {
        TargetPosition = height;
        float target = Mathf.Clamp(height - verticalOffset, minTarget, maxTarget);
        spineJoint.targetPosition = new Vector3(0, target, 0);
    }
}
