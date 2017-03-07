using UnityEngine;
using System.Collections;

public class Destruction_Monitor : MonoBehaviour {

    public static Destruction_Monitor destruction_monitor;

    int initial_number;
    int remaining_number;

    private void OnLevelWasLoaded(int level)
    {
        Destructible_Object[] destructible_objects = FindObjectsOfType<Destructible_Object>();
        initial_number = destructible_objects.Length;
        remaining_number = initial_number;
        UI_Destruction_Meter.ui_destruction_meter.Update_Destruction_Meter(initial_number, remaining_number);
    }

    // Use this for initialization
    void Start () {
        Destructible_Object[] destructible_objects = FindObjectsOfType<Destructible_Object>();
        initial_number = destructible_objects.Length;
        remaining_number = initial_number;
        if (UI_Destruction_Meter.ui_destruction_meter != null)
        {
            UI_Destruction_Meter.ui_destruction_meter.Update_Destruction_Meter(initial_number, remaining_number);
        }

        if (destruction_monitor == null)
        {
            destruction_monitor = this;
        }
	}
	
    public void Destroy_Object()
    {
        if (UI_Destruction_Meter.ui_destruction_meter != null)
        {
            remaining_number -= 1;
            UI_Destruction_Meter.ui_destruction_meter.Update_Destruction_Meter(initial_number, remaining_number);
        }
    }
}
