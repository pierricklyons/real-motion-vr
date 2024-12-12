using UnityEngine;

public class AlignXRAndPhysics : MonoBehaviour
{
    public GameObject CameraOffset;
    public GameObject CameraController;

    private Vector3 previousCameraControllerLocalPosition;

    private void Start()
    {
        previousCameraControllerLocalPosition = CameraOffset.transform.InverseTransformPoint(CameraController.transform.position);
    }

    private void FixedUpdate()
    {
        // Get the current local position of the CameraController relative to the CameraOffset
        Vector3 currentCameraControllerLocalPosition = CameraOffset.transform.InverseTransformPoint(CameraController.transform.position);

        // Calculate the delta in local space
        Vector3 localDelta = currentCameraControllerLocalPosition - previousCameraControllerLocalPosition;

        // Apply the inverse delta (negating x and z) to the CameraOffset's world position
        CameraOffset.transform.position -= CameraOffset.transform.TransformVector(new Vector3(localDelta.x, 0, localDelta.z));

        // Update the previous local position
        previousCameraControllerLocalPosition = CameraOffset.transform.InverseTransformPoint(CameraController.transform.position);
    }
}
