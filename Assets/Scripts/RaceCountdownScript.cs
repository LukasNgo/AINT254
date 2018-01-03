using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class RaceCountdownScript : MonoBehaviour {

    public TextMeshProUGUI countdownText;
    public GameObject p1car1;
    public GameObject p1car2;
    public GameObject p1car3;
    public GameObject p1car4;
    public GameObject p2car1;
    public GameObject p2car2;
    public GameObject p2car3;
    public GameObject p2car4;
    public GameObject overviewCamera1;
    public GameObject overviewCamera2;
    public GameObject canvasGUI;
    public AudioSource shortBeep;
    public AudioSource longBeep;

    // Use this for initialization
    void Start () {
        overviewCamera1.SetActive(false);
        overviewCamera2.SetActive(false);
        canvasGUI.SetActive(false);
        StartCoroutine(Countdown());
    }

    public IEnumerator Countdown()
    {
        overviewCamera1.SetActive(true);
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
        shortBeep.Play();
        countdownText.text = "3";
        yield return new WaitForSecondsRealtime(1f);
        overviewCamera1.SetActive(false);
        overviewCamera2.SetActive(true);
        countdownText.text = "2";
        shortBeep.Play();
        yield return new WaitForSecondsRealtime(1f);
        overviewCamera2.SetActive(false);
        countdownText.text = "1";
        shortBeep.Play();
        canvasGUI.SetActive(true);
        yield return new WaitForSecondsRealtime(1f);
        countdownText.text = "GO!";
        longBeep.Play();
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
