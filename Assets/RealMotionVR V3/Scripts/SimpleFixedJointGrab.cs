using UnityEngine;

public class SimpleFixedJointGrab : MonoBehaviour
{
    public GameObject Hand;
    public enum HandSide { Left, Right }; public HandSide handSide;
    public float releaseDelay = 100f;

    private PhysicsRig physicsRig;
    private XRInputManager xrInputManager;
    private FixedJoint joint = null;
    private bool isGrabbing = false;
    private bool isAttached = false;

    void Awake()
    {
        physicsRig = GetComponent<PhysicsRig>();
        xrInputManager = physicsRig.XRInputManager;
    }

    void FixedUpdate()
    {
        switch (handSide)
        {
            case HandSide.Right:
                isGrabbing = xrInputManager.RightSelectValue > 0.5f;
                break;
            case HandSide.Left:
                isGrabbing = xrInputManager.LeftSelectValue > 0.5f;
                break;
        }
        if (isAttached && isGrabbing) Release();
    }

    void OnCollisionStay(Collision collision)
    {
        Debug.Log("COLLISION!!!!!!!!");
        if (!isAttached && isGrabbing && collision.gameObject.tag == "SimpleGrabbable") Attach(collision);
    }

    private void Attach(Collision collision)
    {
        isAttached = true;
        joint = Hand.AddComponent<FixedJoint>();
        joint.anchor = collision.contacts[0].point;
        joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
        joint.enableCollision = false;
    }

    private void Release()
    {
        Destroy(joint);
        joint = null;
        isAttached = false;
    }
}
