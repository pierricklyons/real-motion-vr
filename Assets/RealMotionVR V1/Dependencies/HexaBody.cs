using UnityEngine;
using Unity.XR.CoreUtils;

public class HexaBody : MonoBehaviour
{
    // Public inspector fields
    [Header("XR Rig")]
    public GameObject PlayerController;
    public XROrigin XROrigin;
    public GameObject XRCamera;
    public GameObject CameraOffset;

    // Reference to InputManager script
    private InputManager InputManager;

    [Header("Hexabody")]
    public GameObject Body;
    public GameObject Head;
    public GameObject Chest;
    public GameObject Fender;
    public GameObject Sphere;
    public GameObject RightHand;
    public GameObject LeftHand;

    public ConfigurableJoint RightHandJoint;
    public ConfigurableJoint LeftHandJoint;
    public ConfigurableJoint Spine;

    [Header("Movement")]
    public float turnForce = 2.5f;
    public float moveForceCrouch = 5;
    public float moveForceWalk = 15;
    public float moveForceSprint = 20;

    [Header("Drag")]
    public float angularDragOnMove = 35;
    public float angularBreakDrag = 100;

    [Header("Crouch and Jump")]
    public float jumpPreloadForce = 1.3f;
    public float jumpReleaseForce = 1.3f;
    public float jumpMinCrouch = 0.125f;
    public float crouchForce = 0.005f;
    public float minCrouch = 0f;
    public float maxCrouch = 1.8f;
    public Vector3 crouchTarget;

    // Body fields
    private bool jumping = false;
    private bool moving = false;
    // private bool crouching = false;
    private bool tiptoeing = false;

    private float originalHeight;
    private float additionalHeight;
    private Vector3 climbingInitialPosition;

    private Quaternion headYaw;
    private Vector3 moveDirection;
    private Vector3 sphereTorque;

    // On script start
    void Start()
    {
        InputManager = gameObject.GetComponent<InputManager>();
        InitializePlayerHeight();
    }

    // On every physics update
    private void FixedUpdate()
    {
        CalculateBodyDirection();
        MoveAndRotateHands();
        MoveAndRotateBody();
        AlignRigAndBody();
        Jump();
        CrouchControl();
        // Debugs();
    }

    private void Debugs()
    {
        Debug.Log("Jumping: " + jumping);
        Debug.Log("Moving: " + moving);
        Debug.Log("Tiptoeing: " + tiptoeing);
    }

    // Initialize player's height
    private void InitializePlayerHeight()
    {
        originalHeight = (0.5f * Sphere.transform.lossyScale.y) + (0.5f * Fender.transform.lossyScale.y) + (Head.transform.position.y - Chest.transform.position.y);
        additionalHeight = originalHeight;
    }

    // Calculate body orientation and movement direction from inputs
    private void CalculateBodyDirection()
    {
        // Values
        headYaw = Quaternion.Euler(0, XRCamera.transform.eulerAngles.y, 0);
        moveDirection = headYaw * new Vector3(InputManager.leftTrackpadValue.x, 0, InputManager.leftTrackpadValue.y);
        sphereTorque = new Vector3(moveDirection.z, 0, -moveDirection.x);
    }

    // Sync up Body and XRRig + Roomscale
    private void AlignRigAndBody()
    {
        Body.transform.position = new Vector3(InputManager.CameraController.transform.position.x, Body.transform.position.y, InputManager.CameraController.transform.position.z);
        XRCamera.transform.rotation = InputManager.CameraController.transform.rotation;
    }

    // Movement
    private void MoveAndRotateBody()
    {
        RotateBody();
        MoveBody();
    }

    // Rotates Rig AND Body
    private void RotateBody()
    {
        Chest.transform.rotation = headYaw;
        Fender.transform.rotation = headYaw;
        if (InputManager.rightTrackpadPressed == 1) return;
        if (InputManager.rightTrackpadValue.x > 0.25f || InputManager.rightTrackpadValue.x < -0.25f)
        {
            Head.transform.Rotate(0, InputManager.rightTrackpadValue.x * turnForce, 0, Space.Self);
            XROrigin.transform.RotateAround(Body.transform.position, Vector3.up, InputManager.rightTrackpadValue.x * turnForce);
        }
    }

