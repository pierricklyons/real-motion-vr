using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpineController : MonoBehaviour
{
    public XRInputManager XRInputManager;
    public GameObject Head;
    public GameObject Chest;
    public GameObject Fender;
    public ConfigurableJoint Spine;

    public JumpController JumpController;
    // public CrouchController CrouchController;

    private float verticalOffset;
    // private float spineTarget;

    void Awake()
    {
        verticalOffset = Fender.transform.position.y + (Head.transform.position.y - Chest.transform.position.y);
    }

    void FixedUpdate()
    {
        SetSpineTargetPosition(XRInputManager.CameraControllerPosition.y);
    }

    private void SetSpineTargetPosition(float spineTarget)
    {
        float target = spineTarget - verticalOffset;
        Spine.targetPosition = new Vector3(0, target, 0);
    }
}
