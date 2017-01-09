using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface hit_tracking_interface : IEventSystemHandler {
    character_controller Get_Hit_Root_Character();
    void Set_Hit_Root_Character(character_controller _charater);
}
