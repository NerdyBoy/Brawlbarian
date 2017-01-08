using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface hit_tracking_interface : IEventSystemHandler {
    character_score_component Get_Hit_Root_Character_Score();
    void Set_Hit_Root_Character(character_score_component _charater_score);
    void Increase_Hit_Root_Score(int _amount);
}
