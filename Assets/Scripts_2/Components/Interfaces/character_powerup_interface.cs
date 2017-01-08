using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface character_powerup_interface : IEventSystemHandler {

    void Enable_Damage_Modifier(float _modify_amount, float _lifetime);
    void Enable_Speed_Modifier(float _modify_amount, float _lifetime);
}
