using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class SpineController : MonoBehaviour
{
    [Header("Spine Settings")]
    [SerializeField,] private float verticalOffset;
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
        if (physicsRig == null || xrInputManager == null) Debug.LogWarning($"{nameof(SpineController)}: Missing required references.");

        // Cache references from the physics rig
        head = physicsRig.Head;
        chest = physicsRig.Chest;
        fender = physicsRig.Fender;
        spineJoint = physicsRig.SpineJoint;

        // Calculate vertical offset:
        verticalOffset = fender.transform.localPosition.y + (head.transform.localPosition.y - chest.transform.localPosition.y);

        // Initialize crouch and tiptoe limits based on the rig’s height configuration
        minTarget = physicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = physicsRig.MaxTiptoeHeight - verticalOffset;
    }

    private void FixedUpdate()
    {
        // Skip updates if setup is incomplete
        if (physicsRig == null || xrInputManager == null) return;

        // Continuously update crouch/tiptoe limits to match real-time rig height adjustments
        minTarget = physicsRig.MinCrouchHeight - verticalOffset;
        maxTarget = physicsRig.MaxTiptoeHeight - verticalOffset;

        // If the player is standing normally (not crouching, tiptoeing, or jumping), the spine joint should track the headset’s (camera’s) vertical position
        if (!physicsRig.IsCrouching && !physicsRig.IsTiptoeing && !physicsRig.IsJumping) SetSpineTargetPosition(xrInputManager.CameraControllerPosition.y);
    }


    // Updates the target height of the spine joint, ensuring it stays within crouch/tiptoe limits
    public void SetSpineTargetPosition(float height)
    {
        // Store the target height for debugging or external queries
        targetPosition = height;

        // Clamp the height to stay within allowed crouch and tiptoe range
        float clampedHeight = Mathf.Clamp(height - verticalOffset, minTarget, maxTarget);

        // Apply the clamped height to the ConfigurableJoint’s target position
        spineJoint.targetPosition = new Vector3(0f, clampedHeight, 0f);
    }
}

