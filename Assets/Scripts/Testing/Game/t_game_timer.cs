using UnityEngine;
using System;
using System.Diagnostics;

public class t_game_timer {

    private Stopwatch timer = null;
    private int time_to_run = -1;

    public t_game_timer() {
    }

    public t_game_timer(int _time_to_run) {
        time_to_run = _time_to_run;
    }

    public void Set_Time_To_Run(int _time_to_run) {
        time_to_run = _time_to_run;
    }

    public int Get_Time_To_Run() {
        return time_to_run;
    }

    public bool Get_Countdown_Finished() {
        if (null != timer && true == timer.IsRunning) {
            if (-1 != time_to_run && timer.Elapsed.Seconds >= time_to_run) {
                return true;
            }
        }
        return false;
    }

	public void Create_Stopwatch() {
        timer = new Stopwatch();
    }

    public void Start_Stopwatch() {
        timer.Start();
    }

    public void Stop_Stopwatch() {
        timer.Stop();
    }

    public void Reset_Stopwatch() {
        timer.Reset();
    }

    public int Get_Minutes_Elapsed() {
        return timer.Elapsed.Minutes;
    }

    public int Get_Seconds_Elapsed() {
        return timer.Elapsed.Seconds;
    }

    public int Get_Seconds_Elapsed_Minus_Minutes() {
        return timer.Elapsed.Seconds - (timer.Elapsed.Minutes * 60);
    }

    public string Get_Elapsed_Time_String(bool _minutes, bool _seconds) {
        string elapsed_time = "";
        if(null != timer && (false != _minutes || false != _seconds)) {

            TimeSpan timespan = TimeSpan.FromSeconds(timer.Elapsed.Seconds);

            if(true == _minutes && true == _seconds) {
                elapsed_time = String.Format("{0:D2}:{1:D2}", timespan.Minutes, timespan.Seconds);
            }
            else if(true == _minutes) {
                elapsed_time = String.Format("{0:D2}", timespan.Minutes);
            }
            else {
                elapsed_time = String.Format("{0:D2}", timespan.Seconds);
            }
        }

        return elapsed_time;
    }
}
