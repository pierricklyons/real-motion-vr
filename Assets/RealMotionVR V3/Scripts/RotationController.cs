using UnityEngine;

public class RotationController : MonoBehaviour
{
    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private Rigidbody fenderRigidbody;
    private float rotationSpeed;

    private void Awake()
    {
        physicsRig = GetComponentInChildren<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;

        fenderRigidbody = physicsRig.Fender.GetComponent<Rigidbody>();
        rotationSpeed = physicsRig.RotationSpeed;
    }

    private void FixedUpdate()
    {
        if (xrInputManager.RightTranslateAnchorValue.x != 0)
        {
            float rotationAmount = xrInputManager.RightTranslateAnchorValue.x * rotationSpeed * Time.fixedDeltaTime;
            Quaternion deltaRotation = Quaternion.Euler(0, rotationAmount, 0);

            fenderRigidbody.MoveRotation(fenderRigidbody.rotation * deltaRotation);
        }
    }
}
