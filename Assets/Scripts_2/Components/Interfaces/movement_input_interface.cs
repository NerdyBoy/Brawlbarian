using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface movement_input_interface : IEventSystemHandler {
    void On_Movement_Input(float _forward, float _sideways);
}
