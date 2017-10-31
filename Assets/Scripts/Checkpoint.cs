using UnityEngine;
using System.Collections;

public class Checkpoint : MonoBehaviour
{

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player" && transform == Laps.checkpointA[Laps.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (Laps.currentCheckpoint + 1 < Laps.checkpointA.Length)
            {
                //Add to currentLap if currentCheckpoint is 0
                if (Laps.currentCheckpoint == 0)
                    Laps.currentLap++;
                Laps.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                Laps.currentCheckpoint = 0;
            }
        }


    }

}