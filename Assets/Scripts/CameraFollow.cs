using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField]
    private Transform target;
    [SerializeField]
    private float distance, height, rotationDamping, heightDamping;

    private float rotation_vector;

    //reversing camera
	private void FixedUpdate ()
    {
        Vector3 local_velocity = target.InverseTransformDirection(target.GetComponent<Rigidbody>().velocity);
        if (local_velocity.z < -0.5f)
        {
            rotation_vector = target.eulerAngles.y + 200;
        }
        else
        {
            rotation_vector = target.eulerAngles.y;
        }
    }

    //smooth follow camera
    private void LateUpdate()
    {
        float wantedAngle = rotation_vector;
        float wantedHeight = target.position.y + height;
        float myAngle = transform.eulerAngles.y;
        float myHeight = transform.position.y;

        myAngle = Mathf.LerpAngle(myAngle, wantedAngle, rotationDamping * Time.deltaTime);
        myHeight = Mathf.LerpAngle(myHeight, wantedHeight, heightDamping * Time.deltaTime);

        Quaternion currentRotation = Quaternion.Euler(0, myAngle, 0);

        transform.position = target.position;
        transform.position -= currentRotation * Vector3.forward * distance;

        Vector3 temp = transform.position;
        temp.y = myHeight;
        transform.position = temp;

        transform.LookAt(target);
    }
}
