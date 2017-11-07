using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

[System.Serializable]
public class AxleInfo
{
    public WheelCollider leftWheel;
    public WheelCollider rightWheel;
    public bool motor;
    public bool steering;
}

public class SimpleCarController : MonoBehaviour
{
    public List<AxleInfo> axleInfos;
    public float maxMotorTorque;
    public float maxSteeringAngle;
    private int boostTime = 500;
    public Text boostText;
    private bool boostReady = false;
    public ParticleSystem boostEffect;
    [Range(1,2)]
    public int playerNumber = 1;

    // finds the corresponding visual wheel
    // correctly applies the transform
    public void ApplyLocalPositionToVisuals(WheelCollider collider)
    {
        if (collider.transform.childCount == 0)
        {
            return;
        }

        Transform visualWheel = collider.transform.GetChild(0);

        Vector3 position;
        Quaternion rotation;
        collider.GetWorldPose(out position, out rotation);

        visualWheel.transform.position = position;
        visualWheel.transform.rotation = rotation;
    }

    private IEnumerator Boost()
    {
        while (boostTime > 0)
        {
            yield return new WaitForSeconds(0.5f);
            boostTime -= 1;
            boostText.text = boostTime.ToString();
            if(boostTime <= 0)
            {
                boostReady = true;
                boostText.text = "boost ready";
            }
        }
    }

    public void Update()
    {
                StartCoroutine("Boost");

    }

    public void FixedUpdate()
    {

        Vector3 oldRot = transform.rotation.eulerAngles;

        float motor;
        float steering;

        if (playerNumber == 1)
        {
            motor = maxMotorTorque * Input.GetAxis("Vertical");
            steering = maxSteeringAngle * Input.GetAxis("Horizontal");
        }
        else
        {
            motor = maxMotorTorque * Input.GetAxis("Vertical2");
            steering = maxSteeringAngle * Input.GetAxis("Horizontal2");
        }

        foreach (AxleInfo axleInfo in axleInfos)
        {
            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor)
            {
                GetComponent<Rigidbody>().drag = 0.0f;
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
        
            if (axleInfo.motor && Input.GetKeyDown(KeyCode.RightShift) && playerNumber == 1)
            {
                if(boostReady == true)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Acceleration);
                    boostReady = false;
                    boostTime = 500;
                    boostEffect.Play();
                }
            }
            if (axleInfo.motor && Input.GetKeyDown(KeyCode.LeftShift) && playerNumber == 2)
            {
                if (boostReady == true)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * 1000, ForceMode.Acceleration);
                    boostReady = false;
                    boostTime = 500;
                    boostEffect.Play();
                }
            }
            if (axleInfo.motor && Input.GetAxis("Vertical") < 0 && playerNumber == 1)
            {
                GetComponent<Rigidbody>().drag = 1.5f;
                GetComponent<Rigidbody>().AddForce(transform.forward * (-10), ForceMode.Acceleration);
            }
            if (axleInfo.motor && Input.GetAxis("Vertical2") < 0 && playerNumber == 2)
            {
                GetComponent<Rigidbody>().drag = 1.5f;
                GetComponent<Rigidbody>().AddForce(transform.forward * (-10), ForceMode.Acceleration);
            }

            transform.rotation = Quaternion.Euler(oldRot.x, oldRot.y, 0);

            ApplyLocalPositionToVisuals(axleInfo.leftWheel);
            ApplyLocalPositionToVisuals(axleInfo.rightWheel);
        }
    }
}