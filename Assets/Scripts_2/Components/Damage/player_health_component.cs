using UnityEngine;
using System.Collections;

public class player_health_component : MonoBehaviour {

    [SerializeField]
    private float health_value = 0;

    public void On_Player_Modify_Health(float _amount)
    {
        health_value += _amount;

        if (health_value <= 0.0f)
        {
            this.gameObject.SendMessage("On_Health_Is_Zero", SendMessageOptions.DontRequireReceiver);
        }
        else
        {
            //do something else
        }
    }
}
