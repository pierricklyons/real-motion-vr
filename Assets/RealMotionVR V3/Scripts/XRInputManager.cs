using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class XRInputManager : MonoBehaviour
{
    public ActionBasedController CameraController;
    public ActionBasedController LeftHandController;
    public ActionBasedController RightHandController;
    public InputActionReference LeftPrimaryPress;
    public InputActionReference LeftSecondaryPress;
    public InputActionReference RightPrimaryPress;
    public InputActionReference RightSecondaryPress;

    public bool AreControllersInitialized;

    public Vector3 CameraControllerPosition;
    public Quaternion CameraControllerRotation;

    public Quaternion LeftHandControllerRotation;
    public Vector3 LeftHandControllerPosition;

    public Vector3 RightHandControllerPosition;
    public Quaternion RightHandControllerRotation;

    public Vector2 LeftTranslateAnchorValue;
    public float LeftPrimaryValue;
    public float LeftSecondaryValue;

    public Vector2 RightTranslateAnchorValue;
    public float RightPrimaryValue;
    public float RightSecondaryValue;

    void FixedUpdate()
    {
        GetControllerInputs();

        AreControllersInitialized = GetAreControllersInitialized();
    }

    private bool GetAreControllersInitialized()
    {
        return
            GetIsPositionValid(CameraControllerPosition) &&
            GetIsRotationValid(CameraControllerRotation) &&
            GetIsPositionValid(LeftHandControllerPosition) &&
            GetIsRotationValid(LeftHandControllerRotation) &&
            GetIsPositionValid(RightHandControllerPosition) &&
            GetIsRotationValid(RightHandControllerRotation);
    }

    private bool GetIsPositionValid(Vector3 position)
    {
        return position != Vector3.zero && !float.IsNaN(position.x) && !float.IsInfinity(position.x);
    }

    private bool GetIsRotationValid(Quaternion rotation)
    {
        return !float.IsNaN(rotation.x) && !float.IsNaN(rotation.y) && !float.IsNaN(rotation.z) && !float.IsNaN(rotation.w);
    }

    private void GetControllerInputs()
    {
        CameraControllerPosition = CameraController.positionAction.action.ReadValue<Vector3>();
        CameraControllerRotation = CameraController.rotationAction.action.ReadValue<Quaternion>();

        LeftHandControllerPosition = LeftHandController.positionAction.action.ReadValue<Vector3>();
        LeftHandControllerRotation = LeftHandController.rotationAction.action.ReadValue<Quaternion>();

        RightHandControllerPosition = RightHandController.positionAction.action.ReadValue<Vector3>();
        RightHandControllerRotation = RightHandController.rotationAction.action.ReadValue<Quaternion>();

        LeftTranslateAnchorValue = LeftHandController.translateAnchorAction.action.ReadValue<Vector2>();
        LeftPrimaryValue = LeftPrimaryPress.action.ReadValue<float>();
        LeftSecondaryValue = LeftSecondaryPress.action.ReadValue<float>();

        RightTranslateAnchorValue = RightHandController.translateAnchorAction.action.ReadValue<Vector2>();
        RightPrimaryValue = RightPrimaryPress.action.ReadValue<float>();
        RightSecondaryValue = RightSecondaryPress.action.ReadValue<float>();
    }
}
