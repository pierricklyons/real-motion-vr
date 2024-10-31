using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [Header("Controllers")]
    public ActionBasedController CameraController;
    public ActionBasedController RightHandController;
    public ActionBasedController LeftHandController;

    public InputActionReference RightTrackPadPress;
    public InputActionReference LeftTrackPadPress;

    public InputActionReference RightTrackPadTouch;
    public InputActionReference LeftTrackPadTouch;

    public InputActionReference RightPrimaryPress;
    public InputActionReference LeftPrimaryPress;

    public InputActionReference RightSecondaryPress;
    public InputActionReference LeftSecondaryPress;

    // Input fields
    public Vector3 cameraControllerPosition;
    public Vector3 rightHandControllerPosition;
    public Vector3 leftHandControllerPosition;

    public Quaternion cameraControllerRotation;
    public Quaternion rightHandControllerRotation;
    public Quaternion leftHandControllerRotation;

    public Vector2 rightTrackpadValue;
    public Vector2 leftTrackpadValue;

    public float rightTrackpadPressed;
    public float leftTrackpadPressed;

    public float rightTrackpadTouched;
    public float leftTrackpadTouched;

    public float rightPrimaryPressed;
    public float leftPrimaryPressed;

    public float rightSecondaryPressed;
    public float leftSecondaryPressed;

    void Start()
    {
        GetControllerInputs();
    }

    void FixedUpdate()
    {
        GetControllerInputs();
    }

    // Gets controller inputs and values for use with body
    private void GetControllerInputs()
    {
        // Right controller position & rotation
        rightHandControllerPosition = RightHandController.positionAction.action.ReadValue<Vector3>();
        rightHandControllerRotation = RightHandController.rotationAction.action.ReadValue<Quaternion>();
        // Right trackpad value, press and touch
        rightTrackpadValue = RightHandController.translateAnchorAction.action.ReadValue<Vector2>();
        rightTrackpadPressed = RightTrackPadPress.action.ReadValue<float>();
        rightTrackpadTouched = RightTrackPadTouch.action.ReadValue<float>();
        // Right primary and secondary press
        rightPrimaryPressed = RightPrimaryPress.action.ReadValue<float>();
        rightSecondaryPressed = RightSecondaryPress.action.ReadValue<float>();

        // Left contoller position & rotation
        leftHandControllerPosition = LeftHandController.positionAction.action.ReadValue<Vector3>();
        leftHandControllerRotation = LeftHandController.rotationAction.action.ReadValue<Quaternion>();
        // Left trackpad value, press and touch
        leftTrackpadValue = LeftHandController.translateAnchorAction.action.ReadValue<Vector2>();
        leftTrackpadPressed = LeftTrackPadPress.action.ReadValue<float>();
        leftTrackpadTouched = LeftTrackPadTouch.action.ReadValue<float>();
        // Left primary and secondary press
        leftPrimaryPressed = LeftPrimaryPress.action.ReadValue<float>();
        leftSecondaryPressed = LeftSecondaryPress.action.ReadValue<float>();

        // Headset controller position & rotation
        cameraControllerPosition = CameraController.positionAction.action.ReadValue<Vector3>();
        cameraControllerRotation = CameraController.rotationAction.action.ReadValue<Quaternion>();
    }
}
