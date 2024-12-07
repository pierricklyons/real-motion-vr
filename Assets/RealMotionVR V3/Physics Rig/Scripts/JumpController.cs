using UnityEngine;

public class JumpController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManger;
    private SpineController spineController;

    private float jumpPreloadForce;
    private float jumpReleaseForce;

    private float jumpPreloadOffset;
    private float jumpPreloadTarget;

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
        if (!physicsRig.isGrounded) return;
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
        spineController.SetSpineTargetPosition(physicsRig.MaxTiptoeHeight + jumpReleaseForce * Time.fixedDeltaTime);
    }
}
