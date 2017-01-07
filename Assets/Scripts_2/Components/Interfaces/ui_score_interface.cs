using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public interface ui_score_interface : IEventSystemHandler {

    void Update_Score(int _score);
}
