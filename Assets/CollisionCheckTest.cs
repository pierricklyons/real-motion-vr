using UnityEngine;

public class CollisionCheckTest : MonoBehaviour
{
    private bool isColliding = false; // Tracks collision state

    // Called when the GameObject starts colliding with another object
    private void OnCollisionEnter(Collision collision)
    {
        isColliding = true;
        Debug.Log($"Started colliding with: {collision.gameObject.name}");
    }

    // Called while the GameObject stays in contact with another object
    private void OnCollisionStay(Collision collision)
    {
        Debug.Log($"Still colliding with: {collision.gameObject.name}");
    }

    // Called when the GameObject stops colliding with another object
    private void OnCollisionExit(Collision collision)
    {
        isColliding = false;
        Debug.Log($"Stopped colliding with: {collision.gameObject.name}");
    }

    // You can use this to check the collision state
    public bool IsColliding()
    {
        return isColliding;
    }
}
