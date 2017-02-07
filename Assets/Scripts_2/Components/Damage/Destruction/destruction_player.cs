using UnityEngine;
using System.Collections;

public class destruction_player : destruction_component{

    public override void On_Health_Is_Zero()
    {
        base.On_Health_Is_Zero();
        Application.LoadLevel(4);
    }
}
