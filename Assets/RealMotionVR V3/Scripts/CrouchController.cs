using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private PhysicsRig PhysicsRig;
    private XRInputManager XRInputManager;
    private SpineController SpineController;

    private float crouchOffset;
    private float crouchTarget;

    void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        XRInputManager = PhysicsRig.XRInputManager;
        SpineController = GetComponent<SpineController>();
    }

    void FixedUpdate()
    {
        if (PhysicsRig.isJumping) return;

        float minCrouchTarget = SpineController.minTarget - (XRInputManager.CameraControllerPosition.y - SpineController.verticalOffset);
        float maxCrouchTarget = SpineController.maxTarget - (XRInputManager.CameraControllerPosition.y - SpineController.verticalOffset);

        float inputY = XRInputManager.RightTranslateAnchorValue.y;
        crouchOffset = Mathf.Clamp(crouchOffset + inputY * Time.fixedDeltaTime, minCrouchTarget, maxCrouchTarget);

        PhysicsRig.isCrouching = crouchOffset < 0;
        PhysicsRig.isTiptoeing = crouchOffset > 0 && inputY != 0;

        if (crouchOffset > 0 && !PhysicsRig.isTiptoeing) crouchOffset = 0;

        crouchTarget = XRInputManager.CameraControllerPosition.y + crouchOffset;

        if (PhysicsRig.isCrouching || PhysicsRig.isTiptoeing) SpineController.SetSpineTargetPosition(crouchTarget);
    }
}
