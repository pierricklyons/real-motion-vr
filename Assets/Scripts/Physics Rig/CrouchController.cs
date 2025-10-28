using UnityEngine;

public class CrouchController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PhysicsRig physicsRig;
    [SerializeField] private XRInputManager xrInputManager;
    [SerializeField] private SpineController spineController;

    [Header("Crouch Settings")]
    [SerializeField] private float crouchOffset;

    private void Awake()
    {
        // Ensure references are assigned
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
        if (spineController == null) spineController = GetComponent<SpineController>();
        if (physicsRig == null || xrInputManager == null || spineController == null) Debug.LogWarning($"{nameof(CrouchController)}: Missing required references.");
    }

    private void FixedUpdate()
    {
        // Skip crouch logic while jumping
        if (physicsRig.IsJumping) return;

        // Calculate crouch limits relative to camera height and spine offset
        float minCrouchTarget = spineController.MinTarget - (xrInputManager.CameraControllerPosition.y - spineController.VerticalOffset);
        float maxCrouchTarget = spineController.MaxTarget - (xrInputManager.CameraControllerPosition.y - spineController.VerticalOffset);

        // Apply vertical input to crouch offset
        float inputY = xrInputManager.RightTranslateAnchorValue.y;
        crouchOffset = Mathf.Clamp(crouchOffset + inputY * Time.fixedDeltaTime, minCrouchTarget, maxCrouchTarget);

        // Update target height based on offset
        physicsRig.CrouchTarget = xrInputManager.CameraControllerPosition.y + crouchOffset;

        // Store previous states for comparison
        bool wasCrouching = physicsRig.IsCrouching;
        bool wasTiptoeing = physicsRig.IsTiptoeing;

        // Determine crouching/tiptoeing states
        physicsRig.IsCrouching = physicsRig.CrouchTarget < physicsRig.UserHeight - physicsRig.CrouchAndTiptoeTriggerThreshold;
        physicsRig.IsTiptoeing = physicsRig.CrouchTarget > physicsRig.UserHeight + physicsRig.CrouchAndTiptoeTriggerThreshold;

        // Preserve state when transitioning between crouch/tiptoe/neutral
        if (wasCrouching && !physicsRig.IsCrouching && !physicsRig.IsTiptoeing) physicsRig.IsCrouching = true;
        if (wasTiptoeing && !physicsRig.IsTiptoeing && !physicsRig.IsCrouching) physicsRig.IsTiptoeing = true;

        // Reset crouch offset on secondary button press
        if (xrInputManager.RightSecondaryValue == 1) crouchOffset = 0f;

        // Adjust offset when tiptoeing and input stops
        if (physicsRig.IsTiptoeing && crouchOffset > 0f && inputY == 0f) crouchOffset = physicsRig.UserHeight - xrInputManager.CameraControllerPosition.y;

        // Apply final target position to the spine controller
        spineController.SetSpineTargetPosition(physicsRig.CrouchTarget);
    }
}
