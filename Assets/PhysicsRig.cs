using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhysicsRig : MonoBehaviour
{
    public GameObject XROrigin;

    public Transform headController;
    public Transform leftHandController;
    public Transform rightHandController;

    public ConfigurableJoint headJoint;
    public ConfigurableJoint leftHandJoint;
    public ConfigurableJoint rightHandJoint;

    public CapsuleCollider bodyCollider;

    public float bodyHeightMin = .5f;
    public float bodyHeightMax = 2;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void FixedUpdate()
    {
        bodyCollider.height = Mathf.Clamp(headController.localPosition.y, bodyHeightMin, bodyHeightMax);
        bodyCollider.center = new Vector3(headController.localPosition.x, bodyCollider.height / 2, headController.localPosition.z);

        headJoint.targetPosition = headController.localPosition;

        leftHandJoint.targetPosition = leftHandController.localPosition;
        leftHandJoint.targetRotation = leftHandController.localRotation;

        rightHandJoint.targetPosition = rightHandController.localPosition;
        rightHandJoint.targetRotation = rightHandController.localRotation;

        XROrigin.transform.position = bodyCollider.transform.position;
    }
}
