using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleFixedJointGrab : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private InputActionReference controllerSelect;
    [SerializeField] private GameObject hand;

    [Header("Settings")]
    [SerializeField] private string grabbableTag = "SimpleGrabbable";

    private float controllerSelected;
    private FixedJoint joint = null;
    private bool attached = false;

    private void FixedUpdate()
    {
        // Read controller input
        GetInput();

        // Release object if currently attached but controller released
        if (attached && Mathf.Approximately(controllerSelected, 0f)) Release();

    }

    private void OnCollisionStay(Collision collision)
    {
        // Only attach if not already attached, controller pressed, and object has correct tag
        if (!attached && Mathf.Approximately(controllerSelected, 1f) && collision.gameObject.CompareTag(grabbableTag)) Attach(collision);
    }

    // Reads the controller's select input.
    private void GetInput()
    {
        if (controllerSelect != null && controllerSelect.action != null) controllerSelected = controllerSelect.action.ReadValue<float>();
    }

    // Attaches the hand to the collided object using a FixedJoint.
    private void Attach(Collision collision)
    {
        if (hand == null || collision == null) return;

        attached = true;

        // Add FixedJoint to hand
        joint = hand.AddComponent<FixedJoint>();
        joint.enableCollision = false;

        // Set joint connection to collided object's Rigidbody
        Rigidbody targetRigidbody = collision.collider.attachedRigidbody ?? collision.collider.GetComponentInParent<Rigidbody>();
        if (targetRigidbody != null)
        {
            joint.connectedBody = targetRigidbody;
            joint.anchor = hand.transform.InverseTransformPoint(collision.contacts[0].point);
        }
        else
        {
            Debug.LogWarning($"SimpleFixedJointGrab: No Rigidbody found on {collision.gameObject.name}");
            Destroy(joint);
            joint = null;
            attached = false;
        }
    }

    // Releases the grabbed object by destroying the FixedJoint.
    private void Release()
    {
        if (joint != null)
        {
            Destroy(joint);
            joint = null;
        }

        attached = false;
    }
}
