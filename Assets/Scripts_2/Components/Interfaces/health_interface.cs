using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface health_interface : IEventSystemHandler {

    void On_Modify_Health(float _amount);
}
