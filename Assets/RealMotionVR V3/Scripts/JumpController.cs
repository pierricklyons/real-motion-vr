using UnityEngine;

public class JumpController : MonoBehaviour
{
    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private SpineController SpineController;

    private float jumpPreloadForce;
    private float jumpReleaseForce;

    private float jumpPreloadOffset;
    private float jumpPreloadTarget;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        SpineController = GetComponent<SpineController>();

        jumpPreloadForce = PhysicsRig.JumpPreloadForce;
        jumpReleaseForce = PhysicsRig.JumpReleaseForce;
    }

    void FixedUpdate()
    {
        if (XRInputManager.RightPrimaryValue == 1)
        {
            PhysicsRig.isJumping = true;
            JumpPreload();
        }
        if (PhysicsRig.isJumping && XRInputManager.RightPrimaryValue == 0)
        {
            PhysicsRig.isJumping = false;
            JumpRelease();
        }
    }

    void JumpPreload()
    {
        float minJumpPreloadTarget = SpineController.minTarget - (XRInputManager.CameraControllerPosition.y - SpineController.verticalOffset);

        jumpPreloadOffset = Mathf.Max(jumpPreloadOffset - jumpPreloadForce * Time.fixedDeltaTime, minJumpPreloadTarget);

        jumpPreloadTarget = XRInputManager.CameraControllerPosition.y + jumpPreloadOffset;

        SpineController.SetSpineTargetPosition(jumpPreloadTarget);
    }

    void JumpRelease()
    {
        jumpPreloadOffset = 0;
        SpineController.SetSpineTargetPosition(PhysicsRig.MaxCrouchHeight + jumpReleaseForce * Time.fixedDeltaTime);
    }
}
