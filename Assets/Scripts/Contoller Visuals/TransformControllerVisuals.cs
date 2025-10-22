using UnityEngine;

public class TransformControllerVisuals : MonoBehaviour
{
    public XRInputManager XRInputManager;
    public PhysicsRig PhysicsRig;
    public GameObject LeftHandControllerVisual;
    public GameObject RightHandControllerVisual;

    private GameObject head;

    private void Awake()
    {
        head = PhysicsRig.Head;
    }

    private void Update()
    {
        LeftHandControllerVisual.transform.position = head.transform.rotation * PhysicsRig.LeftHandJoint.targetPosition + head.transform.position;
        RightHandControllerVisual.transform.position = head.transform.rotation * PhysicsRig.RightHandJoint.targetPosition + head.transform.position;
        LeftHandControllerVisual.transform.rotation = head.transform.rotation * XRInputManager.LeftHandControllerRotation;
        RightHandControllerVisual.transform.rotation = head.transform.rotation * XRInputManager.RightHandControllerRotation;
    }
}
