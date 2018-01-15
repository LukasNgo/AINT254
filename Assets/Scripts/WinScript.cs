using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WinScript : MonoBehaviour {

    public TextMeshProUGUI P1WinText;
    public TextMeshProUGUI P2WinText;
    public AudioSource winSound;
    public GameObject trophy;

	// Use this for initialization
	void Start () {
        P1WinText.text = "";
        P2WinText.text = "";
        trophy.SetActive(false);

    }
	
	// Update is called once per frame
	void Update () {
        if (Laps.Lap == 4 && Laps2.Lap != 4)
        {
            P1WinText.text = "Winner" + System.Environment.NewLine + "Press ESC to show menu";
            P2WinText.text = "Loser";
            trophy.SetActive(true);
            winSound.Play();
        }
        if (Laps2.Lap == 4 && Laps.Lap != 4)
        {
            P1WinText.text = "Loser";
            P2WinText.text = "Winner" + System.Environment.NewLine + "Press ESC to show menu";
            trophy.SetActive(true);
            winSound.Play();
        }
    }
}
