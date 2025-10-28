using UnityEngine;

public class SphereCollisionChecker : MonoBehaviour
{
    private PhysicsRig physicsRig;

    void Awake()
    {
        // Ensure references are assigned
        if (physicsRig == null) physicsRig = GetComponentInParent<PhysicsRig>();
        if (physicsRig == null) Debug.LogWarning($"{nameof(JumpController)}: Missing required references.");
    }

    private void OnCollisionEnter(Collision collision)
    {
        physicsRig.IsGrounded = true; // Set true if Sphere on collision
    }

    // private void OnCollisionStay(Collision collision){}

    private void OnCollisionExit(Collision collision)
    {
        physicsRig.IsGrounded = false; // Set false on collisioin exit
    }
}
