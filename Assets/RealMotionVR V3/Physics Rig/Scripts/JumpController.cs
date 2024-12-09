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

    private float previousHeight;
    private float verticalVelocity;

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
        // Calculate vertical velocity
        float currentHeight = physicsRig.Head.transform.position.y; // Or use the monoball position
        verticalVelocity = (currentHeight - previousHeight) / Time.fixedDeltaTime;
        previousHeight = currentHeight;

        // if (!physicsRig.isGrounded) return;
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

        if (!physicsRig.isGrounded && physicsRig.isJumping == false)
        {
            Lift(verticalVelocity);
        }
    }

    private void JumpPreload()
    {
        float minJumpPreloadTarget = spineController.minTarget - (xrInputManger.CameraControllerPosition.y - spineController.verticalOffset);

        jumpPreloadOffset = Mathf.Max(jumpPreloadOffset - jumpPreloadForce * Time.fixedDeltaTime, minJumpPreloadTarget);

        jumpPreloadTarget = physicsRig.Head.transform.localPosition.y + jumpPreloadOffset;

        spineController.SetSpineTargetPosition(jumpPreloadTarget);
    }

    private void JumpRelease()
    {
        jumpPreloadOffset = 0;
        spineController.SetSpineTargetPosition(physicsRig.UserHeight + jumpReleaseForce * Time.fixedDeltaTime);
    }

    private void Lift(float verticalVelocity)
    {
        float targetOffset;

        if (verticalVelocity > 0)
        {
            // Moving upward: Tuck legs by lowering target
            targetOffset = Mathf.Lerp(physicsRig.UserHeight, jumpPreloadTarget, 500f);
        }
        else if (verticalVelocity < 0)
        {
            // Moving downward: Extend legs back to normal
            targetOffset = Mathf.Lerp(jumpPreloadTarget, physicsRig.UserHeight, 500);
        }
        else
        {
            // Maintain the current leg position
            targetOffset = jumpPreloadTarget;
        }

        spineController.SetSpineTargetPosition(targetOffset);
    }
}