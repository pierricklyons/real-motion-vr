using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private XRInputManager xrInputManager;
    [SerializeField] private GameObject head;
    [SerializeField] private GameObject leftHand;
    [SerializeField] private GameObject rightHand;
    [SerializeField] private GameObject chest;
    [SerializeField] private GameObject fender;
    [SerializeField] private GameObject sphere;

    [Header("Joints")]
    [SerializeField] private ConfigurableJoint headJoint;
    [SerializeField] private ConfigurableJoint leftHandJoint;
    [SerializeField] private ConfigurableJoint rightHandJoint;
    [SerializeField] private ConfigurableJoint spineJoint;

    [Header("Movement Settings")]
    [SerializeField] private float rotationSpeed = 180.0f;
    [SerializeField] private float movementSpeed = 5.0f;

    [Header("Height Settings")]
    [SerializeField] private float defaultUserHeight = 1.75f;
    [SerializeField] private float userHeight;
    [SerializeField] private float crouchTarget;
    [SerializeField] private float tiptoeHeightPercentage = 107.5f;
    [SerializeField] private float crouchHeightPercentage = 60.0f;
    [SerializeField] private float maxTiptoeHeight;
    [SerializeField] private float minCrouchHeight;

    [Header("Jump Settings")]
    [SerializeField] private float jumpPreloadForce = 1.0f;
    [SerializeField] private float jumpReleaseForce = 1.0f;

    [Header("Triggers")]
    [SerializeField] private float crouchAndTiptoeTriggerThreshold = 0.025f;

    [Header("States")]
    [SerializeField] private bool isCrouching;
    [SerializeField] private bool isTiptoeing;
    [SerializeField] private bool isJumping;
    [SerializeField] private bool isGrounded;

    public XRInputManager XRInputManager => xrInputManager;

    public GameObject Head => head;
    public GameObject LeftHand => leftHand;
    public GameObject RightHand => rightHand;
    public GameObject Chest => chest;
    public GameObject Fender => fender;
    public GameObject Sphere => sphere;

    public ConfigurableJoint HeadJoint => headJoint;
    public ConfigurableJoint LeftHandJoint => leftHandJoint;
    public ConfigurableJoint RightHandJoint => rightHandJoint;
    public ConfigurableJoint SpineJoint => spineJoint;

    public float RotationSpeed => rotationSpeed;
    public float MovementSpeed => movementSpeed;

    public float DefaultUserHeight => defaultUserHeight;
    public float UserHeight
    {
        get => userHeight;
        set => userHeight = value;
    }

    public float CrouchTarget
    {
        get => crouchTarget;
        set => crouchTarget = value;
    }

    public float TiptoeHeightPercentage => tiptoeHeightPercentage;
    public float CrouchHeightPercentage => crouchHeightPercentage;
    public float MaxTiptoeHeight
    {
        get => maxTiptoeHeight;
        set => maxTiptoeHeight = value;
    }
    public float MinCrouchHeight
    {
        get => minCrouchHeight;
        set => minCrouchHeight = value;
    }

    public float JumpPreloadForce => jumpPreloadForce;
    public float JumpReleaseForce => jumpReleaseForce;

    public float CrouchAndTiptoeTriggerThreshold => crouchAndTiptoeTriggerThreshold;

    public bool IsCrouching
    {
        get => isCrouching;
        set => isCrouching = value;
    }

    public bool IsTiptoeing
    {
        get => isTiptoeing;
        set => isTiptoeing = value;
    }

    public bool IsJumping
    {
        get => isJumping;
        set => isJumping = value;
    }

    public bool IsGrounded
    {
        get => isGrounded;
        set => isGrounded = value;
    }
}
