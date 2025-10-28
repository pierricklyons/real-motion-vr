using UnityEngine;

[RequireComponent(typeof(PhysicsRig))]
public class HeightManager : MonoBehaviour
{
	[Header("References")]
	[SerializeField] private PhysicsRig physicsRig;
	[SerializeField] private XRInputManager xrInputManager;

	[Header("Input Settings")]
	[SerializeField] private float inputHoldDeltaThreshold = 0.5f;

	private float inputHoldDelta = 0f;

	private void Awake()
	{
		// Ensure references are assigned
		if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();
		if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;
		if (physicsRig == null || xrInputManager == null) Debug.LogWarning($"{nameof(HeightManager)}: Missing required references.");

		// Initialize the height values for the user's default body proportions
		InitializeRigHeights();
	}

	private void FixedUpdate()
	{
		// Skip updates if setup is incomplete
		if (physicsRig == null || xrInputManager == null) return;

		// Detect if the right secondary button is being held
		if (xrInputManager.RightSecondaryValue == 1f)
		{
			// Increment hold duration timer
			inputHoldDelta += Time.deltaTime;

			// Trigger height recalibration if the hold exceeds the threshold
			if (inputHoldDelta >= inputHoldDeltaThreshold) SetRigHeights();
		}
		else
		{
			// Reset hold timer when button is released
			inputHoldDelta = 0f;
		}
	}

	/// Initializes the PhysicsRig height values based on the default user height.
	private void InitializeRigHeights()
	{
		// Set the default standing height
		physicsRig.UserHeight = physicsRig.DefaultUserHeight;

		// Calculate tiptoe and crouch height limits as percentages of the user's height
		physicsRig.MaxTiptoeHeight = (physicsRig.TiptoeHeightPercentage / 100f) * physicsRig.UserHeight;
		physicsRig.MinCrouchHeight = (physicsRig.CrouchHeightPercentage / 100f) * physicsRig.UserHeight;
	}


	/// Recalibrates the PhysicsRig height based on the current headset (camera) position.
	private void SetRigHeights()
	{
		// Get the current camera (HMD) Y position in world space
		float currentCameraHeight = xrInputManager.CameraControllerPosition.y;

		// Update user height to match real-world camera height
		physicsRig.UserHeight = currentCameraHeight;

		// Recalculate crouch and tiptoe thresholds relative to the new height
		physicsRig.MaxTiptoeHeight = (physicsRig.TiptoeHeightPercentage / 100f) * physicsRig.UserHeight;
		physicsRig.MinCrouchHeight = (physicsRig.CrouchHeightPercentage / 100f) * physicsRig.UserHeight;
	}
}
