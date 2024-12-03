using UnityEngine;

public class CrouchController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private SpineController spineController;

    public float crouchOffset;
    private float crouchTarget;

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

        physicsRig.isCrouching = crouchOffset < 0 && physicsRig.Head.transform.position.y < physicsRig.UserHeight;
        physicsRig.isTiptoeing = crouchOffset > 0 && physicsRig.Head.transform.position.y > physicsRig.UserHeight;

        // if (crouchOffset > 0 && !physicsRig.isTiptoeing) crouchOffset = 0;

        // physicsRig.isCrouching = physicsRig.Head.transform.position.y + 0.05f < physicsRig.UserHeight;
        // physicsRig.isTiptoeing = physicsRig.Head.transform.position.y - 0.05f > physicsRig.UserHeight;

        if (xrInputManager.RightSecondaryValue == 1) crouchOffset = 0;
        // if (physicsRig.Head.transform.position.y > physicsRig.UserHeight && inputY == 0)

        crouchTarget = xrInputManager.CameraControllerPosition.y + crouchOffset;

        if (physicsRig.Head.transform.position.y > physicsRig.UserHeight && inputY == 0) crouchTarget = physicsRig.UserHeight;
        if (crouchOffset != 0) spineController.SetSpineTargetPosition(crouchTarget);
    }
}
