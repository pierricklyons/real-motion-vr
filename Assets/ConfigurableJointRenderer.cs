using UnityEngine;

public class ConfigurableJointRenderer : MonoBehaviour
{
    public ConfigurableJoint joint; // Reference to the Configurable Joint
    public GameObject capsule; // Capsule GameObject (can be a Capsule Collider or Mesh)

    private void Update()
    {
        if (joint == null || capsule == null) return;

        // Get world positions of the Anchor and Connected Anchor
        Vector3 anchorPosition = transform.TransformPoint(joint.anchor);

        Vector3 connectedAnchorPosition;
        if (joint.connectedBody != null)
        {
            connectedAnchorPosition = joint.connectedBody.transform.TransformPoint(joint.connectedAnchor);
        }
        else
        {
            connectedAnchorPosition = joint.connectedAnchor; // World-space if no connected body
        }

        // Calculate the direction and distance between the two points
        Vector3 direction = connectedAnchorPosition - anchorPosition;
        float distance = direction.magnitude;

        // Set the capsule's position to the midpoint between the two points
        Vector3 midpoint = (anchorPosition + connectedAnchorPosition) / 2f;
        capsule.transform.position = midpoint;

        // Rotate the capsule to align with the direction vector
        if (direction != Vector3.zero) // Avoid errors if direction vector is zero
        {
            capsule.transform.rotation = Quaternion.LookRotation(direction) * Quaternion.Euler(90, 0, 0);
        }

        // Scale the capsule to stretch between the two points
        CapsuleCollider capsuleCollider = capsule.GetComponent<CapsuleCollider>();
        if (capsuleCollider != null)
        {
            // Adjust the height of the Capsule Collider
            capsuleCollider.height = distance;
            capsuleCollider.center = Vector3.zero; // Ensure it's centered
        }

        // Adjust the capsule's transform scale for visual representation
        Vector3 localScale = capsule.transform.localScale;
        localScale.y = distance / 2f; // Capsule height is scaled relative to local Y-axis
        capsule.transform.localScale = localScale;
    }
}
