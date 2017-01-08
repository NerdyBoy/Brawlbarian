using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface input_button_interface : IEventSystemHandler {
    void On_Button_Input(action_buttons _button_action, action_button_states _button_state);
}
