using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using TMPro;

public class LapCounter : MonoBehaviour
{
    public TextMeshProUGUI lapTimerText1;
    public TextMeshProUGUI lapTimerText2;
    public TextMeshProUGUI lapTimerText3;
    public TextMeshProUGUI lapTimerText4;
    private float lapTimer1;
    private float lapTimer2;
    private float lapTimer3;
    private bool lapTimerEnabled1 = false;
    private bool lapTimerEnabled2 = false;
    private bool lapTimerEnabled3 = false;
    [Range(1, 2)]
    public int playerSelect;


    void Start()
    {
        lapTimerText1.text = "";
        lapTimerText2.text = "";
        lapTimerText3.text = "";
        lapTimerText4.text = "";
    }

    void FixedUpdate()
    {
        if (playerSelect == 1)
        {
            if (lapTimerEnabled1 == true && Laps.Lap == 1)
            {
                lapTimer1 += Time.deltaTime;
                lapTimerText1.text = "Lap 1 time: " + System.Math.Round(lapTimer1, 2).ToString();
            }
            if (lapTimerEnabled2 == true && Laps.Lap == 2)
            {
                lapTimer2 += Time.deltaTime;
                lapTimerText2.text = "Lap 2 time: " + System.Math.Round(lapTimer2, 2).ToString();
            }
            if (lapTimerEnabled3 == true && Laps.Lap == 3)
            {
                lapTimer3 += Time.deltaTime;
                lapTimerText3.text = "Lap 3 time: " + System.Math.Round(lapTimer3, 2).ToString();
            }
            if (Laps.Lap == 4)
            {
                float bestLap = 0;
                if (lapTimer1 < lapTimer2 && lapTimer1 < lapTimer3)
                {
                    bestLap = lapTimer1;
                }
                if (lapTimer2 < lapTimer1 && lapTimer2 < lapTimer3)
                {
                    bestLap = lapTimer2;
                }
                if (lapTimer3 < lapTimer1 && lapTimer3 < lapTimer2)
                {
                    bestLap = lapTimer3;
                }
                lapTimerText4.text = "Best Lap: " + System.Math.Round(bestLap, 2).ToString();
            }
        }
        else
        {
            if (lapTimerEnabled1 == true && Laps2.Lap == 1)
            {
                lapTimer1 += Time.deltaTime;
                lapTimerText1.text = "Lap 1 time: " + System.Math.Round(lapTimer1, 2).ToString();
            }
            if (lapTimerEnabled2 == true && Laps2.Lap == 2)
            {
                lapTimer2 += Time.deltaTime;
                lapTimerText2.text = "Lap 2 time: " + System.Math.Round(lapTimer2, 2).ToString();
            }
            if (lapTimerEnabled3 == true && Laps2.Lap == 3)
            {
                lapTimer3 += Time.deltaTime;
                lapTimerText3.text = "Lap 3 time: " + System.Math.Round(lapTimer3, 2).ToString();
            }
            if (Laps2.Lap == 4)
            {
                float bestLap = 0;
                if (lapTimer1 < lapTimer2 && lapTimer1 < lapTimer3)
                {
                    bestLap = lapTimer1;
                }
                if (lapTimer2 < lapTimer1 && lapTimer2 < lapTimer3)
                {
                    bestLap = lapTimer2;
                }
                if (lapTimer3 < lapTimer1 && lapTimer3 < lapTimer2)
                {
                    bestLap = lapTimer3;
                }
                lapTimerText4.text = "Best Lap: " + System.Math.Round(bestLap,2).ToString();
            }
        }

    }

    void OnTriggerEnter(Collider other)
    {
        if (playerSelect == 1)
        {
            if (other.gameObject.tag == "StartTimer" && Laps.Lap == 1)
            {
                lapTimerEnabled1 = true;
            }
            if (other.gameObject.tag == "StartTimer" && Laps.Lap == 2)
            {
                lapTimerEnabled2 = true;
            }
            if (other.gameObject.tag == "StartTimer" && Laps.Lap == 3)
            {
                lapTimerEnabled3 = true;
            }
            if (other.gameObject.tag == "EndTimer" && Laps.Lap == 1)
            {
                lapTimerEnabled1 = false;
            }
            if (other.gameObject.tag == "EndTimer" && Laps.Lap == 2)
            {
                lapTimerEnabled2 = false;
            }
            if (other.gameObject.tag == "EndTimer" && Laps.Lap == 3)
            {
                lapTimerEnabled3 = false;
            }
        }
        else
        {
            if (other.gameObject.tag == "StartTimer" && Laps2.Lap == 1)
            {
                lapTimerEnabled1 = true;
            }
            if (other.gameObject.tag == "StartTimer" && Laps2.Lap == 2)
            {
                lapTimerEnabled2 = true;
            }
            if (other.gameObject.tag == "StartTimer" && Laps2.Lap == 3)
            {
                lapTimerEnabled3 = true;
            }
            if (other.gameObject.tag == "EndTimer" && Laps2.Lap == 1)
            {
                lapTimerEnabled1 = false;
            }
            if (other.gameObject.tag == "EndTimer" && Laps2.Lap == 2)
            {
                lapTimerEnabled2 = false;
            }
            if (other.gameObject.tag == "EndTimer" && Laps2.Lap == 3)
            {
                lapTimerEnabled3 = false;
            }

        }

    }

}