using UnityEngine;

public class SphereCollisionChecker : MonoBehaviour
{
    private PhysicsRig PhysicsRig; // Reference to your custom PhysicsRig class
    private SphereCollider sphereCollider; // The specific SphereCollider to monitor
    private bool isColliding = false; // Tracks if this SphereCollider is colliding

    private void Awake()
    {
        PhysicsRig = GetComponent<PhysicsRig>();
        sphereCollider = PhysicsRig.Sphere.GetComponent<SphereCollider>();
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the collision involves this specific SphereCollider
        if (collision.collider == sphereCollider)
        {
            isColliding = true;
            Debug.Log($"{sphereCollider.name} started colliding with {collision.gameObject.name}");
        }
    }

    void OnCollisionStay(Collision collision)
    {
        // Ensure we are still colliding with the specific SphereCollider
        if (collision.collider == sphereCollider)
        {
            isColliding = true;
            Debug.Log($"{sphereCollider.name} is still colliding with {collision.gameObject.name}");
        }
    }

    void OnCollisionExit(Collision collision)
    {
        // Check if the exiting collider is this specific SphereCollider
        if (collision.collider == sphereCollider)
        {
            isColliding = false;
            Debug.Log($"{sphereCollider.name} stopped colliding with {collision.gameObject.name}");
        }
    }

    void FixedUpdate()
    {
        PhysicsRig.isGrounded = isColliding;
    }
}
