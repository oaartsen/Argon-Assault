using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Scoreboard : MonoBehaviour {

    // Configuration parameters

    // State variables
    int score = 0;

    // Cached parameters
    TMP_Text scoreText = null;

    void Start() {
        scoreText = GetComponent<TMP_Text>();
        scoreText.text = score.ToString();
    }

    public void IncreaseScore(int amountToIncrease) {

        score += amountToIncrease;
        scoreText.text = score.ToString();


    }

}
