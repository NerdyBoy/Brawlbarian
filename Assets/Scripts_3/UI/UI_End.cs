using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_End : MonoBehaviour {

    public Text highscore_text;
    public Text current_score_text;

    private int current_highscore = 0;
    private int current_score = 0;

    private void Start()
    {
        cursor_display.Enable_Cursor();
        StartCoroutine(Setup());
    }

    IEnumerator Setup()
    {
        while(Score.score == null)
        {
            yield return new WaitForSeconds(0.25f);
        }

        current_score = Score.score.player_score;
        if(PlayerPrefs.HasKey("highscore") == true)
        {
            current_highscore = PlayerPrefs.GetInt("highscore");
        }

        highscore_text.text = "Higscore: " + current_highscore.ToString();
        current_score_text.text = "Score: " + current_score.ToString();
        StartCoroutine(Update_Highscore());
    }

    IEnumerator Update_Highscore()
    {
        yield return new WaitForSeconds(1.5f);
        if(current_score > current_highscore)
        {
            highscore_text.text = "Score: " + current_score.ToString();
            PlayerPrefs.SetInt("highscore", current_score);
        }
    }
}
