using UnityEngine;

public class JumpController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManger;
    private SpineController spineController;

    private float jumpPreloadForce;
    private float jumpReleaseForce;

    public float jumpPreloadOffset;
    public float jumpPreloadTarget;

    private float liftTimer;
    private float minLiftDuration = 0.0f;
    private float maxLiftDuration = 1.5f;
    private float liftDuration;
    private float liftPhaseDuration;
    private float resetPhaseDuration;

    private bool isLifting;

    private void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManger = physicsRig.XRInputManager;
        spineController = GetComponent<SpineController>();

        jumpPreloadForce = physicsRig.JumpPreloadForce;
        jumpReleaseForce = physicsRig.JumpReleaseForce;
    }

    private void FixedUpdate()
    {
        if (xrInputManger.RightPrimaryValue == 1)
        {
            physicsRig.isJumping = true;
            JumpPreload();
        }

        if (physicsRig.isJumping && xrInputManger.RightPrimaryValue == 0)
        {
            physicsRig.isJumping = false;
            JumpRelease();
        }

        if (!physicsRig.isGrounded && !physicsRig.isJumping)
        {
            if (isLifting)
            {
                Lift();
            }
        }
    }

    private void JumpPreload()
    {
        float minJumpPreloadTarget = spineController.MinTarget - (xrInputManger.CameraControllerPosition.y - spineController.VerticalOffset);
        jumpPreloadOffset = Mathf.Max(jumpPreloadOffset - jumpPreloadForce * Time.fixedDeltaTime, minJumpPreloadTarget);
        jumpPreloadTarget = physicsRig.Head.transform.localPosition.y + jumpPreloadOffset;
        spineController.SetSpineTargetPosition(jumpPreloadTarget);

        float preloadRatio = (jumpPreloadTarget - minJumpPreloadTarget) / (physicsRig.UserHeight - minJumpPreloadTarget);
        liftDuration = Mathf.Lerp(maxLiftDuration, minLiftDuration, preloadRatio);
    }

    private void JumpRelease()
    {
        jumpPreloadOffset = 0;
        spineController.SetSpineTargetPosition(physicsRig.UserHeight + jumpReleaseForce * Time.fixedDeltaTime);

        liftTimer = 0.0f;
        isLifting = true;
    }

    private void Lift()
    {
        liftPhaseDuration = liftDuration / 2.0f;
        resetPhaseDuration = liftDuration / 2.0f;

        liftTimer += Time.fixedDeltaTime;

        if (liftTimer <= liftPhaseDuration)
        {
            float t = liftTimer / liftPhaseDuration;
            float targetPosition = Mathf.Lerp(physicsRig.UserHeight, jumpPreloadTarget, t);
            spineController.SetSpineTargetPosition(targetPosition);
        }
        else if (liftTimer <= liftDuration)
        {
            float t = (liftTimer - liftPhaseDuration) / resetPhaseDuration;
            float targetPosition = Mathf.Lerp(jumpPreloadTarget, physicsRig.CrouchTarget, t);
            spineController.SetSpineTargetPosition(targetPosition);
        }
        else
        {
            isLifting = false;
            spineController.SetSpineTargetPosition(physicsRig.CrouchTarget);
        }
    }

    // private float CustomEaseInOut(float t)
    // {
    //     // Ease in-out cubic function
    //     if (t < 0.5f)
    //     {
    //         return 4f * t * t * t; // Accelerate
    //     }
    //     else
    //     {
    //         return 1f - Mathf.Pow(-2f * t + 2f, 3f) / 2f; // Decelerate
    //     }
    // }

    // private float CustomEaseOut(float t)
    // {
    //     // Cubic easing function for smooth deceleration
    //     return 1f - Mathf.Pow(1f - t, 3f);
    // }
}
