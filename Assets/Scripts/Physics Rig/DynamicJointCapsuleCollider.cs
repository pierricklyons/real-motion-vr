using UnityEngine;

public class DynamicJointCapsuleCollider : MonoBehaviour
{
    public ConfigurableJoint Joint;
    public CapsuleCollider CapsuleCollider;
    public Transform MeshTransform;

    private void Update()
    {
        Vector3 anchorPositionA = Joint.transform.TransformPoint(Joint.anchor);
        Vector3 anchorPositionB = Joint.connectedBody
            ? Joint.connectedBody.transform.TransformPoint(Joint.connectedAnchor)
            : Joint.connectedAnchor;

        float distance = Vector3.Distance(anchorPositionA, anchorPositionB);

        float scaleFactor = transform.localScale.y;
        float adjustedHeight = distance / scaleFactor;

        CapsuleCollider.height = Mathf.Max(adjustedHeight, 0.1f);

        CapsuleCollider.center = transform.InverseTransformPoint((anchorPositionA + anchorPositionB) / 2);

        if (MeshTransform != null)
        {
            Vector3 meshScale = MeshTransform.localScale;
            meshScale.y = adjustedHeight / 2f;
            MeshTransform.localScale = meshScale;
            MeshTransform.position = (anchorPositionA + anchorPositionB) / 2;
        }
    }
}
