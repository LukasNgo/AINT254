using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public Transform spawnLocation;
    public GameObject player1;
    public GameObject player2;
    public ParticleSystem particleEffect;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Spawn(player1, -2));
        }
        if(other.gameObject.tag == "Player2")
        {
            StartCoroutine(Spawn(player2, 2));
        }
    }

    IEnumerator Spawn(GameObject player, int num)
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        yield return new WaitForSeconds(1);
        player.transform.position = spawnLocation.transform.position + new Vector3(num, 0, 0);
        player.transform.rotation = spawnLocation.transform.rotation;
        player.GetComponent<Rigidbody>().isKinematic = false;
        particleEffect.Play();
    }
}
