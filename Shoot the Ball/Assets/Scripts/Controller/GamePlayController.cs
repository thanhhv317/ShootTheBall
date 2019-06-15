using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GamePlayController : MonoBehaviour {

    public static GamePlayController gameplay;

    private float timeR = 0.7f;

    [SerializeField]
    private GameObject pausePanel;

    public GameObject pauseButton;


    public GameObject losePanel;

    private void Awake()
    {
        if (gameplay == null)
        {
            gameplay = this;
        }
    }

    public void _pauseGame()
    {     
        pausePanel.SetActive(true);
        pauseButton.SetActive(false);
        StartCoroutine(delay(timeR));
    }
    public void _resumeGame()
    {
        pausePanel.SetActive(false);
        pauseButton.SetActive(true);
        Time.timeScale = 1f;
        
    }

    IEnumerator delay(float time)
    {
        yield return new WaitForSeconds(time);
        Time.timeScale = 0;
        
    }
    public void _exitGame()
    {
        Application.LoadLevel("mainMenu");
        Time.timeScale = 1f;
        ScoreScripts.scoreValue -= ScoreScripts.scoreValue;
    }

    public void _restartGame()
    {
        Time.timeScale = 1f;
        Application.LoadLevel("gamePlay");
        pauseButton.SetActive(true);
        ScoreScripts.scoreValue -= ScoreScripts.scoreValue;
    }


}
