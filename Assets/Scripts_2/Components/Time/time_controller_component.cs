using UnityEngine;
using System.Collections;

public class time_controller_component : MonoBehaviour {

    public static time_controller_component time_controller;

	// Use this for initialization
	void Start () {
	    if(time_controller != null)
        {
            Destroy(time_controller.gameObject);
        }
        time_controller = this;
	}

    public void Warp_Time(float _target_speed, float _rate)
    {
        StopAllCoroutines();
        StartCoroutine_Auto(Modify_Time_Speed(_target_speed, _rate));
    }
	
    public IEnumerator Modify_Time_Speed(float _target_speed, float _rate)
    {
        while (Time.timeScale != _target_speed)
        {
            Time.timeScale = Mathf.Lerp(Time.timeScale, _target_speed, _rate * (Time.deltaTime * (1.0f + Time.deltaTime)));
            Time.fixedDeltaTime = 0.02f * (Time.timeScale * (1.0f + Time.deltaTime));
            yield return new WaitForFixedUpdate();
        }
    }

    public void Reset_Time()
    {
        Time.timeScale = 1.0f;
        Time.fixedDeltaTime = 0.02f;
    }

    
}
