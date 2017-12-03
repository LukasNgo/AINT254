using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnSelectedCars : MonoBehaviour {

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
        if (CarSelectionCamera.playerOneSelection == 1)
        {
            p1car1.SetActive(true);
        }
        if (CarSelectionCamera.playerOneSelection == 2)
        {
            p1car2.SetActive(true);
        }
        if (CarSelectionCamera.playerOneSelection == 3)
        {
            p1car3.SetActive(true);
        }
        if (CarSelectionCamera.playerOneSelection == 4)
        {
            p1car4.SetActive(true);
        }

        if (CarSelectionCamera.playerTwoSelection == 1)
        {
            p2car1.SetActive(true);
        }
        if (CarSelectionCamera.playerTwoSelection == 2)
        {
            p2car2.SetActive(true);
        }
        if (CarSelectionCamera.playerTwoSelection == 3)
        {
            p2car3.SetActive(true);
        }
        if (CarSelectionCamera.playerTwoSelection == 4)
        {
            p2car4.SetActive(true);
        }
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
