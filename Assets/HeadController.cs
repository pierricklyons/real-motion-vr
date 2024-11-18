using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Assertions.Must;

public class HeadController : MonoBehaviour
{
    public XRInputManager XRInputManager;
    public GameObject Head;
    public GameObject XRCamera;

    void Awake()
    {
    }

    void Update()
    {
        // XRCamera.transform.rotation = XRInputManager.CameraControllerRotation;
    }
}
