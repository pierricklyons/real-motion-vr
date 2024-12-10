using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private SpineController spineController;

    public float crouchOffset;
    // public float crouchTarget;

    private void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        spineController = GetComponent<SpineController>();
    }

    private void FixedUpdate()
    {
        if (physicsRig.isJumping) return;

        float minCrouchTarget = spineController.minTarget - (xrInputManager.CameraControllerPosition.y - spineController.verticalOffset);
        float maxCrouchTarget = spineController.maxTarget - (xrInputManager.CameraControllerPosition.y - spineController.verticalOffset);

        float inputY = xrInputManager.RightTranslateAnchorValue.y;
        crouchOffset = Mathf.Clamp(crouchOffset + inputY * Time.fixedDeltaTime, minCrouchTarget, maxCrouchTarget);

        physicsRig.isCrouching = physicsRig.Head.transform.position.y < physicsRig.UserHeight - physicsRig.CrouchAndTiptoeTriggerThreshold;
        physicsRig.isTiptoeing = physicsRig.Head.transform.position.y > physicsRig.UserHeight + physicsRig.CrouchAndTiptoeTriggerThreshold;

        if (xrInputManager.RightSecondaryValue == 1) crouchOffset = 0;

        if (physicsRig.isTiptoeing && crouchOffset > 0 && inputY == 0) crouchOffset = physicsRig.UserHeight - xrInputManager.CameraControllerPosition.y;

        physicsRig.CrouchTarget = xrInputManager.CameraControllerPosition.y + crouchOffset;

        spineController.SetSpineTargetPosition(physicsRig.CrouchTarget);
    }
}
