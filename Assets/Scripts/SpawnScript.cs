using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public Transform spawnLocation;
    public GameObject player1;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player1.transform.position = spawnLocation.transform.position;
        }
    }
}
