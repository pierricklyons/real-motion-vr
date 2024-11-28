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

    void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManger = physicsRig.XRInputManager;
        spineController = GetComponent<SpineController>();

        jumpPreloadForce = physicsRig.JumpPreloadForce;
        jumpReleaseForce = physicsRig.JumpReleaseForce;
    }

    void FixedUpdate()
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
    }

    void JumpPreload()
    {
        float minJumpPreloadTarget = spineController.minTarget - (xrInputManger.CameraControllerPosition.y - spineController.verticalOffset);

        jumpPreloadOffset = Mathf.Max(jumpPreloadOffset - jumpPreloadForce * Time.fixedDeltaTime, minJumpPreloadTarget);

        jumpPreloadTarget = physicsRig.Head.transform.localPosition.y + jumpPreloadOffset;

        spineController.SetSpineTargetPosition(jumpPreloadTarget);
    }

    void JumpRelease()
    {
        jumpPreloadOffset = 0;
        spineController.SetSpineTargetPosition(physicsRig.MaxCrouchHeight + jumpReleaseForce * Time.fixedDeltaTime);
    }
}
