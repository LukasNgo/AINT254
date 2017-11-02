using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinScript : MonoBehaviour {

    public Text P1WinText;
    public Text P2WinText;

	// Use this for initialization
	void Start () {
        P1WinText.text = "";
        P2WinText.text = "";

    }
	
	// Update is called once per frame
	void Update () {
        if (Laps.Lap == 4 && Laps2.Lap != 4)
        {
            P1WinText.text = "Winner";
            P2WinText.text = "Loser";
        }
        if (Laps2.Lap == 4 && Laps.Lap != 4)
        {
            P1WinText.text = "Loser";
            P2WinText.text = "Winner";
        }
    }
}
