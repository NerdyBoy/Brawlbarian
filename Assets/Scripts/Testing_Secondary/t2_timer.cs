using UnityEngine;
using System.Collections;

public class t2_timer : MonoBehaviour {

    private int initial_minutes;
    private int initial_seconds;

    [SerializeField]
    private int game_time_minutes;
    [SerializeField]
    private int game_time_seconds;
    [SerializeField]
    private int total_time_in_seconds;
    [SerializeField]
    private int current_time;
    private float elapsed_time;

    public bool timer_active = false;

	// Use this for initialization
	void Start () {
        if(0 < game_time_minutes || 0 < game_time_seconds) {
            Set_Time(game_time_minutes, game_time_seconds);
        }
	}

    public void Set_Time(int _minutes, int _seconds) {
        initial_minutes = _minutes;
        initial_seconds = _seconds;
        game_time_minutes = _minutes;
        game_time_seconds = _seconds;
        game_time_minutes += _seconds / 60;
        game_time_seconds -= ((_seconds / 60) * 60);
        Calculate_Total_Seconds();
    }

    void Calculate_Total_Seconds() {
        total_time_in_seconds = game_time_seconds;
        total_time_in_seconds += game_time_minutes * 60;
    }

    public void Start_Timer() {
        timer_active = true;
    }

    public void Stop_Timer() {
        timer_active = false;
    }

    public void Reset_Timer() {
        timer_active = false;
        game_time_minutes = initial_minutes;
        game_time_seconds = initial_seconds;
        Set_Time(game_time_minutes, game_time_seconds);
    }
	
	// Update is called once per frame
	void Update () {
        if (true == timer_active) {
            Calculate_Current_Time();
        }
	}

    void Calculate_Current_Time() {
        elapsed_time += Time.deltaTime;
        current_time = total_time_in_seconds - (int)elapsed_time;
    }

    public int Get_Current_Time_Minutes() {
        int minutes = current_time / 60;        
        return minutes;
    }

    public int Get_Current_Time_Seconds() {
        int seconds = current_time - ((current_time / 60) * 60);
        return seconds;
    }

    public int Get_Seconds_Remaining() {
        return total_time_in_seconds - (int)elapsed_time;
    }


}
