using UnityEngine;

public class DynamicJointCapsuleCollider : MonoBehaviour
{
    public ConfigurableJoint Joint;
    public CapsuleCollider CapsuleCollider;
    public Transform MeshTransform; // Reference to the visual mesh's Transform

    private void Update()
    {
        // Calculate the world positions of the joint's anchor points
        Vector3 anchorPositionA = Joint.transform.TransformPoint(Joint.anchor);
        Vector3 anchorPositionB = Joint.connectedBody
            ? Joint.connectedBody.transform.TransformPoint(Joint.connectedAnchor)
            : Joint.connectedAnchor;

        // Calculate the distance between the two anchors
        float distance = Vector3.Distance(anchorPositionA, anchorPositionB);

        // Factor in the initial scale of the collider
        float scaleFactor = transform.localScale.y; // Scale along the Y-axis
        float adjustedHeight = distance / scaleFactor;

        // Update the capsule collider's height
        CapsuleCollider.height = Mathf.Max(adjustedHeight, 0.1f); // Ensure height is non-zero

        // Center the capsule collider between the anchors
        CapsuleCollider.center = transform.InverseTransformPoint((anchorPositionA + anchorPositionB) / 2);

        // Scale and position the visual mesh
        if (MeshTransform != null)
        {
            // Scale the mesh's Y-axis to match the collider's height
            Vector3 meshScale = MeshTransform.localScale;
            meshScale.y = adjustedHeight / 2f; // Divide by 2 because capsule height includes both ends
            MeshTransform.localScale = meshScale;

            // Center the mesh between the anchors
            MeshTransform.position = (anchorPositionA + anchorPositionB) / 2;
        }
    }
}
