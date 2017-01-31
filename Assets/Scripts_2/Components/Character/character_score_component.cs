using UnityEngine;
using System.Collections;
using System;

public class character_score_component : MonoBehaviour, character_score_interface {

    public static character_score_component character_score;

    public int score;

    public void Modify_Score(int _amount)
    {
        score += _amount;
        BroadcastMessage("Update_Score", score);
    }

    public int Get_Score()
    {
        return score;
    }
}
