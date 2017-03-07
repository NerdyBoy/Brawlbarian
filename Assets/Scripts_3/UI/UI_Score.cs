using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class UI_Score : MonoBehaviour
{

    public static UI_Score ui_score;
    public Text score_text;

    // Use this for initialization
    void Start()
    {
        if (ui_score == null)
        {
            ui_score = this;
        }
    }

    public void Update_Score(int _score)
    {
        if(score_text != null)
        {
            score_text.text = "Score: " + _score.ToString();
        }
    }
}
