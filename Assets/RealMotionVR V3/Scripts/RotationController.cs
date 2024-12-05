using UnityEngine;

public class RotationController : MonoBehaviour
{
    // public GameObject ParentGameObject;
    // private Rigidbody parentRigidbody;

    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private GameObject fender;
    private Rigidbody fenderRigidbody;
    private float rotationSpeed;

    private void Awake()
    {
        physicsRig = GetComponentInChildren<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;

        // parentRigidbody = ParentGameObject.GetComponent<Rigidbody>();
        fender = physicsRig.Fender;
        fenderRigidbody = fender.GetComponent<Rigidbody>();
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
