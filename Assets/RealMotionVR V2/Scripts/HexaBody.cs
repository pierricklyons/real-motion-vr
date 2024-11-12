using UnityEngine;
using Unity.XR.CoreUtils;

public class HexaBody : MonoBehaviour
{
	// Public inspector fields
	[Header("XR Rig")]
	public GameObject PlayerController;
	public XROrigin XROrigin;
	public GameObject CameraOffset;
	public GameObject XRCamera;

	// Reference to InputManager script
	public InputManager InputManager;

	[Header("Hexabody")]
	public GameObject Head;
	public GameObject Chest;
	public GameObject Fender;
	public GameObject Sphere;
	public ConfigurableJoint Spine;

	public float turnForce = 2.5f;

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
	private bool tiptoeing = false;

	private float playerHeight;
	private float additionalHeight;


	private Rigidbody headRigidbody;
	private Rigidbody chestRigidbody;
	private Rigidbody fenderRigidbody;

	// On script start
	void Start()
	{
		InputManager.GetComponent<InputManager>();
		InitializePlayerHeight();

		headRigidbody = Head.GetComponent<Rigidbody>();
		chestRigidbody = Chest.GetComponent<Rigidbody>();
		fenderRigidbody = Fender.GetComponent<Rigidbody>();
	}

	// On every physics update
	private void FixedUpdate()
	{
		RotateBody();
		Jump();
		CrouchControl();
	}

	// Initialize player's height
	private void InitializePlayerHeight()
	{
		playerHeight = (0.5f * Sphere.transform.lossyScale.y) + (0.5f * Fender.transform.lossyScale.y) + (Head.transform.position.y - Chest.transform.position.y);
		additionalHeight = playerHeight;
	}

	// Rotates Rig AND Body
	private void RotateBody()
	{
		if (InputManager.rightTrackpadPressed == 1) return;
		if (InputManager.rightTrackpadValue.x > 0.25f || InputManager.rightTrackpadValue.x < -0.25f)
		{
			// Head.transform.Rotate(0, InputManager.rightTrackpadValue.x * turnForce, 0, Space.Self);
			// Chest.transform.Rotate(0, InputManager.rightTrackpadValue.x * turnForce, 0, Space.Self);
			// Fender.transform.Rotate(0, InputManager.rightTrackpadValue.x * turnForce, 0, Space.Self);
			XROrigin.transform.RotateAround(Head.transform.position, Vector3.up, InputManager.rightTrackpadValue.x * turnForce);

			// Calculate the new rotation using the existing rotation plus the desired turn angle
			Quaternion deltaRotation = Quaternion.Euler(0, InputManager.rightTrackpadValue.x * turnForce, 0);
			Quaternion targetRotation = headRigidbody.rotation * deltaRotation;

			// Apply the rotation smoothly using MoveRotation
			headRigidbody.MoveRotation(targetRotation);
			chestRigidbody.MoveRotation(targetRotation);
			fenderRigidbody.MoveRotation(targetRotation);
		}
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
		if (jumping) return;

		VirtuallyCrouch();
		PhysicallyCrouch();
		if (InputManager.rightSecondaryPressed == 1) ResetCrouchHeight();
	}

	// Resets height to originalHeight calculated at Start()
	private void ResetCrouchHeight()
	{
		additionalHeight = playerHeight;
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
		if (tiptoeing == true && InputManager.rightTrackpadValue.y < 0.85f && additionalHeight < playerHeight)
		{
			ResetCrouchHeight();
			tiptoeing = false;
		}
	}

	// Physical crouch dictated by head height and additional height based on virtual crouch
	private void PhysicallyCrouch()
	{
		crouchTarget.y = Mathf.Clamp(InputManager.cameraControllerPosition.y - additionalHeight, minCrouch, maxCrouch - playerHeight);
		Spine.targetPosition = new Vector3(0, crouchTarget.y, 0);
	}
}