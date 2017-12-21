using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnScript : MonoBehaviour {

    public Transform spawnLocation;
    public GameObject p1car1;
    public GameObject p1car2;
    public GameObject p1car3;
    public GameObject p1car4;
    public GameObject p2car1;
    public GameObject p2car2;
    public GameObject p2car3;
    public GameObject p2car4;

    private bool isSpawning = false;

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" && !isSpawning)
        {
            StartCoroutine(Spawn(p1car1, -3));
            StartCoroutine(Spawn(p1car2, -3));
            StartCoroutine(Spawn(p1car3, -3));
            StartCoroutine(Spawn(p1car4, -3));
        }
        if (other.gameObject.tag == "Player2" && !isSpawning)
        {
            StartCoroutine(Spawn(p2car1, 3));
            StartCoroutine(Spawn(p2car2, 3));
            StartCoroutine(Spawn(p2car3, 3));
            StartCoroutine(Spawn(p2car4, 3));
        }
    }

    IEnumerator Spawn(GameObject player, int offset)
    {
        isSpawning = true;
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<SimpleCarController>().DestroyEffectPlay();
        yield return new WaitForSeconds(1f);
        player.GetComponent<SimpleCarController>().SpawnEffectPlay();
        yield return new WaitForSeconds(1f);
        player.GetComponent<SimpleCarController>().DestroyEffectStop();
        player.transform.position = spawnLocation.transform.position + new Vector3(offset, 0, 0);
        player.transform.rotation = spawnLocation.transform.rotation;
        yield return new WaitForSeconds(0.5f);
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<SimpleCarController>().SpawnEffectStop();
        isSpawning = false;
    }
}
