﻿using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class Laps : MonoBehaviour
{

    // These Static Variables are accessed in "checkpoint" Script
    public Transform[] checkPointArray;
    public static Transform[] checkpointA;
    public static int currentCheckpoint = 0;
    public static int currentLap = 0;
    public Vector3 startPos;
    public static int Lap;
    public TextMeshProUGUI lapCounter;

    void Start()
    {
        startPos = transform.position;
        currentCheckpoint = 0;
        currentLap = 0;
    }

    void FixedUpdate()
    {
        Lap = currentLap;
        checkpointA = checkPointArray;
        if (currentLap < 4)
        {
            lapCounter.text = "Current Lap: " + currentLap;
        }
        else
        {
            lapCounter.text = "Race Finished";
        }

    }

}