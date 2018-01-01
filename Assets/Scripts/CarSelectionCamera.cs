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

    public static int playerOneSelection = 0;
    public static int playerTwoSelection = 0;

    private bool isPlayer1Selected = false;
    private bool isPlayer2Selected = false;
    private bool isSelectTimeout = true;

    private Vector3 pos1 = new Vector3(50f, 9f, 10f);
    private Vector3 pos2 = new Vector3(40f, 9f, 10f);
    private Vector3 pos3 = new Vector3(30f, 9f, 10f);
    private Vector3 pos4 = new Vector3(20f, 9f, 10f);

    private int currentPos = 1;

    public Image fadeImage;
    private bool isInTransition;
    private bool isShowing;
    private float transition;
    private float duration;


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
            //speed 60 acc 3000 handling 45 boost 1000
            carInfo.text = "POLICE\n\nSpeed: Fast\nAcceleration: Low\nHandling: High\nBoost power: Low";
        }
        if (currentPos == 2)
        {
            //speed 50 acc 5000 handling 40 boost 2000
            carInfo.text = "TAXI\n\nSpeed: Medium\nAcceleration: High\nHandling: Medium\nBoost power: Medium";
        }
        if (currentPos == 3)
        {
            //speed 40 acc 5000 handling 50 boost 3000
            carInfo.text = "RED\n\nSpeed: Slow\nAcceleration: High\nHandling: Very High\nBoost power: High";
        }
        if (currentPos == 4)
        {
            //speed 65 acc 4000 handling 35 boost 1000
            carInfo.text = "YELLOW\n\nSpeed: Very Fast\nAcceleration: Medium\nHandling: Low\nBoost power: Low";
        }

        if((Input.GetKeyDown(KeyCode.F) || Input.GetKeyDown(KeyCode.Return)) && !isPlayer1Selected && isSelectTimeout)
        {
            selectCarText.text = "Player 2 select car";
            playerOneSelection = currentPos;
            isPlayer1Selected = true;
            Fade(true, 0.5f);
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

        //fade in/out function
        if (!isInTransition)
        {
            return;
        }

        transition += (isShowing) ? Time.deltaTime * (1 / duration) : -Time.deltaTime * (1 / duration);
        fadeImage.color = Color.Lerp(new Color(1, 1, 1, 0), Color.white, transition);

        if (transition > 1 || transition < 0)
        {
            isInTransition = false;
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
        transform.position = pos1;
        currentPos = 1;
        yield return new WaitForSecondsRealtime(1f);
        Fade(false, 0.5f);
        isSelectTimeout = true;
    }

    //fade in/out function
    public void Fade(bool showing, float duration)
    {
        isShowing = showing;
        isInTransition = true;
        this.duration = duration;
        transition = (isShowing) ? 0 : 1;
    }
}
