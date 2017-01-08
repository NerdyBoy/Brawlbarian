using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface character_score_interface : IEventSystemHandler {
    void Modify_Score(int _amount);
}
