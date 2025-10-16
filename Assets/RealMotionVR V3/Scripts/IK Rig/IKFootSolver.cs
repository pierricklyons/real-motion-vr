using UnityEngine;

public class IKFootSolver : MonoBehaviour
{
    public GameObject Sphere;
    public Transform IKRig = default;
    public LayerMask terrainLayer = default;
    public IKFootSolver OtherFoot = default;
    public float stepSpeed = 4, stepDistanceThreshold = .2f, stepLength = .2f, sideStepLength = .1f;
    public float stepHeight = .3f, footYPosOffset = 0.1f, rayStartYOffset = 0, rayLength = 1.5f;
    public Vector3 footPositionOffset, footRotationOffset;

    private float sphereRadius, footSpacing, lerp;
    private bool isGrounded, isMovingForward;
    private Vector3 storedPosition, currentPosition, newPosition, currentNormal, newNormal;

    private void Start()
    {
        sphereRadius = Sphere.transform.lossyScale.x / 2;
        footSpacing = transform.localPosition.x;
        currentPosition = newPosition = transform.position;
        currentNormal = newNormal = transform.up;
        lerp = 1;
    }

    private void Update()
    {
        Ray ray = new Ray(Sphere.transform.position + (IKRig.right * footSpacing) + Vector3.up * rayStartYOffset, Vector3.down);
        isGrounded = Physics.Raycast(ray, rayLength, terrainLayer);


        transform.position = currentPosition + Vector3.up * footYPosOffset;
        transform.localRotation = Quaternion.Euler(footRotationOffset);

        if (isGrounded)
        {
            RaycastHit hitInfo;

            if (Physics.Raycast(ray, out hitInfo, rayLength, terrainLayer))
            {
                // Update foot position if stepping distance is large enough
                if (Vector3.Distance(newPosition, hitInfo.point) > stepDistanceThreshold && !OtherFoot.IsMoving() && lerp >= 1)
                {
                    lerp = 0;
                    Vector3 direction = Vector3.ProjectOnPlane(hitInfo.point - currentPosition, Vector3.up).normalized;
                    isMovingForward = Vector3.Angle(IKRig.forward, IKRig.InverseTransformDirection(direction)) < 50 || Vector3.Angle(IKRig.forward, IKRig.InverseTransformDirection(direction)) > 130;

                    // Set new position based on step length
                    newPosition = hitInfo.point + direction * (isMovingForward ? stepLength : sideStepLength) + footPositionOffset;
                    newNormal = hitInfo.normal;

                    // Store current X, Z for airborne use
                    storedPosition = new Vector3(newPosition.x, Sphere.transform.position.y - sphereRadius, newPosition.z);
                }
            }
        }
        else
        {
            // Maintain X, Z position while in the air and set Y to bottom of the sphere
            newPosition = storedPosition;
            newPosition.y = Sphere.transform.position.y - sphereRadius;
            newNormal = Vector3.up;
        }

        // Lerp foot position and normal
        if (lerp < 1)
        {
            Vector3 tempPosition = Vector3.Lerp(currentPosition, newPosition, lerp);
            tempPosition.y += Mathf.Sin(lerp * Mathf.PI) * stepHeight;
            currentPosition = tempPosition;
            currentNormal = Vector3.Lerp(currentNormal, newNormal, lerp);
            lerp += Time.deltaTime * stepSpeed;
        }
        else
        {
            currentPosition = newPosition;
            currentNormal = newNormal;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Vector3 gizmoPosition = isGrounded ? newPosition : new Vector3(storedPosition.x, Sphere.transform.position.y - sphereRadius, storedPosition.z);
        Gizmos.DrawSphere(gizmoPosition, 0.1f);
    }

    public bool IsMoving() => lerp < 1;
}