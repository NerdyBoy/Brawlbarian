using UnityEngine;
using System.Collections;

public class countdown : MonoBehaviour {
    
    private float time_in_seconds;

    private float time_remaining_seconds;

    private bool timer_active = true;

	// Use this for initialization
	void Start () {
	    
	}
	
	// Update is called once per frame
	void Update () {
	    if(true == timer_active)
        {
            time_remaining_seconds -= Time.deltaTime;
            if(null != ui_time_remaining_global_component.ui_time_component)
            {
                ui_time_remaining_global_component.ui_time_component.Update_Time(Get_Time_String());
            }
        }
	}

    public void Set_Time(float _seconds)
    {
        time_in_seconds = _seconds;
        time_remaining_seconds = time_in_seconds;
    }

    public void Start_Timer()
    {
        timer_active = true;
    }

    public void Pause_Timer()
    {
        timer_active = false;
    }

    public void Reset_Timer()
    {
        time_remaining_seconds = time_in_seconds;
    }

    public float Get_Time_Remaining()
    {
        return time_remaining_seconds;
    }

    public string Get_Time_String()
    {
        return ((int)time_remaining_seconds).ToString();
    }
}
