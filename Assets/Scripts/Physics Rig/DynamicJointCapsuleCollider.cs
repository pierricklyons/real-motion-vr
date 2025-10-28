using UnityEngine;

public class DynamicJointCapsuleCollider : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private ConfigurableJoint joint;
    [SerializeField] private CapsuleCollider capsuleCollider;
    [SerializeField] private Transform meshTransform;

    public ConfigurableJoint Joint => joint;
    public CapsuleCollider CapsuleCollider => capsuleCollider;
    public Transform MeshTransform => meshTransform;

    private void Update()
    {
        // Skip updates if setup is incomplete
        if (joint == null || capsuleCollider == null) return;

        // Get world positions of both joint anchors
        Vector3 anchorPositionA = joint.transform.TransformPoint(joint.anchor);
        Vector3 anchorPositionB = joint.connectedBody
            ? joint.connectedBody.transform.TransformPoint(joint.connectedAnchor)
            : joint.connectedAnchor;

        // Calculate distance between anchors
        float distance = Vector3.Distance(anchorPositionA, anchorPositionB);

        // Adjust for local scale — ensures collider height matches visual proportions
        float scaleFactor = transform.localScale.y;
        float adjustedHeight = Mathf.Max(distance / scaleFactor, 0.1f); // Prevent zero or negative height

        // Apply new collider height
        capsuleCollider.height = adjustedHeight;

        // Center the collider between both anchors (in local space)
        capsuleCollider.center = transform.InverseTransformPoint((anchorPositionA + anchorPositionB) / 2f);

        // If a visual mesh is assigned, update its scale and position to match
        if (meshTransform != null)
        {
            Vector3 meshScale = meshTransform.localScale;
            meshScale.y = adjustedHeight / 2f; // Divide by 2 since capsule height spans both directions
            meshTransform.localScale = meshScale;

            // Position mesh midway between both anchor points
            meshTransform.position = (anchorPositionA + anchorPositionB) / 2f;
        }
    }


}
