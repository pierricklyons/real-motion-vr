using UnityEngine;

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
		// Ensure references are set
		if (physicsRig == null) physicsRig = GetComponent<PhysicsRig>();

		if (xrInputManager == null && physicsRig != null) xrInputManager = physicsRig.XRInputManager;

		if (physicsRig == null || xrInputManager == null)
		{
			Debug.LogWarning($"{nameof(HeightManager)}: Missing required references.");
			return;
		}

		InitializeRigHeights();
	}

	private void FixedUpdate()
	{
		if (physicsRig == null || xrInputManager == null) return;

		// Hold secondary button to recalibrate height
		if (xrInputManager.RightSecondaryValue == 1f)
		{
			inputHoldDelta += Time.deltaTime;

			if (inputHoldDelta >= inputHoldDeltaThreshold) SetRigHeights();
		}
		else
		{
			inputHoldDelta = 0f;
		}
	}

	// Initialize to default height
	private void InitializeRigHeights()
	{
		physicsRig.UserHeight = physicsRig.DefaultUserHeight;
		physicsRig.MaxTiptoeHeight = (physicsRig.TiptoeHeightPercentage / 100f) * physicsRig.UserHeight;
		physicsRig.MinCrouchHeight = (physicsRig.CrouchHeightPercentage / 100f) * physicsRig.UserHeight;
	}

	private void SetRigHeights()
	{
		float currentCameraHeight = xrInputManager.CameraControllerPosition.y;

		physicsRig.UserHeight = currentCameraHeight;
		physicsRig.MaxTiptoeHeight = (physicsRig.TiptoeHeightPercentage / 100f) * physicsRig.UserHeight;
		physicsRig.MinCrouchHeight = (physicsRig.CrouchHeightPercentage / 100f) * physicsRig.UserHeight;
	}
}