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
    public int maxSpeed;
    private int boostCooldown = 500;
    public float boostPower = 1000f;
    private int boostTime = 500;
    public Text boostText;
    public Text speedText;
    private bool boostReady = false;
    public ParticleSystem boostEffect;
    public ParticleSystem setDestroyEffect;
    public ParticleSystem setSpawnEffect;
    [Range(1,2)]
    public int playerNumber = 1;

    public void Start()
    {
        DestroyEffectStop();
        SpawnEffectStop();
    }

    //particle effect methods
    public void DestroyEffectPlay()
    {
        setDestroyEffect.Play();
    }

    public void DestroyEffectStop()
    {
        setDestroyEffect.Stop();
    }

    public void SpawnEffectPlay()
    {
        setSpawnEffect.Play();
    }

    public void SpawnEffectStop()
    {
        setSpawnEffect.Stop();
    }

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
         StartCoroutine(Boost());
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
            //if wheels are not touching the ground add force downwards
            bool groundedLeft;
            bool groundedRight;
            groundedRight = axleInfo.rightWheel.isGrounded;
            groundedLeft = axleInfo.leftWheel.isGrounded;
            if (!groundedLeft || !groundedRight)
            {
                GetComponent<Rigidbody>().AddForce(transform.up * (-50), ForceMode.Acceleration);
            }

            //get car speed
            float speedoMeter;
            speedoMeter = GetComponent<Rigidbody>().velocity.magnitude;
            speedText.text = "Speed: " + (int)speedoMeter;


            if (axleInfo.steering)
            {
                axleInfo.leftWheel.steerAngle = steering;
                axleInfo.rightWheel.steerAngle = steering;
            }
            if (axleInfo.motor && speedoMeter < maxSpeed)
            {
                GetComponent<Rigidbody>().drag = 0.0f;
                axleInfo.leftWheel.motorTorque = motor;
                axleInfo.rightWheel.motorTorque = motor;
            }
            else
            {
                axleInfo.leftWheel.motorTorque = 0;
                axleInfo.rightWheel.motorTorque = 0;
            }
        
            //boost method
            if (axleInfo.motor && Input.GetKeyDown(KeyCode.RightShift) && playerNumber == 1)
            {
                if(boostReady == true)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * boostPower, ForceMode.Acceleration);
                    boostReady = false;
                    boostTime = boostCooldown;
                    boostEffect.Play();
                }
            }
            if (axleInfo.motor && Input.GetKeyDown(KeyCode.LeftShift) && playerNumber == 2)
            {
                if (boostReady == true)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * boostPower, ForceMode.Acceleration);
                    boostReady = false;
                    boostTime = boostCooldown;
                    boostEffect.Play();
                }
            }

            //add force when reversing
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