using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private SpineController spineController;

    private float crouchOffset;
    private float crouchTarget;

    void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
        spineController = GetComponent<SpineController>();
    }

    void FixedUpdate()
    {
        if (physicsRig.isJumping) return;

        float minCrouchTarget = spineController.minTarget - (xrInputManager.CameraControllerPosition.y - spineController.verticalOffset);
        float maxCrouchTarget = spineController.maxTarget - (xrInputManager.CameraControllerPosition.y - spineController.verticalOffset);

        float inputY = xrInputManager.RightTranslateAnchorValue.y;
        crouchOffset = Mathf.Clamp(crouchOffset + inputY * Time.fixedDeltaTime, minCrouchTarget, maxCrouchTarget);

        physicsRig.isCrouching = crouchOffset < 0;
        physicsRig.isTiptoeing = crouchOffset > 0 && inputY != 0;

        if (crouchOffset > 0 && !physicsRig.isTiptoeing) crouchOffset = 0;

        crouchTarget = xrInputManager.CameraControllerPosition.y + crouchOffset;

        if (physicsRig.isCrouching || physicsRig.isTiptoeing) spineController.SetSpineTargetPosition(crouchTarget);
    }
}
