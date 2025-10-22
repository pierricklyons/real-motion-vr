using UnityEngine;
using UnityEngine.InputSystem;

public class SimpleFixedJointGrab : MonoBehaviour
{
    public InputActionReference ControllerSelect;
    public GameObject Hand;

    private float ControllerSelected;
    private FixedJoint joint = null;
    private bool attached = false;

    private void FixedUpdate()
    {
        GetInput();
        if (attached && ControllerSelected == 0) Release();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (!attached && ControllerSelected == 1 && collision.gameObject.tag == "SimpleGrabbable") Attach(collision);
    }

    private void GetInput()
    {
        ControllerSelected = ControllerSelect.action.ReadValue<float>();
    }

    private void Attach(Collision collision)
    {
        attached = true;
        joint = Hand.AddComponent<FixedJoint>();
        joint.anchor = collision.contacts[0].point;
        joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
        joint.enableCollision = false;
    }

    private void Release()
    {
        Destroy(joint);
        joint = null;
        attached = false;
    }
}
