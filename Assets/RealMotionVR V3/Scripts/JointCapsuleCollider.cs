using UnityEngine;
public class JointCapsuleCollider : MonoBehaviour
{
    public ConfigurableJoint Joint;
    public CapsuleCollider CapsuleCollider;

    void FixedUpdate()
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
    }
}