using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class JumpController : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PhysicsRig physicsRig;
    [SerializeField] private XRInputManager xrInputManager;
    [SerializeField] private SpineController spineController;

    [Header("Jump Settings")]
    [SerializeField] private float jumpPreloadForce;
    [SerializeField] private float jumpReleaseForce;

    [Header("Runtime State (Debug)")]
    [SerializeField] private float jumpPreloadOffset;
    [SerializeField] private float jumpPreloadTarget;
    [SerializeField] private bool isLifting;

    private float liftTimer;
    private float liftDuration;
    private float liftPhaseDuration;
    private float resetPhaseDuration;

    private const float MinLiftDuration = 0.0f;
    private const float MaxLiftDuration = 1.5f;

    private void Awake()
    {
        // Ensure references are assigned
        if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
        if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
        if (spineController == null) spineController = GetComponent<SpineController>();
        if (physicsRig == null || xrInputManager == null || spineController == null) Debug.LogWarning($"{nameof(JumpController)}: Missing required references.");

        // Initialize jump forces from the rig configuration
        jumpPreloadForce = physicsRig.JumpPreloadForce;
        jumpReleaseForce = physicsRig.JumpReleaseForce;
    }

    private void FixedUpdate()
    {
        // Skip updates if setup is incomplete
        if (physicsRig == null || xrInputManager == null || spineController == null) return;

        // Hold Right Primary button to preload a jump
        if (xrInputManager.RightPrimaryValue == 1f)
        {
            physicsRig.IsJumping = true;
            JumpPreload();
        }

        // Release button to trigger the jump release
        if (physicsRig.IsJumping && xrInputManager.RightPrimaryValue == 0f)
        {
            physicsRig.IsJumping = false;
            JumpRelease();
        }

        // While in the air, perform lift interpolation
        if (!physicsRig.IsGrounded && !physicsRig.IsJumping && isLifting)
        {
            Lift();
        }
    }

    // Simulates a downward compression (preload) phase before jumping, the longer the player holds the button, the greater the potential jump force
    private void JumpPreload()
    {
        // Determine minimum crouch-based offset
        float minJumpPreloadTarget = spineController.MinTarget - (xrInputManager.CameraControllerPosition.y - spineController.VerticalOffset);

        // Apply downward offset gradually based on preload force
        jumpPreloadOffset = Mathf.Max(jumpPreloadOffset - jumpPreloadForce * Time.fixedDeltaTime, minJumpPreloadTarget);

        // Calculate target spine position
        jumpPreloadTarget = physicsRig.Head.transform.localPosition.y + jumpPreloadOffset;
        spineController.SetSpineTargetPosition(jumpPreloadTarget);

        // Determine preload ratio (used to modulate lift speed)
        float preloadRatio = (jumpPreloadTarget - minJumpPreloadTarget) / (physicsRig.UserHeight - minJumpPreloadTarget);

        // Interpolate total lift duration between min and max based on preload depth
        liftDuration = Mathf.Lerp(MaxLiftDuration, MinLiftDuration, preloadRatio);
    }

    // Triggered when the preload button is released — initiates the upward lift motion
    private void JumpRelease()
    {
        // Reset preload offset and apply initial upward force
        jumpPreloadOffset = 0f;
        spineController.SetSpineTargetPosition(physicsRig.UserHeight + jumpReleaseForce * Time.fixedDeltaTime);

        // Initialize lift timing
        liftTimer = 0f;
        isLifting = true;
    }


    // Handles the lifting motion after jump release, moves the spine target smoothly upward and then back down (needs replacing with an ease function)
    private void Lift()
    {
        // Split total lift duration into ascent and descent phases
        liftPhaseDuration = liftDuration / 2f;
        resetPhaseDuration = liftDuration / 2f;

        liftTimer += Time.fixedDeltaTime;

        // Ascent
        if (liftTimer <= liftPhaseDuration)
        {
            float t = liftTimer / liftPhaseDuration;
            float targetPosition = Mathf.Lerp(physicsRig.UserHeight, jumpPreloadTarget, t);
            spineController.SetSpineTargetPosition(targetPosition);
        }
        // Descent
        else if (liftTimer <= liftDuration)
        {
            float t = (liftTimer - liftPhaseDuration) / resetPhaseDuration;
            float targetPosition = Mathf.Lerp(jumpPreloadTarget, physicsRig.CrouchTarget, t);
            spineController.SetSpineTargetPosition(targetPosition);
        }
        // Land
        else
        {
            isLifting = false;
            spineController.SetSpineTargetPosition(physicsRig.CrouchTarget);
        }
    }

    /*
    private float CustomEaseInOut(float t)
    {
        return t < 0.5f ? 4f * t * t * t : 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f;
    }

    private float CustomEaseOut(float t)
    {
        return 1f - Mathf.Pow(1f - t, 3f);
    }
    */
}
