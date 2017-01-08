using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface destruction_interface : IEventSystemHandler {

    void On_Health_Is_Zero();
}
