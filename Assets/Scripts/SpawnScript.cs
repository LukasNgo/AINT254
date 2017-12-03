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

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            StartCoroutine(Spawn(p1car1, -3));
            StartCoroutine(Spawn(p1car2, -3));
            StartCoroutine(Spawn(p1car3, -3));
            StartCoroutine(Spawn(p1car4, -3));
        }
        if (other.gameObject.tag == "Player2")
        {
            StartCoroutine(Spawn(p2car1, 3));
            StartCoroutine(Spawn(p2car2, 3));
            StartCoroutine(Spawn(p2car3, 3));
            StartCoroutine(Spawn(p2car4, 3));
        }
    }

    IEnumerator Spawn(GameObject player, int offset)
    {
        player.GetComponent<Rigidbody>().isKinematic = true;
        player.GetComponent<SimpleCarController>().DestroyEffect();
        yield return new WaitForSeconds(1f);
        player.transform.position = spawnLocation.transform.position + new Vector3(offset, 0, 0);
        player.transform.rotation = spawnLocation.transform.rotation;
        player.GetComponent<Rigidbody>().isKinematic = false;
        player.GetComponent<SimpleCarController>().SpawnEffect();
    }
}
