using UnityEngine;

public class SphereCollisionChecker : MonoBehaviour
{
    private PhysicsRig physicsRig;

    void Awake()
    {
        physicsRig = GetComponentInParent<PhysicsRig>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        physicsRig.IsGrounded = true;
    }

    // private void OnCollisionStay(Collision collision){}

    private void OnCollisionExit(Collision collision)
    {
        physicsRig.IsGrounded = false;
    }
}
