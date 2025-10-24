using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class XRInputManager : MonoBehaviour
{
    [Header("Controllers")]
    [SerializeField] private ActionBasedController cameraController;
    [SerializeField] private ActionBasedController leftHandController;
    [SerializeField] private ActionBasedController rightHandController;

    [Header("Input Actions")]
    [SerializeField] private InputActionReference leftPrimaryPress;
    [SerializeField] private InputActionReference leftSecondaryPress;
    [SerializeField] private InputActionReference rightPrimaryPress;
    [SerializeField] private InputActionReference rightSecondaryPress;

    [Header("Controller States")]
    [SerializeField] private Vector3 cameraControllerPosition;
    [SerializeField] private Quaternion cameraControllerRotation;

    [SerializeField] private Vector3 leftHandControllerPosition;
    [SerializeField] private Quaternion leftHandControllerRotation;
    [SerializeField] private Vector2 leftTranslateAnchorValue;
    [SerializeField] private float leftPrimaryValue;
    [SerializeField] private float leftSecondaryValue;
    [SerializeField] private float leftActivateValue;
    [SerializeField] private float leftSelectValue;

    [SerializeField] private Vector3 rightHandControllerPosition;
    [SerializeField] private Quaternion rightHandControllerRotation;
    [SerializeField] private Vector2 rightTranslateAnchorValue;
    [SerializeField] private float rightPrimaryValue;
    [SerializeField] private float rightSecondaryValue;
    [SerializeField] private float rightActivateValue;
    [SerializeField] private float rightSelectValue;

    [SerializeField] private bool areControllersInitialized;

    public Vector3 CameraControllerPosition => cameraControllerPosition;
    public Quaternion CameraControllerRotation => cameraControllerRotation;

    public Vector3 LeftHandControllerPosition => leftHandControllerPosition;
    public Quaternion LeftHandControllerRotation => leftHandControllerRotation;
    public Vector2 LeftTranslateAnchorValue => leftTranslateAnchorValue;
    public float LeftPrimaryValue => leftPrimaryValue;
    public float LeftSecondaryValue => leftSecondaryValue;
    public float LeftActivateValue => leftActivateValue;
    public float LeftSelectValue => leftSelectValue;

    public Vector3 RightHandControllerPosition => rightHandControllerPosition;
    public Quaternion RightHandControllerRotation => rightHandControllerRotation;
    public Vector2 RightTranslateAnchorValue => rightTranslateAnchorValue;
    public float RightPrimaryValue => rightPrimaryValue;
    public float RightSecondaryValue => rightSecondaryValue;
    public float RightActivateValue => rightActivateValue;
    public float RightSelectValue => rightSelectValue;

    public bool AreControllersInitialized => areControllersInitialized;

    private void Update()
    {
        UpdateControllerInputs();
        areControllersInitialized = CheckControllersInitialized();
    }

    private bool CheckControllersInitialized()
    {
        return (
            IsPositionValid(cameraControllerPosition) &&
            IsRotationValid(cameraControllerRotation) &&
            IsPositionValid(leftHandControllerPosition) &&
            IsRotationValid(leftHandControllerRotation) &&
            IsPositionValid(rightHandControllerPosition) &&
            IsRotationValid(rightHandControllerRotation)
        );
    }

    private bool IsPositionValid(Vector3 position)
    {
        return (
            position != Vector3.zero &&
            !float.IsNaN(position.x) &&
            !float.IsInfinity(position.x) &&
            !float.IsNaN(position.y) &&
            !float.IsInfinity(position.y) &&
            !float.IsNaN(position.z) &&
            !float.IsInfinity(position.z)
        );
    }

    private bool IsRotationValid(Quaternion rotation)
    {
        return (
            !float.IsNaN(rotation.x) &&
            !float.IsInfinity(rotation.x) &&
            !float.IsNaN(rotation.y) &&
            !float.IsInfinity(rotation.y) &&
            !float.IsNaN(rotation.z) &&
            !float.IsInfinity(rotation.z) &&
            !float.IsNaN(rotation.w) &&
            !float.IsInfinity(rotation.w)
        );
    }

    private void UpdateControllerInputs()
    {
        cameraControllerPosition = cameraController.positionAction.action.ReadValue<Vector3>();
        cameraControllerRotation = cameraController.rotationAction.action.ReadValue<Quaternion>();

        leftHandControllerPosition = leftHandController.positionAction.action.ReadValue<Vector3>();
        leftHandControllerRotation = leftHandController.rotationAction.action.ReadValue<Quaternion>();
        leftTranslateAnchorValue = leftHandController.translateAnchorAction.action.ReadValue<Vector2>();
        leftPrimaryValue = leftPrimaryPress.action.ReadValue<float>();
        leftSecondaryValue = leftSecondaryPress.action.ReadValue<float>();
        leftActivateValue = leftHandController.activateAction.action.ReadValue<float>();
        leftSelectValue = leftHandController.selectAction.action.ReadValue<float>();

        rightHandControllerPosition = rightHandController.positionAction.action.ReadValue<Vector3>();
        rightHandControllerRotation = rightHandController.rotationAction.action.ReadValue<Quaternion>();
        rightTranslateAnchorValue = rightHandController.translateAnchorAction.action.ReadValue<Vector2>();
        rightPrimaryValue = rightPrimaryPress.action.ReadValue<float>();
        rightSecondaryValue = rightSecondaryPress.action.ReadValue<float>();
        rightActivateValue = rightHandController.activateAction.action.ReadValue<float>();
        rightSelectValue = rightHandController.selectAction.action.ReadValue<float>();
    }
}
