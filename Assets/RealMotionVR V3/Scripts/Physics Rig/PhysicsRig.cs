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
    public float DefaultUserHeight = 1.75f;
    public float UserHeight;
    public float CrouchTarget;
    public float TiptoeHeightPercentage = 7.5f;
    public float CrouchHeightPercentage = 60.0f;
    public float MaxTiptoeHeight;
    public float MinCrouchHeight;
    public float JumpPreloadForce = 1.0f;
    public float JumpReleaseForce = 1.0f;

    public float CrouchAndTiptoeTriggerThreshold = 0.025f;

    public bool isCrouching = false;
    public bool isTiptoeing = false;
    public bool isJumping = false;
    public bool isGrounded = false;

    // private void Awake()
    // {
    //     HeightTarget = UserHeight;
    // }

    // private MovementController MovementController;
    // private HandsController HandsController;
    // private SpineController SpineController;
    // private CrouchController CrouchController;
    // private JumpController JumpController;

    // private void InitializeControllerScripts()
    // {
    //     MovementController = GetComponent<MovementController>();
    //     HandsController = GetComponent<HandsController>();
    //     SpineController = GetComponent<SpineController>();
    //     CrouchController = GetComponent<CrouchController>();
    //     JumpController = GetComponent<JumpController>();
    // }
}