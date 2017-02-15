using UnityEngine;
using System.Collections;
using System;

public class character_score_component : MonoBehaviour, character_score_interface {

    ui_score_component score_component;
    public int score;

    void Start()
    {
        score_component = FindObjectOfType<ui_score_component>();
    }

    public void Modify_Score(int _amount)
    {
        score += _amount;
        if(score_component == null)
        {
            score_component = FindObjectOfType<ui_score_component>();
        }
        if (score_component != null)
        {
            score_component.Update_Score(score);
        }
        //BroadcastMessage("Update_Score", score, SendMessageOptions.DontRequireReceiver);
    }

    public int Get_Score()
    {
        return score;
    }
}
