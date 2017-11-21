using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RaceCountdownScript : MonoBehaviour {

    public Text countdownText;
    public GameObject car1;
    public GameObject car2;

	// Use this for initialization
	void Start () {
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        countdownText.text = "";
        car1.GetComponent<Rigidbody>().isKinematic = true;
        car2.GetComponent<Rigidbody>().isKinematic = true;
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
        car1.GetComponent<Rigidbody>().isKinematic = false;
        car2.GetComponent<Rigidbody>().isKinematic = false;
        yield return new WaitForSeconds(1f);
        countdownText.text = "";
    }
}
