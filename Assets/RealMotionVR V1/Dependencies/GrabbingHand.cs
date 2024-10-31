using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEditor.XR.LegacyInputHelpers;
using UnityEngine.XR;
using UnityEngine.InputSystem;

// Describes grabbing for holding objects and climbing
public class GrabbingHand : MonoBehaviour
{
    // Public inspector fields
    [Header("Action")]
    public InputActionReference ControllerSelect;
    [Header("Hand")]
    public GameObject Hand;
    [Header("Release Delay")]
    public float releaseDelay = 500f;

    // Input fields
    private float ControllerSelected;

    // Grab fiels
    private FixedJoint joint = null;
    private bool attached = false;

    // On every physics tick
    void FixedUpdate()
    {
        GetInput();
        if (attached && ControllerSelected == 0) Release();
    }

    // Get controller inputs
    void GetInput()
    {
        ControllerSelected = ControllerSelect.action.ReadValue<float>();
    }

    // Creates and attaches joint at collision points of contact and stops propagation to other gameobjects
    void Attach(Collision collision)
    {
        attached = true;
        joint = gameObject.AddComponent<FixedJoint>();
        joint.anchor = collision.contacts[0].point;
        joint.connectedBody = collision.contacts[0].otherCollider.transform.GetComponentInParent<Rigidbody>();
        joint.enableCollision = false;
    }

    // Destroys joint set up by attach
    void Release()
    {
        Destroy(joint);
        joint = null;
        attached = false;
        // gameObject.GetComponent<Hands>().EnableHandColliderDelay(releaseDelay);
    }

    // Calls Attach() on collision & on input
    void OnCollisionStay(Collision collision)
    {
        if (!attached && ControllerSelected == 1 && collision.gameObject.tag == "Grabbable") Attach(collision);
    }
}
