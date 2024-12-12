using UnityEngine;

public class TransformControllerVisuals : MonoBehaviour
{
    public XRInputManager XRInputManager;
    public GameObject LeftHandControllerVisual;
    public GameObject RightHandControllerVisual;

    private void Update()
    {
        LeftHandControllerVisual.transform.position = XRInputManager.LeftHandControllerPosition;
        RightHandControllerVisual.transform.position = XRInputManager.RightHandControllerPosition;
        LeftHandControllerVisual.transform.rotation = XRInputManager.LeftHandControllerRotation;
        RightHandControllerVisual.transform.rotation = XRInputManager.RightHandControllerRotation;
    }
}
