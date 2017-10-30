using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;

    public float smoothSpeed = 0.125f;
    public Vector3 offset;

    private Vector3 oldRot;

	void FixedUpdate () {
        Vector3 desiredPosition = target.position + offset;
        Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
        transform.position = smoothedPosition;

        oldRot = target.transform.rotation.eulerAngles;
        transform.rotation = Quaternion.Euler(60, oldRot.y, 0);
    }
}
