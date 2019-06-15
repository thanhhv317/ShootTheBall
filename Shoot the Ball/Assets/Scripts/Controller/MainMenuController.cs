using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuController : MonoBehaviour {
    public GameObject panelBye;

    public void _PlayGame()
    {
        Application.LoadLevel("gamePlay");
    }
    public void _ExitGame()
    {
        StartCoroutine(delay());
        Application.Quit();
    }

    IEnumerator delay()
    {
        panelBye.SetActive(true);
        yield return new WaitForSeconds(1.5f);
    }
}
