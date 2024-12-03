using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    public XRInputManager XRInputManager;
    public GameObject Head;
    public GameObject LeftHand;
    public GameObject RightHand;
    public GameObject Chest;
    public GameObject Fender;
    public GameObject Sphere;

    public ConfigurableJoint HeadJoint;
    public ConfigurableJoint LeftHandJoint;
    public ConfigurableJoint RightHandJoint;
    public ConfigurableJoint SpineJoint;

    public float RotationSpeed = 180.0f;
    public float MovementSpeed = 5.0f;
    public float UserHeight = 1.75f;
    public float MinCrouchHeight;
    public float MaxCrouchHeight;
    public float JumpPreloadForce = 1.0f;
    public float JumpReleaseForce = 2.0f;

    public bool isCrouching = false;
    public bool isTiptoeing = false;
    public bool isJumping = false;
    public bool isGrounded = false;

    // private MovementController MovementController;
    // private HandsController HandsController;
    // private SpineController SpineController;
    // private CrouchController CrouchController;
    // private JumpController JumpController;

    private float inputHoldTime = 0f; // Tracks how long the input is held
    private float holdThreshold = 0.5f; // Minimum time (in seconds) to register a hold

    void Awake()
    {
        MinCrouchHeight = 0.5f * UserHeight;
        MaxCrouchHeight = UserHeight + 0.075f * UserHeight;
    }

    void FixedUpdate()
    {
        if (XRInputManager.RightSecondaryValue == 1)
        {
            inputHoldTime += Time.deltaTime;
            if (inputHoldTime >= holdThreshold) SetRigHeights();
        }
        else inputHoldTime = 0f;
    }

    // private void InitializeControllerScripts()
    // {
    //     MovementController = GetComponent<MovementController>();
    //     HandsController = GetComponent<HandsController>();
    //     SpineController = GetComponent<SpineController>();
    //     CrouchController = GetComponent<CrouchController>();
    //     JumpController = GetComponent<JumpController>();
    // }

    public void SetRigHeights()
    {
        UserHeight = XRInputManager.CameraControllerPosition.y;
        MinCrouchHeight = 0.5f * UserHeight;
        MaxCrouchHeight = UserHeight + 0.075f * UserHeight;
    }
}
