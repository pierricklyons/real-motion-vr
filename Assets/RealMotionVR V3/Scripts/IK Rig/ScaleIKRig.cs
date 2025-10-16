using UnityEngine;

public class ScaleIKRig : MonoBehaviour
{
    public PhysicsRig physicsRig;
    public XRInputManager xrInputManager;

    private float defaultHeight;

    private void Awake()
    {
        defaultHeight = physicsRig.UserHeight;
    }

    private void Update()
    {
        transform.localScale = new Vector3(physicsRig.UserHeight / defaultHeight, physicsRig.UserHeight / defaultHeight, physicsRig.UserHeight / defaultHeight);
    }
}
