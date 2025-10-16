using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private SpineController spineController;

    public float crouchOffset;

    private void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        spineController = GetComponent<SpineController>();
    }

    private void FixedUpdate()
    {
        if (physicsRig.isJumping) return;

        float minCrouchTarget = spineController.MinTarget - (xrInputManager.CameraControllerPosition.y - spineController.VerticalOffset);
        float maxCrouchTarget = spineController.MaxTarget - (xrInputManager.CameraControllerPosition.y - spineController.VerticalOffset);

        float inputY = xrInputManager.RightTranslateAnchorValue.y;

        crouchOffset = Mathf.Clamp(crouchOffset + inputY * Time.fixedDeltaTime, minCrouchTarget, maxCrouchTarget);

        physicsRig.CrouchTarget = xrInputManager.CameraControllerPosition.y + crouchOffset;

        bool wasCrouching = physicsRig.isCrouching;
        bool wasTiptoeing = physicsRig.isTiptoeing;

        physicsRig.isCrouching = physicsRig.CrouchTarget < physicsRig.UserHeight - physicsRig.CrouchAndTiptoeTriggerThreshold;
        physicsRig.isTiptoeing = physicsRig.CrouchTarget > physicsRig.UserHeight + physicsRig.CrouchAndTiptoeTriggerThreshold;

        if (wasCrouching && !physicsRig.isCrouching && !physicsRig.isTiptoeing) physicsRig.isCrouching = true;
        if (wasTiptoeing && !physicsRig.isTiptoeing && !physicsRig.isCrouching) physicsRig.isTiptoeing = true;

        if (xrInputManager.RightSecondaryValue == 1) crouchOffset = 0;

        if (physicsRig.isTiptoeing && crouchOffset > 0 && inputY == 0) crouchOffset = physicsRig.UserHeight - xrInputManager.CameraControllerPosition.y;

        spineController.SetSpineTargetPosition(physicsRig.CrouchTarget);
    }

}