    // Sphere control on input
    private void MoveBody()
    {
        if (InputManager.leftTrackpadTouched == 0) StopSphere();
        if (InputManager.leftTrackpadTouched == 1 && InputManager.leftTrackpadPressed == 0) MoveSphere(moveForceWalk);
        if (InputManager.leftTrackpadTouched == 1 && InputManager.leftTrackpadPressed == 1) MoveSphere(moveForceSprint);
        if (jumping && InputManager.leftTrackpadTouched == 1) MoveSphere(moveForceCrouch);
    }

    // Add torque to sphere for body movement
    private void MoveSphere(float force)
    {
        Sphere.GetComponent<Rigidbody>().freezeRotation = false;
        moving = true;
        Sphere.GetComponent<Rigidbody>().angularDrag = angularDragOnMove;
        Sphere.GetComponent<Rigidbody>().AddTorque(sphereTorque * (force * 2), ForceMode.Force);
    }

    // Stops sphere and freezes its rotation
    private void StopSphere()
    {
        Sphere.GetComponent<Rigidbody>().angularDrag = angularBreakDrag;
        if (Sphere.GetComponent<Rigidbody>().velocity == Vector3.zero) Sphere.GetComponent<Rigidbody>().freezeRotation = true;
        moving = false;
    }

    // Jump control on input
    private void Jump()
    {
        bool jumpButtonPressed = InputManager.rightPrimaryPressed == 1 || InputManager.rightTrackpadPressed == 1;
        if (jumpButtonPressed) JumpPreload();
        else if (jumping == true) JumpRelease();
    }

    // Virtual crouch preload for jump
    private void JumpPreload()
    {
        jumping = true;
        crouchTarget.y = Mathf.Clamp(crouchTarget.y -= jumpPreloadForce * Time.fixedDeltaTime, jumpMinCrouch, maxCrouch);
        Spine.targetPosition = new Vector3(0, crouchTarget.y, 0);
    }

    // Virtual crouch release for jump
    private void JumpRelease()
    {
        jumping = false;
        crouchTarget.y = Mathf.Clamp(crouchTarget.y += jumpReleaseForce * Time.fixedDeltaTime, jumpMinCrouch, maxCrouch);
        Spine.targetPosition = new Vector3(0, crouchTarget.y, 0);
    }

    // Crouch control
    private void CrouchControl()
    {
        if (!jumping)
        {
            VirtuallyCrouch();
            PhysicallyCrouch();
            if (InputManager.rightSecondaryPressed == 1) ResetCrouchHeight();
        }
    }

    // Resets height to originalHeight calculated at Start()
    private void ResetCrouchHeight()
    {
        additionalHeight = originalHeight;
    }

    // Additional height on input for virtual crouch 
    private void VirtuallyCrouch()
    {
        if (InputManager.rightTrackpadValue.y < -0.85f) additionalHeight += crouchForce;
        if (InputManager.rightTrackpadValue.y > 0.85f)
        {
            tiptoeing = true;
            additionalHeight -= crouchForce;
        }
        if (tiptoeing == true && InputManager.rightTrackpadValue.y < 0.85f && additionalHeight < originalHeight)
        {
            ResetCrouchHeight();
            tiptoeing = false;
        }
    }

    // Physical crouch dictated by head height and additional height based on virtual crouch
    private void PhysicallyCrouch()
    {
        crouchTarget.y = Mathf.Clamp(InputManager.cameraControllerPosition.y - additionalHeight, minCrouch, maxCrouch - originalHeight);
        Spine.targetPosition = new Vector3(0, crouchTarget.y, 0);
    }

    // Moves and rotates hands with a target
    private void MoveAndRotateHands()
    {
        RightHandJoint.targetPosition = InputManager.rightHandControllerPosition - InputManager.cameraControllerPosition;
        LeftHandJoint.targetPosition = InputManager.leftHandControllerPosition - InputManager.cameraControllerPosition;
        RightHandJoint.targetRotation = InputManager.rightHandControllerRotation;
        LeftHandJoint.targetRotation = InputManager.leftHandControllerRotation;
    }
}