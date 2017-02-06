using UnityEngine;
using UnityEngine.EventSystems;
using System.Collections;

public class health_component : MonoBehaviour, health_interface {
    
    [SerializeField]
    private float health_value = 0;

    public void On_Modify_Health(float _amount)
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
