using UnityEngine;
using System.Collections;
using System;

public class hit_tracking_component : MonoBehaviour, hit_tracking_interface {

    private character_score_component root_hit_character_score;

    public character_score_component Get_Hit_Root_Character_Score()
    {
        return root_hit_character_score;
    }

    public void Set_Hit_Root_Character(character_score_component _charater_score)
    {
        root_hit_character_score= _charater_score;
    }

    public void Increase_Hit_Root_Score(int _amount)
    {
        if(null != root_hit_character_score)
        {
            root_hit_character_score.Modify_Score(_amount);
        }
    }
}
