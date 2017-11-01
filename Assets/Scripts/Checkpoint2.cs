using UnityEngine;
using System.Collections;

public class Checkpoint2 : MonoBehaviour
{

    void Start()
    {

    }

    void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.tag == "Player2" && transform == Laps2.checkpointA[Laps2.currentCheckpoint].transform)
        {
            //Check so we dont exceed our checkpoint quantity
            if (Laps2.currentCheckpoint + 1 < Laps2.checkpointA.Length)
            {
                //Add to currentLap if currentCheckpoint is 0
                if (Laps2.currentCheckpoint == 0)
                    Laps2.currentLap++;
                Laps2.currentCheckpoint++;
            }
            else
            {
                //If we dont have any Checkpoints left, go back to 0
                Laps2.currentCheckpoint = 0;
            }
        }


    }

}