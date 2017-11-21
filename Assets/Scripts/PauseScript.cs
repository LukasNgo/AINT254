using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseScript : MonoBehaviour
{

    [SerializeField]
    private GameObject pausePanel;

    private bool isPaused = false;

    void Start()
    {
        pausePanel.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            if (isPaused)
            {
                ContinueGame();
            }
        }
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pausePanel.SetActive(true);
        StartCoroutine(Timer());
        Debug.Log("paused");
    }

    public void ContinueGame()
    {
        Time.timeScale = 1;
        pausePanel.SetActive(false);
        StartCoroutine(Timer());
        Debug.Log("unpaused");
    }

    private IEnumerator Timer()
    {
        yield return new WaitForSecondsRealtime(0.3f);
        isPaused = !isPaused;
    }

    public void ButtonExitToMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void ButtonExit()
    {
        Application.Quit();
    }

}
