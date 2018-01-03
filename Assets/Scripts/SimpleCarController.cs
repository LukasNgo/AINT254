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
    private float boostCooldown = 0;
    public float boostPower = 1000f;
    public Text boostText;
    public GameObject boostBar;
    public Text speedText;
    public Transform speedoNeedle;
    private bool boostReady = false;
    public ParticleSystem boostEffect;
    public ParticleSystem setDestroyEffect;
    public ParticleSystem setSpawnEffect;
    [Range(1,2)]
    public int playerNumber = 1;
    public AudioSource boostAudioSource;
    private bool isBoostActivated = false;
    public AudioSource respawnSound;
    public AudioSource explodeSound;

    public void Start()
    {
        DestroyEffectStop();
        SpawnEffectStop();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.RightShift) && playerNumber == 1 && boostReady)
        {
            isBoostActivated = true;
        }
        if (Input.GetKeyDown(KeyCode.LeftShift) && playerNumber == 2 && boostReady)
        {
            isBoostActivated = true;
        }
    }

    //particle effect methods
    public void DestroyEffectPlay()
    {
        setDestroyEffect.Play();
        explodeSound.Play();
    }

    public void DestroyEffectStop()
    {
        setDestroyEffect.Stop();
    }

    public void SpawnEffectPlay()
    {
        setSpawnEffect.Play();
        respawnSound.Play();
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

    public void FixedUpdate()
    {

        //boost cooldown timer
        if (boostCooldown <= 100)
        {
            boostCooldown += Time.deltaTime * 15;
            boostText.text = System.Math.Round(boostCooldown, 0).ToString() + " %";
            boostBar.transform.localScale = new Vector3(boostBar.transform.localScale.x, boostCooldown / 100, boostBar.transform.localScale.z);
            if (boostCooldown >= 100)
            {
                boostReady = true;
                boostText.text = "Boost Ready";
            }
        }

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
            float ang = Mathf.Lerp(94f, -92f, Mathf.InverseLerp(0f, 100f, speedoMeter));
            speedoNeedle.transform.eulerAngles = new Vector3(0, 0, ang);

            //engine sound
            GetComponent<AudioSource>().pitch = 1 + speedoMeter / maxSpeed;

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

            //apply braketorque
            if (Input.GetAxis("Vertical") == 0 && playerNumber == 1)
            {
                axleInfo.leftWheel.brakeTorque = maxMotorTorque/2;
                axleInfo.rightWheel.brakeTorque = maxMotorTorque/2;
            }
            if (Input.GetAxis("Vertical") != 0 && playerNumber == 1)
            {
                axleInfo.leftWheel.brakeTorque = 0f;
                axleInfo.rightWheel.brakeTorque = 0f;
            }
            if (Input.GetAxis("Vertical2") == 0 && playerNumber == 2)
            {
                axleInfo.leftWheel.brakeTorque = maxMotorTorque/2;
                axleInfo.rightWheel.brakeTorque = maxMotorTorque/2;
            }
            if (Input.GetAxis("Vertical2") != 0 && playerNumber == 2)
            {
                axleInfo.leftWheel.brakeTorque = 0f;
                axleInfo.rightWheel.brakeTorque = 0f;
            }

            //boost method
            if (axleInfo.motor && isBoostActivated)
            {
                if(boostReady == true)
                {
                    GetComponent<Rigidbody>().AddForce(transform.forward * boostPower, ForceMode.Acceleration);
                    boostAudioSource.Play();
                    boostReady = false;
                    isBoostActivated = false;
                    boostCooldown = 0;
                    boostEffect.Play();
                }
            }
            //if (axleInfo.motor && Input.GetKeyDown(KeyCode.LeftShift) && playerNumber == 2)
            //{
            //    if (boostReady == true)
            //    {
            //        GetComponent<Rigidbody>().AddForce(transform.forward * boostPower, ForceMode.Acceleration);
            //        boostAudioSource.Play();
            //        boostReady = false;
            //        boostCooldown = 0;
            //        boostEffect.Play();
            //    }
            //}

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