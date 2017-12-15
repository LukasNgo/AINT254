using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class CarSelectionCamera : MonoBehaviour {

    [SerializeField]
    private Camera cam;
    [SerializeField]
    private TextMeshProUGUI carInfo, selectCarText;
    [SerializeField]

    public static int playerOneSelection;
    public static int playerTwoSelection;

    private bool isPlayer1Selected = false;
    private bool isPlayer2Selected = false;
    private bool isSelectTimeout = true;

    private Vector3 pos1 = new Vector3(50f, 9f, 10f);
    private Vector3 pos2 = new Vector3(40f, 9f, 10f);
    private Vector3 pos3 = new Vector3(30f, 9f, 10f);
    private Vector3 pos4 = new Vector3(20f, 9f, 10f);

    private int currentPos = 1;

    void Start () {
        cam.transform.position = pos1;
	}
	
	void Update () {

        if (currentPos == 1 && Input.GetKeyDown(KeyCode.D) || currentPos == 1 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Transition(pos1, pos2, 2));
        }

        if (currentPos == 2 && Input.GetKeyDown(KeyCode.A) || currentPos == 2 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Transition(pos2, pos1, 1));
        }
        if (currentPos == 2 && Input.GetKeyDown(KeyCode.D) || currentPos == 2 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Transition(pos2, pos3, 3));
        }

        if (currentPos == 3 && Input.GetKeyDown(KeyCode.A) || currentPos == 3 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Transition(pos3, pos2, 2));
        }
        if (currentPos == 3 && Input.GetKeyDown(KeyCode.D) || currentPos == 3 && Input.GetKeyDown(KeyCode.RightArrow))
        {
            StartCoroutine(Transition(pos3, pos4, 4));
        }

        if (currentPos == 4 && Input.GetKeyDown(KeyCode.A) || currentPos == 4 && Input.GetKeyDown(KeyCode.LeftArrow))
        {
            StartCoroutine(Transition(pos4, pos3, 3));
        }
        if (currentPos == 0)
        {
            carInfo.text = "";
        }
        if (currentPos == 1)
        {
            carInfo.text = "RED\n\nSpeed: 60\nAcceleration: 3000\nHandling: 45\nBoost: 1000";
        }
        if (currentPos == 2)
        {
            carInfo.text = "BLUE\n\nSpeed: 50\nAcceleration: 5000\nHandling: 40\nBoost: 2000";
        }
        if (currentPos == 3)
        {
            carInfo.text = "GREEN\n\nSpeed: 40\nAcceleration: 3000\nHandling: 50\nBoost: 3000";
        }
        if (currentPos == 4)
        {
            carInfo.text = "YELLOW\n\nSpeed: 65\nAcceleration: 4000\nHandling: 35\nBoost: 1000";
        }

        if((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return)) && !isPlayer1Selected && isSelectTimeout)
        {
            selectCarText.text = "Player 2 select car";
            playerOneSelection = currentPos;
            isPlayer1Selected = true;
            StartCoroutine(PlayerSelectTimeout());
        }
        if((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return)) && isPlayer1Selected && isSelectTimeout)
        {
            playerTwoSelection = currentPos;
            isPlayer2Selected = true;
        }
        if(isPlayer1Selected && isPlayer2Selected)
        {
            SceneManager.LoadScene(1);
        }
    }

    private IEnumerator Transition(Vector3 startPos, Vector3 finishPos, int pos)
    {
        currentPos = 0;
        float t = 0.0f;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale) * 2;
            transform.position = Vector3.Lerp(startPos, finishPos, t);
            yield return 0;
        }
        currentPos = pos;
    }

    private IEnumerator PlayerSelectTimeout()
    {
        isSelectTimeout = false;
        yield return new WaitForSecondsRealtime(1f);
        isSelectTimeout = true;
    }
}
