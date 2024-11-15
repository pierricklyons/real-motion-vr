using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class MovementController : MonoBehaviour
{
    public XRInputManager XRInputManager;
    public GameObject XRCamera;
    public GameObject Sphere;
    public float MovementForce;

    private Rigidbody sphereRigidbody;
    private float sphereRadius;

    void Awake()
    {
        sphereRigidbody = Sphere.GetComponent<Rigidbody>();
        sphereRadius = Sphere.GetComponent<SphereCollider>().radius * Sphere.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        MoveSphere();
    }

    private void MoveSphere()
    {
        sphereRigidbody.AddTorque(GetSphereDirection() * MovementForce, ForceMode.Force);
    }

    private Vector3 GetSphereDirection()
    {
        Quaternion headYaw = Quaternion.Euler(0, XRCamera.transform.eulerAngles.y, 0);
        Vector3 moveDirection = headYaw * new Vector3(XRInputManager.LeftTranslateAnchorValue.x, 0, XRInputManager.LeftTranslateAnchorValue.y);
        return new Vector3(moveDirection.z, 0, -moveDirection.x);
    }
}
