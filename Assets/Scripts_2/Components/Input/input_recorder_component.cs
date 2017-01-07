using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class input_recorder_component : MonoBehaviour, rotation_input_interface, input_button_interface {

    [SerializeField]
    private int inputs_to_record = 25;

    private Queue<Vector2> input_records;
    private input_component input;

    private float total_distance_moved;

    private float button_hold_time;

    private void OnEnable()
    {
        if (null == input_records)
        {
            input_records = new Queue<Vector2>();
        }
        if (null == input)
        {
            input= GetComponent<input_component>();
        }

        total_distance_moved = 0.0f;
    }


    public void On_Rotation_Input(float _horizontal, float _vertical)
    {
        Add_Record(_horizontal, _vertical);
    }

    private void Add_Record(float _horizontal, float _vertical)
    {
        Vector2 record_vector = new Vector2(_horizontal, _vertical);
        input_records.Enqueue(record_vector);
        total_distance_moved += record_vector.magnitude;
        Remove_Old_Records();
    }

    public void On_Button_Input(action_buttons _action_button, action_button_states _action_button_state)
    {
        if(action_button_states.held == _action_button_state)
        {
            button_hold_time += Time.deltaTime;
        }
        else
        {
            Reset_Records();
            button_hold_time = 0.0f;
        }
    }

    private void Remove_Old_Records()
    {
        while(inputs_to_record < input_records.Count)
        {
            Vector2 removed_record = input_records.Dequeue();
            total_distance_moved -= removed_record.magnitude;
        }
    }

    public void Reset_Records()
    {
        input_records.Clear();
        total_distance_moved = 0.0f;
    }

    public Vector2[] Get_Records()
    {
        return input_records.ToArray();
    }

    public Vector2 Get_Average()
    {
        Vector2 average = Vector2.zero;

        Vector2[] record_array = Get_Records();
        if (0 < record_array.Length)
        {
            for (int i = 0; i < record_array.Length; i++)
            {
                average += record_array[i];
            }
            average /= record_array.Length;
        }

        return average;
    }

    public float Get_Average_Magnitude()
    {
        return Get_Average().magnitude;
    }

    public float Get_Total_Distance_Moved()
    {
        return total_distance_moved;
    }

    public float Get_Button_Hold_Time()
    {
        return button_hold_time;
    }
}
