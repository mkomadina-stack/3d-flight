using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{


    public float score;
    public float fastestScore = 0.0f;

    public Text scoreText;
    public Text altitudeText;
    public Text speedBoostText;
    public Text fastestScoreText;
    public GameObject Player;
    public Image hitImage;
  

    public void ResetScore()
    {
        score = 0.0f;
    }

    public void ReachedEnd()
    {
        if (fastestScore > 0.0f)
        {
            if (score < fastestScore)
            {
                fastestScore = score;
            }
        }
        else
        {
            fastestScore = score;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        score = 0.0f;

        // change the alpha of image
        var tempColor = hitImage.color;
        tempColor.a = 0.0f;
        hitImage.color = tempColor;

    }

    // Update is called once per frame
    void Update()
    {
      score += 0.01f;
      scoreText.text = score.ToString("#.#") + " s";

      altitudeText.text = Player.transform.position.y.ToString("#.0") + " m";

      if (Player.GetComponent<Player>().isSpeedBoost()) {
        speedBoostText.text = "Speed Burst ON";
      }
      else {
        speedBoostText.text = "";
      }

      if (fastestScore > 0.0f)
        {
            fastestScoreText.text = fastestScore.ToString("#.#");
        }

    //  Debug.Log(score);
    }
}
