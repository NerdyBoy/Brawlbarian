using UnityEngine;
using System.Collections;

public class t_input_4 : MonoBehaviour {
    
    //delegates        
    public delegate void On_Mouse_Moved(Vector2 _mouse_delta);
    public event On_Mouse_Moved on_mouse_moved;

    public delegate void On_Move_Keys_Pressed(Vector2 _keyboard_delta);
    public event On_Move_Keys_Pressed on_move_keys_pressed;

    //recording
    public const int MAX_FRAMES_TO_RECORD = 300;
    private Vector2[] mouse_positions;

    private int current_frame = 0;
    private int last_frame = 0;    

    void Start()
    {
        on_mouse_moved += Record_Mouse_Delta;
        mouse_positions = new Vector2[MAX_FRAMES_TO_RECORD];
    }
	
	// Update is called once per frame
	void Update () {
        Handle_Input();
	}

    void Handle_Input()
    {
        last_frame = current_frame; //is this bit the wrong way around?
        current_frame++;
        if(MAX_FRAMES_TO_RECORD == current_frame)
        {
            current_frame = 0;
        }

        Handle_Mouse_Input();
        Handle_Keyboard_Input();
    } 

    void Handle_Mouse_Input()
    {
        float mouse_y_delta = Input.GetAxis("Mouse Y");
        float mouse_x_delta = Input.GetAxis("Mouse X");

        Vector2 mouse_delta = new Vector2(mouse_x_delta, mouse_y_delta);

        if (mouse_y_delta != 0.0f || mouse_x_delta != 0.0f)
        {
            if(on_mouse_moved != null)
            {
                on_mouse_moved(mouse_delta);
            }
        }
    }

    void Handle_Keyboard_Input()
    {
        float keyboard_horizontal_delta = Input.GetAxis("Horizontal");
        float keyboard_vertical_delta = Input.GetAxis("Vertical");

        if(keyboard_horizontal_delta != 0.0f || keyboard_vertical_delta != 0.0f)
        {
            Vector2 keyboard_delta = new Vector2(keyboard_horizontal_delta, keyboard_vertical_delta);
            if(on_move_keys_pressed != null)
            {
                on_move_keys_pressed(keyboard_delta);
            }
        }
    }

    void Record_Mouse_Delta(Vector2 _mouse_position)
    {
        mouse_positions[current_frame] = _mouse_position;
    }

    public Vector2[] Get_Mouse_Positions()
    {
        return mouse_positions;
    }
}
