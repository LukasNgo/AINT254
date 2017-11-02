using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public Transform spawnLocation;
    public GameObject player1;
    public GameObject player2;
    public ParticleSystem particleEffectSpawnPlayer1;
    public ParticleSystem particleEffectSpawnPlayer2;
    public ParticleSystem particleEffectDestroyPlayer1;
    public ParticleSystem particleEffectDestroyPlayer2;

    void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(Spawn(player1, -3, particleEffectSpawnPlayer1, particleEffectDestroyPlayer1));
        }
        if(other.gameObject.tag == "Player2")
        {
            StartCoroutine(Spawn(player2, 3, particleEffectSpawnPlayer2, particleEffectDestroyPlayer2));
        }
    }

    IEnumerator Spawn(GameObject player, int num, ParticleSystem particleSpawn, ParticleSystem particleDestroy)
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        particleDestroy.Play();
        yield return new WaitForSeconds(1f);
        player.transform.position = spawnLocation.transform.position + new Vector3(num, 0, 0);
        player.transform.rotation = spawnLocation.transform.rotation;
        player.GetComponent<Rigidbody>().isKinematic = false;
        particleSpawn.Play();
    }
}
