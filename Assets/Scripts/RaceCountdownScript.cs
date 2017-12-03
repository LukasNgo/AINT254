using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCountdownScript : MonoBehaviour {

    public Text countdownText;
    public GameObject p1car1;
    public GameObject p1car2;
    public GameObject p1car3;
    public GameObject p1car4;
    public GameObject p2car1;
    public GameObject p2car2;
    public GameObject p2car3;
    public GameObject p2car4;

    // Use this for initialization
    void Start () {
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        countdownText.text = "";
        p1car1.GetComponent<Rigidbody>().isKinematic = true;
        p1car2.GetComponent<Rigidbody>().isKinematic = true;
        p1car3.GetComponent<Rigidbody>().isKinematic = true;
        p1car4.GetComponent<Rigidbody>().isKinematic = true;
        p2car1.GetComponent<Rigidbody>().isKinematic = true;
        p2car2.GetComponent<Rigidbody>().isKinematic = true;
        p2car3.GetComponent<Rigidbody>().isKinematic = true;
        p2car4.GetComponent<Rigidbody>().isKinematic = true;
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "2";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "1";
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "GO!";
        Time.timeScale = 1;
        p1car1.GetComponent<Rigidbody>().isKinematic = false;
        p1car2.GetComponent<Rigidbody>().isKinematic = false;
        p1car3.GetComponent<Rigidbody>().isKinematic = false;
        p1car4.GetComponent<Rigidbody>().isKinematic = false;
        p2car1.GetComponent<Rigidbody>().isKinematic = false;
        p2car2.GetComponent<Rigidbody>().isKinematic = false;
        p2car3.GetComponent<Rigidbody>().isKinematic = false;
        p2car4.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
    }
}
