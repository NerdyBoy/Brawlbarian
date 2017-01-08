using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface rotation_input_interface : IEventSystemHandler {

    void On_Rotation_Input(float _horizontal, float _vertical);
}
