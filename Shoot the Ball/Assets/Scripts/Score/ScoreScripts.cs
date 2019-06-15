using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreScripts : MonoBehaviour {

    public static int scoreValue = 0;
    Text score;
    public Text scoreEndGame ;
    public static int scoreEnd = 0;

	// Use this for initialization
	void Start () {
        score = GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        scoreEndGame.text = "" + scoreValue;
        score.text  =""+ scoreValue;
	}
}
