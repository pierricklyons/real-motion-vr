using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class SpineController : MonoBehaviour
{
    [Header("Spine Settings")]
    [SerializeField, ] private float verticalOffset;
    [SerializeField] private float targetPosition;
    [SerializeField] private float minTarget;
    [SerializeField] private float maxTarget;

    [Header("References")]
    [SerializeField] private PhysicsRig physicsRig;
    [SerializeField] private XRInputManager xrInputManager;

    private GameObject head;
    private GameObject chest;
    private GameObject fender;
    private ConfigurableJoint spineJoint;

    public float VerticalOffset => verticalOffset;
    public float TargetPosition => targetPosition;
    public float MinTarget => minTarget;
    public float MaxTarget => maxTarget;

    private void Awake()
    {
        // Ensure references are assigned
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;

        if (physicsRig == null || xrInputManager == null)
        {
            Debug.LogWarning($"{nameof(SpineController)}: Missing required references.");
            return;
        }

        head = physicsRig.Head;
        chest = physicsRig.Chest;
        fender = physicsRig.Fender;
        spineJoint = physicsRig.SpineJoint;

        // Calculate offsets and bounds
        verticalOffset = fender.transform.localPosition.y + (head.transform.localPosition.y - chest.transform.localPosition.y);

        minTarget = physicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = physicsRig.MaxTiptoeHeight - verticalOffset;
    }

    private void FixedUpdate()
    {
        // Update crouch/tiptoe limits each frame
        minTarget = physicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = physicsRig.MaxTiptoeHeight - verticalOffset;

        // When standing normally (not crouching, tiptoeing, or jumping),
        // the spine should follow the camera height
        if (!physicsRig.IsCrouching && !physicsRig.IsTiptoeing && !physicsRig.IsJumping) SetSpineTargetPosition(xrInputManager.CameraControllerPosition.y);

    }

    public void SetSpineTargetPosition(float height)
    {
        targetPosition = height;

        float clampedHeight = Mathf.Clamp(height - verticalOffset, minTarget, maxTarget);
        spineJoint.targetPosition = new Vector3(0f, clampedHeight, 0f);
    }
}
