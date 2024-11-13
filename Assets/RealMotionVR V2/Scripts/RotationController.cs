using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotationController : MonoBehaviour
{
    public InputManager InputManager;
    public GameObject XROrigin;

    public GameObject Head;
    public GameObject Chest;
    public GameObject Fender;

    public float TurnForce = 5.0f;

    void FixedUpdate()
    {
        RotateBody();
    }

    // Rotates Rig AND Body
    private void RotateBody()
    {
        if (InputManager.rightTrackpadPressed == 1) return;
        if (InputManager.rightTrackpadValue.x > 0.25f || InputManager.rightTrackpadValue.x < -0.25f)
        {
            XROrigin.transform.RotateAround(InputManager.CameraController.transform.position, Vector3.up, InputManager.rightTrackpadValue.x * TurnForce);

            Head.transform.rotation = XROrigin.transform.rotation;
            Chest.transform.rotation = XROrigin.transform.rotation;
            Fender.transform.rotation = XROrigin.transform.rotation;
        }
    }
}
