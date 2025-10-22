using UnityEngine;

public class HeightManager : MonoBehaviour
{
	private PhysicsRig physicsRig;
	private XRInputManager xrInputManager;

	private float inputHoldDelta = 0f;
	public float inputHoldDeltaThreshold = 0.5f;

	private void Awake()
	{
		physicsRig = GetComponent<PhysicsRig>();
		xrInputManager = physicsRig.XRInputManager;

		physicsRig.UserHeight = physicsRig.DefaultUserHeight;
		physicsRig.MaxTiptoeHeight = (physicsRig.TiptoeHeightPercentage / 100) * physicsRig.UserHeight;
		physicsRig.MinCrouchHeight = (physicsRig.CrouchHeightPercentage / 100) * physicsRig.UserHeight;
	}

	private void FixedUpdate()
	{
		if (xrInputManager.RightSecondaryValue == 1)
		{
			inputHoldDelta += Time.deltaTime;
			if (inputHoldDelta >= inputHoldDeltaThreshold) SetRigHeights();
		}
		else inputHoldDelta = 0f;
	}

	private void SetRigHeights()
	{
		physicsRig.UserHeight = xrInputManager.CameraControllerPosition.y;
		physicsRig.MaxTiptoeHeight = (physicsRig.TiptoeHeightPercentage / 100) * physicsRig.UserHeight;
		physicsRig.MinCrouchHeight = (physicsRig.CrouchHeightPercentage / 100) * physicsRig.UserHeight;
	}
}