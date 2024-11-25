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

    public ConfigurableJoint LeftHandJoint;
    public ConfigurableJoint RightHandJoint;
    public ConfigurableJoint SpineJoint;

    public float RotationSpeed = 1.0f;
    public float MovementSpeed = 5.0f;
    public float MinCrouchHeight = 1.0f;
    public float MaxCrouchHeight = 1.8f;
    public float JumpPreloadForce = 1.0f;
    public float JumpReleaseForce = 2.0f;

    public bool isCrouching = false;
    public bool isTiptoeing = false;
    public bool isJumping = false;
    public bool isGrounded = false;

    private MovementController MovementController;
    private HandsController HandsController;
    private SpineController SpineController;
    private CrouchController CrouchController;
    private JumpController JumpController;

    void Awake()
    {
        InitializeControllerScripts();
    }

    private void InitializeControllerScripts()
    {
        MovementController = GetComponent<MovementController>();
        HandsController = GetComponent<HandsController>();
        SpineController = GetComponent<SpineController>();
        CrouchController = GetComponent<CrouchController>();
        JumpController = GetComponent<JumpController>();
    }
}
