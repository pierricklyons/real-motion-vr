using UnityEngine;

public class JumpPhysics : MonoBehaviour
{
	[Header("Input Manager")]
	public InputManager InputManager;

	[Header("Hand Physics Joints")]
	public ConfigurableJoint Spine;

	public Vector3 crouchTarget;

	private bool isJumping = false;

	public float jumpPreloadForce = 1.3f;
	public float jumpReleaseForce = 1.3f;
	public float jumpMinCrouch = 0.125f;
	public float maxCrouch = 1.8f;



	// Start is called before the first frame update
	void Start()
	{

	}

	// Update is called once per frame
	void FixedUpdate()
	{

	}

	// Jump control on input
	private void Jump()
	{
		bool jumpButtonPressed = InputManager.rightPrimaryPressed == 1 || InputManager.rightTrackpadPressed == 1;
		if (jumpButtonPressed) JumpPreload();
		else if (isJumping == true) JumpRelease();
	}

	// Virtual crouch preload for jump
	private void JumpPreload()
	{
		isJumping = true;
		crouchTarget.y = Mathf.Clamp(crouchTarget.y -= jumpPreloadForce * Time.fixedDeltaTime, jumpMinCrouch, maxCrouch);
		Spine.targetPosition = new Vector3(0, crouchTarget.y, 0);
	}

	// Virtual crouch release for jump
	private void JumpRelease()
	{
		isJumping = false;
		crouchTarget.y = Mathf.Clamp(crouchTarget.y += jumpReleaseForce * Time.fixedDeltaTime, jumpMinCrouch, maxCrouch);
		Spine.targetPosition = new Vector3(0, crouchTarget.y, 0);
	}
}
